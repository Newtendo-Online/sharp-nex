using System;
using System.Buffers.Binary;
using System.Security.Cryptography;

namespace nex
{
    public class KeyDerivationOld
    {
        private readonly int _baseCount;
        private readonly int _pidCount;

        public KeyDerivationOld(int baseCount = 65000, int pidCount = 1024)
        {
            _baseCount = baseCount;
            _pidCount = pidCount;
        }

        public byte[] DeriveKey(byte[] password, uint pid)
        {
            byte[] key = password;
            int iterations = _baseCount + (int)(pid % (uint)_pidCount);
            for (int i = 0; i < iterations; i++)
            {
                key = MD5.HashData(key);
            }
            return key;
        }
    }

    public class KeyDerivationNew
    {
        private readonly int _baseCount;
        private readonly int _pidCount;

        public KeyDerivationNew(int baseCount = 1, int pidCount = 1)
        {
            _baseCount = baseCount;
            _pidCount = pidCount;
        }

        public byte[] DeriveKey(byte[] password, ulong pid)
        {
            byte[] key = password;
            for (int i = 0; i < _baseCount; i++)
            {
                key = MD5.HashData(key);
            }

            byte[] extendedKey = new byte[key.Length + 8];
            Buffer.BlockCopy(key, 0, extendedKey, 0, key.Length);
            BinaryPrimitives.WriteUInt64LittleEndian(extendedKey.AsSpan(key.Length), pid);
            key = extendedKey;

            for (int i = 0; i < _pidCount; i++)
            {
                key = MD5.HashData(key);
            }

            return key;
        }
    }

    public class KerberosEncryption
    {
        private readonly byte[] _key;

        public KerberosEncryption(byte[] key)
        {
            _key = key;
        }

        public bool Check(ReadOnlySpan<byte> buffer)
        {
            if (buffer.Length < 0x10) return false;
            ReadOnlySpan<byte> data = buffer[..^0x10];
            ReadOnlySpan<byte> checksum = buffer[^0x10..];

            Span<byte> mac = stackalloc byte[16];
            HMACMD5.HashData(_key, data, mac);

            return checksum.SequenceEqual(mac);
        }

        public byte[] Decrypt(byte[] buffer)
        {
            if (!Check(buffer))
            {
                throw new ArgumentException("Invalid Kerberos checksum (incorrect password)");
            }

            var data = buffer.AsSpan()[..^0x10].ToArray();
            var rc4 = new RC4Cipher(_key);
            return rc4.Transform(data);
        }

        public byte[] Encrypt(byte[] buffer)
        {
            var rc4 = new RC4Cipher(_key);
            byte[] encrypted = rc4.Transform(buffer);

            Span<byte> mac = stackalloc byte[16];
            HMACMD5.HashData(_key, encrypted, mac);

            byte[] result = new byte[encrypted.Length + 16];
            Buffer.BlockCopy(encrypted, 0, result, 0, encrypted.Length);
            mac.CopyTo(result.AsSpan(encrypted.Length));
            return result;
        }
    }

    public class ClientTicket
    {
        public byte[]? SessionKey { get; set; }
        public uint Target { get; set; }
        public byte[] Internal { get; set; } = Array.Empty<byte>();

        public static ClientTicket Decrypt(byte[] data, byte[] key)
        {
            using var stream = new StreamOut();
            var kerberos = new KerberosEncryption(key);
            byte[] decrypted = kerberos.Decrypt(data);

            using var stream_in = new StreamIn(decrypted);
            int keySize = Convert.ToInt32(stream.Server.KerberosKeySize);

            var ticket = new ClientTicket
            {
                SessionKey = stream_in.ReadBytes(keySize),
                Target = stream_in.ReadPid(),
                Internal = stream_in.ReadBuffer()
            };
            return ticket;
        }

        public byte[] Encrypt(byte[] key)
        {
            using var stream = new StreamOut();
            byte[] sessionKey = SessionKey ?? throw new ArgumentException("SessionKey cannot be null");
            int keySize = Convert.ToInt32(stream.Server.KerberosKeySize);

            if (sessionKey.Length != keySize)
            {
                throw new ArgumentException("Incorrect session_key size");
            }

            stream.WriteBytes(sessionKey);
            stream.WritePid(Target);
            stream.WriteBuffer(Internal);

            byte[] data = stream.Get();
            var kerberos = new KerberosEncryption(key);
            return kerberos.Encrypt(data);
        }
    }

    public class ServerTicket
    {
        public DateTime Timestamp { get; set; }
        public uint Source { get; set; }
        public byte[]? SessionKey { get; set; }

        public static ServerTicket Decrypt(byte[] data, byte[] key)
        {
            using var stream = new StreamOut();
            int ticketVersion = Convert.ToInt32(stream.Server.KerberosTicketVersion);

            if (ticketVersion == 1)
            {
                using var streamIn = new StreamIn(data);
                byte[] ticketKey = streamIn.ReadBuffer();
                byte[] remainingData = streamIn.ReadBuffer();

                using var md5 = MD5.Create();
                byte[] combined = new byte[key.Length + ticketKey.Length];
                Buffer.BlockCopy(key, 0, combined, 0, key.Length);
                Buffer.BlockCopy(ticketKey, 0, combined, key.Length, ticketKey.Length);
                key = md5.ComputeHash(combined);
                data = remainingData;
            }

            var kerberos = new KerberosEncryption(key);
            byte[] decrypted = kerberos.Decrypt(data);

            using var stream_in = new StreamIn(decrypted);
            int keySize = Convert.ToInt32(stream.Server.KerberosKeySize);

            var ticket = new ServerTicket
            {
                Timestamp = stream_in.ReadDateTime(),
                Source = stream_in.ReadPid(),
                SessionKey = stream_in.ReadBytes(keySize)
            };
            return ticket;
        }

        public byte[] Encrypt(byte[] key, StreamOut stream)
        {
            byte[] sessionKey = SessionKey ?? throw new ArgumentException("SessionKey cannot be null");
            int keySize = Convert.ToInt32(stream.Server.KerberosKeySize);

            if (sessionKey.Length != keySize)
            {
                throw new ArgumentException("Incorrect session_key length");
            }

            stream.WriteDateTime(Timestamp);
            stream.WritePid(Source);
            stream.WriteBytes(sessionKey);

            byte[] data = stream.Get();
            int ticketVersion = Convert.ToInt32(stream.Server.KerberosTicketVersion);

            if (ticketVersion == 1)
            {
                byte[] ticketKey = RandomNumberGenerator.GetBytes(16);

                using var md5 = MD5.Create();
                byte[] combined = new byte[key.Length + ticketKey.Length];
                Buffer.BlockCopy(key, 0, combined, 0, key.Length);
                Buffer.BlockCopy(ticketKey, 0, combined, key.Length, ticketKey.Length);
                byte[] finalKey = md5.ComputeHash(combined);

                var kerbo = new KerberosEncryption(finalKey);
                byte[] encrypted = kerbo.Encrypt(data);

                using var outStream = new StreamOut();
                outStream.WriteBuffer(ticketKey);
                outStream.WriteBuffer(encrypted);
                return outStream.Get();
            }

            var kerberos = new KerberosEncryption(key);
            return kerberos.Encrypt(data);
        }
    }

    public record Credentials(ClientTicket Ticket, uint Pid, uint Cid);
}