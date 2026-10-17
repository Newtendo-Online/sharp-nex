using System;
using System.Buffers.Binary;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using System.Text;

namespace nex
{
    public enum PacketType : ushort
    {
        SynPacket = 0,
        ConnectPacket = 1,
        DataPacket = 2,
        DisconnectPacket = 3,
        PingPacket = 4,
        UserPacket = 5
    }

    [Flags]
    public enum PacketFlags : ushort
    {
        FlagAck = 1,
        FlagReliable = 2,
        FlagNeedAck = 4,
        FlagHasSize = 8,
        FlagMultiAck = 0x200,
    }

    public enum Options : ushort
    {
        OptSupportedFunctions = 0,
        OptConnectionSignature = 1,
        OptInitialSequenceID = 3,
        OptMaxSubstreamID = 4
    }

    public class PRUDPPacket
    {
        public PRUDPClient ?Sender { get; set; }
        public PacketType Type { get; set; }
        public PacketFlags Flags { get; set; }
        public uint SourceType { get; set; }
        public uint SourcePort { get; set; }
        public uint DestinationType { get; set; }
        public uint DestinationPort { get; set; }
        public uint SessionID { get; set; }
        public uint PacketID { get; set; }
        public uint FragmentID { get; set; }
        public uint StreamID { get; set; }
        public byte[] ?ConnectionSignature { get; set; }
        public byte[] ?Payload { get; set; }

        public bool HasFlag(PacketFlags Flag) { return (Flags & Flag) != 0; }

        public static uint SumBytes(ReadOnlySpan<byte> values)
        {
            uint sum = 0;
            foreach (byte value in values)
            {
                sum = unchecked(sum + value);
            }

            return sum;
        }
    }

    public class PRUDPPacketV0 : PRUDPPacket
    {
        public uint CalculateChecksum(byte[] data)
        {
            var server = Sender?.Server;
            string accessKey = server?.AccessKey ?? string.Empty;
            uint checksum = SumBytes(Encoding.UTF8.GetBytes(accessKey));

            if (server?.ChecksumVersion == 0)
            {
                int paddedLength = (data.Length + 3) & ~3;
                byte[] paddedData = new byte[paddedLength];
                Buffer.BlockCopy(data, 0, paddedData, 0, data.Length);

                uint result = checksum & 0xFF;
                for (int offset = 0; offset < paddedData.Length; offset += 4)
                {
                    uint word = BinaryPrimitives.ReadUInt32LittleEndian(
                        paddedData.AsSpan(offset, 4));

                    result = unchecked(result + word);
                }

                return result;
            }

            int fullLength = data.Length & ~3;
            uint temp = 0;

            for (int offset = 0; offset < fullLength; offset += 4)
            {
                uint word = BinaryPrimitives.ReadUInt32LittleEndian(
                    data.AsSpan(offset, 4));

                temp = unchecked(temp + word);
            }

            checksum = unchecked(checksum + SumBytes(data.AsSpan(fullLength)));

            Span<byte> tempBytes = stackalloc byte[4];
            BinaryPrimitives.WriteUInt32LittleEndian(tempBytes, temp);
            checksum = unchecked(checksum + SumBytes(tempBytes));

            return checksum & 0xFF;
        }

        public byte[] CalculateDataSignature(byte[] sessionKey)
        {
            var server = Sender?.Server;
            string accessKey = server?.AccessKey ?? string.Empty;
            byte[] payload = Payload ?? Array.Empty<byte>();

            byte[] data = payload;

            if (accessKey != "ridfebb9")
            {
                using var ms = new MemoryStream();

                if (sessionKey != null && sessionKey.Length > 0)
                {
                    ms.Write(sessionKey);
                }

                Span<byte> header = stackalloc byte[3];
                BinaryPrimitives.WriteUInt16LittleEndian(header, (ushort)PacketID);
                header[2] = (byte)FragmentID;
                ms.Write(header);

                if (payload.Length > 0)
                {
                    ms.Write(payload);
                }

                data = ms.ToArray();
            }

            if (data.Length > 0)
            {
                byte[] hmacKey = MD5.HashData(Encoding.UTF8.GetBytes(accessKey));

                using var hmac = new HMACMD5(hmacKey);
                byte[] digest = hmac.ComputeHash(data);

                return digest.AsSpan(0, 4).ToArray();
            }

            return new byte[] { 0x78, 0x56, 0x34, 0x12 };
        }

        public void Decode(byte[] Data)
        {
            if (Sender == null || Sender.Server == null)
            {
                throw new Exception("Sender or Server is not initialized.");
            }

            var stream = new StreamIn(Data);
            var server = Sender.Server;
            long start = stream.ByteOffset();

            byte source = stream.ReadUInt8();
            byte destination = stream.ReadUInt8();

            SourceType = (uint)(source >> 4);
            SourcePort = (uint)(source & 0xF);
            DestinationType = (uint)(destination >> 4);
            DestinationPort = (uint)(destination & 0xF);

            if (server.ChecksumVersion == 0)
            {
                byte typeAndFlags = stream.ReadUInt8();
                Flags = (PacketFlags)(typeAndFlags >> 3);
                Type = (PacketType)(typeAndFlags & 7);
            }
            else
            {
                ushort typeAndFlags = stream.ReadUInt16LE();
                Flags = (PacketFlags)(typeAndFlags >> 4);
                Type = (PacketType)(typeAndFlags & 0xF);
            }

            SessionID = stream.ReadUInt8();
            PacketID = stream.ReadUInt16LE();

            if (Type == PacketType.SynPacket || Type == PacketType.ConnectPacket)
            {
                ConnectionSignature = stream.ReadBytesNext(4);
            }

            if (Type == PacketType.DataPacket)
            {
                FragmentID = stream.ReadUInt8();
            }

            ushort PayloadSize = 0;

            if (HasFlag(PacketFlags.FlagHasSize))
            {
                PayloadSize = stream.ReadUInt16LE();
            }
            else
            {
                if (server.ChecksumVersion == 0)
                {
                    PayloadSize = (ushort)(stream.Remaining() - 4);
                }
                else
                {
                    PayloadSize = (ushort)(stream.Remaining() - 1);
                }
            }

            if (stream.Remaining() < PayloadSize)
            {
                throw new Exception("Failed to read PRUDPv0 payload. Not have enough data");
            }

            Payload = stream.ReadBytesNext(PayloadSize);

            if (server.ChecksumVersion == 0 && stream.Remaining() < 4)
            {
                throw new Exception("Failed to read PRUDPv0 checksum. Not have enough data");
            }
            else if (server.ChecksumVersion != 0 && stream.Remaining() < 1)
            {
                throw new Exception("Failed to read PRUDPv0 checksum. Not have enough data");
            }

            byte[] fullBytes = stream.Bytes();
            byte[] checksumData = fullBytes[(int)start..(int)stream.ByteOffset()];

            uint checksum = 0;
            try
            {
                if (server.ChecksumVersion == 0)
                {
                    checksum = stream.ReadUInt32LE();
                }
                else
                {
                    byte checksumU8 = stream.ReadUInt8();
                    checksum = checksumU8;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to read PRUDPv0 checksum. {ex.Message}", ex);
            }

            uint calculatedChecksum = CalculateChecksum(checksumData);
            if (checksum != calculatedChecksum)
            {
                throw new Exception("Invalid PRUDPv0 checksum");
            }
        }

        public byte[] Encode()
        {
            var server = Sender?.Server ?? throw new Exception("Sender or Server is not initialized.");
            var stream = new StreamOut();

            stream.WriteUInt8((byte)(SourcePort | (SourceType << 4)));
            stream.WriteUInt8((byte)(DestinationPort | (DestinationType << 4)));

            if (server.ChecksumVersion == 0)
            {
                stream.WriteUInt8((byte)((int)Type | ((int)Flags << 3)));
            }
            else
            {
                stream.WriteUInt16LE((ushort)((int)Type | ((int)Flags << 4)));
            }

            stream.WriteUInt8((byte)SessionID);
            stream.WriteUInt16LE((ushort)PacketID);

            if (Type == PacketType.SynPacket || Type == PacketType.ConnectPacket)
            {
                if (ConnectionSignature != null)
                {
                    stream.WriteBytes(ConnectionSignature);
                }
            }

            if (Type == PacketType.DataPacket)
            {
                stream.WriteUInt8((byte)FragmentID);
            }

            if (HasFlag(PacketFlags.FlagHasSize))
            {
                stream.WriteUInt16LE((ushort)(Payload?.Length ?? 0));
            }

            if (Payload != null && Payload.Length > 0)
            {
                stream.WriteBytes(Payload);
            }

            uint checksum = CalculateChecksum(stream.Get());

            if (server.ChecksumVersion == 0)
            {
                stream.WriteUInt32LE(checksum);
            } 
            else 
            {
                stream.WriteUInt8((byte)checksum);
            }

            return stream.Get();
        }
    }

    public class PRUDPPacketV1 : PRUDPPacket
    {
        
    }

    public class PRUDPPacketLite : PRUDPPacket
    {
        
    }

    public class PRUDPClient
    {
        public PRUDPServer ?Server { get; set; }
    }

    public class PRUDPServer
    {
        public string? AccessKey { get; set; }
        public int FragmentSize { get; set; } = 1300;
        public int PRUDPVersion { get; set; } = 1;
        public Version? Version { get; set; }
        public int ChecksumVersion { get; set; } = 1;
        public string? KerberosPassword { get; set; }
        public int KerberosKeySize { get; set; } = 32;
        public int KerberosKeyDerivation { get; set; } = 0;
        public int KerberosTicketVersion { get; set; } = 1;
    }
}