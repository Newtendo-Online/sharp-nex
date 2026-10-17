using System;

namespace nex
{
    public enum RMCMode : int
    {
        REQUEST = 0,
        RESPONSE = 1
    }

    public class RMCMessage
    {
        public RMCMode Mode { get; set; }
        public ushort ProtocolID { get; set; } 
        public uint CallID { get; set; }
        public uint MethodID { get; set; }
        public uint ErrorCode { get; set; }
        public byte[]? Body { get; set; }

        public void Decode(byte[] data)
        {
            var stream = new StreamIn(data);

            uint length = stream.ReadUInt32LE();
            if (stream.Remaining() != (int)length)
            {
                throw new Exception("RMC Message has unexpected size");
            }

            byte protocolID = stream.ReadUInt8();
            ProtocolID = (ushort)(protocolID & ~0x80);

            if (ProtocolID == 0x7F)
            {
                ProtocolID = stream.ReadUInt16LE();
            }

            if ((protocolID & 0x80) != 0)
            {
                Mode = RMCMode.REQUEST;
                CallID = stream.ReadUInt32LE();
                MethodID = stream.ReadUInt32LE();

                if (stream.Remaining() > 0)
                {
                    Body = stream.ReadBytesNext(stream.Remaining());
                }
                else
                {
                    Body = Array.Empty<byte>();
                }
            }
            else
            {
                Mode = RMCMode.RESPONSE;
                bool isSuccess = stream.ReadBool();

                if (isSuccess)
                {
                    CallID = stream.ReadUInt32LE();
                    MethodID = stream.ReadUInt32LE();
                    MethodID = MethodID & ~((uint)0x8000);

                    if (stream.Remaining() > 0)
                    {
                        Body = stream.ReadBytesNext(stream.Remaining());
                    }
                    else
                    {
                        Body = Array.Empty<byte>();
                    }
                }
                else
                {
                    ErrorCode = stream.ReadUInt32LE();
                    CallID = stream.ReadUInt32LE();
                }
            }
        }

        public byte[] Encode()
        {
            var stream = new StreamOut();

            if (Mode == RMCMode.REQUEST)
            {
                stream.WriteUInt8((byte)(ProtocolID | 0x80));
                stream.WriteUInt32LE(CallID);
                stream.WriteUInt32LE(MethodID);
                if (Body != null && Body.Length > 0)
                {
                    stream.WriteBytes(Body);
                }
            }
            else
            {
                stream.WriteUInt8((byte)ProtocolID);

                bool isError = ErrorCode != 0 && (ErrorCode & 0x80000000) != 0;

                if (isError)
                {
                    stream.WriteBool(false);
                    stream.WriteUInt32LE(ErrorCode);
                    stream.WriteUInt32LE(CallID);
                }
                else
                {
                    stream.WriteBool(true);
                    stream.WriteUInt32LE(CallID);
                    stream.WriteUInt32LE(MethodID | 0x8000);
                    if (Body != null && Body.Length > 0)
                    {
                        stream.WriteBytes(Body);
                    }
                }
            }

            byte[] serialized = stream.Get();
            var message = new StreamOut();
            message.WriteUInt32LE((uint)serialized.Length);
            message.WriteBytes(serialized);

            return message.Get();
        }
    }
}