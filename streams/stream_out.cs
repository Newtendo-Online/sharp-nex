// TODO - Rewrite StreamOut cleanly

using System;
using System.Buffers.Binary;
using System.IO;
using System.Text;

namespace nex
{
    public class StreamOut : IDisposable
    {
        private readonly MemoryStream _stream;

        public PRUDPServer Server { get; set; } = null!;

        public StreamOut()
        {
            _stream = new MemoryStream();
            Server = new PRUDPServer();
        }

        public void WriteBytes(byte[] data) => _stream.Write(data, 0, data.Length);

        public void WriteByte(byte value) => _stream.WriteByte(value);

        public void WriteUInt8(byte value) => _stream.WriteByte(value);

        public void WriteUInt16(ushort value)
        {
            Span<byte> buf = stackalloc byte[2];
            BinaryPrimitives.WriteUInt16BigEndian(buf, value);
            _stream.Write(buf);
        }

        public void WriteUInt32(uint value)
        {
            Span<byte> buf = stackalloc byte[4];
            BinaryPrimitives.WriteUInt32BigEndian(buf, value);
            _stream.Write(buf);
        }

        public void WriteUInt32LittleEndian(uint value)
        {
            Span<byte> buf = stackalloc byte[4];
            BinaryPrimitives.WriteUInt32LittleEndian(buf, value);
            _stream.Write(buf);
        }

        public void WriteUInt64(ulong value)
        {
            Span<byte> buf = stackalloc byte[8];
            BinaryPrimitives.WriteUInt64BigEndian(buf, value);
            _stream.Write(buf);
        }

        public void WriteUInt64LittleEndian(ulong value)
        {
            Span<byte> buf = stackalloc byte[8];
            BinaryPrimitives.WriteUInt64LittleEndian(buf, value);
            _stream.Write(buf);
        }

        public void WritePid(uint pid) => WriteUInt32(pid);

        public void WriteBuffer(byte[] buffer)
        {
            WriteUInt32((uint)buffer.Length);
            _stream.Write(buffer, 0, buffer.Length);
        }

        public void WriteString(string str)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(str + "\0");
            WriteUInt16((ushort)bytes.Length);
            _stream.Write(bytes, 0, bytes.Length);
        }

        public void WriteDateTime(DateTime dt)
        {
            WriteUInt64((ulong)dt.ToFileTime());
        }

        public byte[] Get() => _stream.ToArray();

        public void Dispose() => _stream.Dispose();

        public void WriteBool(bool value) => _stream.WriteByte(value ? (byte)1 : (byte)0);

        public void WriteInt8(sbyte value) => _stream.WriteByte((byte)value);

        public void WriteUInt16LE(ushort value)
        {
            Span<byte> buf = stackalloc byte[2];
            BinaryPrimitives.WriteUInt16LittleEndian(buf, value);
            _stream.Write(buf);
        }

        public void WriteUInt24LE(uint value)
        {
            if (value > 0xFFFFFF) throw new ArgumentOutOfRangeException(nameof(value), "Value does not fit in 24 bits.");
            Span<byte> buf = stackalloc byte[3];
            buf[0] = (byte)value;
            buf[1] = (byte)(value >> 8);
            buf[2] = (byte)(value >> 16);
            _stream.Write(buf);
        }

        public void WriteUInt32LE(uint value) => WriteUInt32LittleEndian(value);

        public void WriteUInt64LE(ulong value) => WriteUInt64LittleEndian(value);

        public void WriteInt16LE(short value)
        {
            Span<byte> buf = stackalloc byte[2];
            BinaryPrimitives.WriteInt16LittleEndian(buf, value);
            _stream.Write(buf);
        }

        public void WriteInt32LE(int value)
        {
            Span<byte> buf = stackalloc byte[4];
            BinaryPrimitives.WriteInt32LittleEndian(buf, value);
            _stream.Write(buf);
        }

        public void WriteInt64LE(long value)
        {
            Span<byte> buf = stackalloc byte[8];
            BinaryPrimitives.WriteInt64LittleEndian(buf, value);
            _stream.Write(buf);
        }

        public void WriteFloat32LE(float value)
        {
            Span<byte> buf = stackalloc byte[4];
            BinaryPrimitives.WriteSingleLittleEndian(buf, value);
            _stream.Write(buf);
        }

        public void WriteFloat64LE(double value)
        {
            Span<byte> buf = stackalloc byte[8];
            BinaryPrimitives.WriteDoubleLittleEndian(buf, value);
            _stream.Write(buf);
        }

        public void WriteUInt16LittleEndian(ushort value) => WriteUInt16LE(value);
    }
}