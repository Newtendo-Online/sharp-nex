// TODO - Rewrite StreamIn cleanly

using System;
using System.Buffers.Binary;
using System.IO;
using System.Text;

namespace nex
{
    public class StreamIn : IDisposable
    {
        private readonly byte[] _buffer;
        private int _position;

        public int Position => _position;

        public StreamIn(byte[] data)
        {
            _buffer = data;
            _position = 0;
        }

        public byte[] ReadBytes(int count)
        {
            if (_position + count > _buffer.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(count), "Attempted to read past the end of the stream.");
            }
            var slice = _buffer.AsSpan(_position, count);
            _position += count;
            return slice.ToArray();
        }

        public byte ReadByte()
        {
            if (_position >= _buffer.Length) throw new EndOfStreamException();
            return _buffer[_position++];
        }

        public byte ReadUInt8()
        {
            if (_position >= _buffer.Length) throw new EndOfStreamException();
            return _buffer[_position++];
        }

        public ushort ReadUInt16()
        {
            var val = BinaryPrimitives.ReadUInt16BigEndian(_buffer.AsSpan(_position, 2));
            _position += 2;
            return val;
        }

        public uint ReadUInt32()
        {
            var val = BinaryPrimitives.ReadUInt32BigEndian(_buffer.AsSpan(_position, 4));
            _position += 4;
            return val;
        }

        public uint ReadUInt32LittleEndian()
        {
            var val = BinaryPrimitives.ReadUInt32LittleEndian(_buffer.AsSpan(_position, 4));
            _position += 4;
            return val;
        }

        public ulong ReadUInt64()
        {
            var val = BinaryPrimitives.ReadUInt64BigEndian(_buffer.AsSpan(_position, 8));
            _position += 8;
            return val;
        }

        public uint ReadPid() => ReadUInt32();

        public byte[] ReadBuffer()
        {
            if (_position + 4 > _buffer.Length) return Array.Empty<byte>();
            int length = (int)ReadUInt32();
            return ReadBytes(length);
        }

        public string ReadString()
        {
            ushort length = ReadUInt16();
            if (length == 0) return string.Empty;
            byte[] bytes = ReadBytes(length);
            string str = Encoding.UTF8.GetString(bytes);
            if (str.EndsWith('\0')) str = str.Substring(0, str.Length - 1);
            return str;
        }

        public StreamIn ReadSubstream()
        {
            byte[] subBuffer = ReadBuffer();
            return new StreamIn(subBuffer);
        }

        public void SeekByte(int offset, bool relative = false)
        {
            if (relative)
            {
                _position += offset;
            }
            else
            {
                _position = offset;
            }
        }

        public bool Eof() => _position >= _buffer.Length;
        public int Size() => _buffer.Length;
        public int Tell() => _position;

        public DateTime ReadDateTime()
        {
            if (_position + 8 > _buffer.Length) return DateTime.MinValue;
            ulong val = ReadUInt64();
            try 
            { 
                return DateTime.FromFileTime((long)val); 
            } 
            catch 
            { 
                return DateTime.MinValue; 
            }
        }

        public void Dispose() { }

        private void Need(int count)
        {
            if (count < 0 || _position + count > _buffer.Length)
                throw new EndOfStreamException("Attempted to read past the end of the stream.");
        }

        public int Remaining() => _buffer.Length - _position;
        public int ByteOffset() => _position;
        public byte[] ReadBytesNext(long count) => ReadBytes((int)count);
        public byte[] Bytes() => _buffer;

        public bool ReadBool() => ReadUInt8() != 0;

        public sbyte ReadInt8() => (sbyte)ReadUInt8();

        public ushort ReadUInt16LE()
        {
            Need(2);
            var val = BinaryPrimitives.ReadUInt16LittleEndian(_buffer.AsSpan(_position, 2));
            _position += 2;
            return val;
        }

        public uint ReadUInt24LE()
        {
            Need(3);
            uint val = (uint)(_buffer[_position] | (_buffer[_position + 1] << 8) | (_buffer[_position + 2] << 16));
            _position += 3;
            return val;
        }

        public uint ReadUInt32LE()
        {
            Need(4);
            var val = BinaryPrimitives.ReadUInt32LittleEndian(_buffer.AsSpan(_position, 4));
            _position += 4;
            return val;
        }

        public ulong ReadUInt64LE()
        {
            Need(8);
            var val = BinaryPrimitives.ReadUInt64LittleEndian(_buffer.AsSpan(_position, 8));
            _position += 8;
            return val;
        }

        public short ReadInt16LE()
        {
            Need(2);
            var val = BinaryPrimitives.ReadInt16LittleEndian(_buffer.AsSpan(_position, 2));
            _position += 2;
            return val;
        }

        public int ReadInt32LE()
        {
            Need(4);
            var val = BinaryPrimitives.ReadInt32LittleEndian(_buffer.AsSpan(_position, 4));
            _position += 4;
            return val;
        }

        public long ReadInt64LE()
        {
            Need(8);
            var val = BinaryPrimitives.ReadInt64LittleEndian(_buffer.AsSpan(_position, 8));
            _position += 8;
            return val;
        }

        public float ReadFloat32LE()
        {
            Need(4);
            var val = BinaryPrimitives.ReadSingleLittleEndian(_buffer.AsSpan(_position, 4));
            _position += 4;
            return val;
        }

        public double ReadFloat64LE()
        {
            Need(8);
            var val = BinaryPrimitives.ReadDoubleLittleEndian(_buffer.AsSpan(_position, 8));
            _position += 8;
            return val;
        }

        public ushort ReadUInt16LittleEndian() => ReadUInt16LE();
        public ulong ReadUInt64LittleEndian() => ReadUInt64LE();
    }
}