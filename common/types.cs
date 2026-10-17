using System;
using System.Collections.Generic;

namespace nex
{
    public abstract class Structure
    {
        public abstract void Decode(StreamIn stream, int version);
        public abstract void Encode(StreamOut stream, int version);
    }

    public class Data : Structure
    {
        public override void Decode(StreamIn stream, int version)
        {
            stream.SeekByte(0, true);
        }

        public override void Encode(StreamOut stream, int version)
        {
            // Save
        }
    }

    public class DataHolder
    {
        private static readonly Dictionary<string, Func<Structure>> ObjectMap = new();

        public string TypeName { get; set; } = string.Empty;
        public uint Length1 { get; private set; }
        public uint Length2 { get; private set; }
        public Structure ObjectData { get; set; } = new Data();

        public static void Register<T>(string name) where T : Structure, new()
        {
            ObjectMap[name] = () => new T();
        }

        public void Decode(StreamIn stream)
        {
            TypeName = stream.ReadString();
            Length1 = stream.ReadUInt32();
            Length2 = stream.ReadUInt32();

            if (ObjectMap.TryGetValue(TypeName, out var factory))
            {
                ObjectData = factory();
                ObjectData.Decode(stream, 0);
            }
            else
            {
                throw new KeyNotFoundException($"Unknown DataHolder type: {TypeName}");
            }
        }

        public void Encode(StreamOut stream)
        {
            var substream = new StreamOut();
            ObjectData.Encode(substream, 0);
            byte[] content = substream.Get();

            stream.WriteString(TypeName);
            stream.WriteUInt32((uint)(content.Length + 4));
            stream.WriteBuffer(content);
        }
    }

    public class RVConnectionData : Structure
    {
        public string StationURL { get; set; } = string.Empty;
        public byte[] SpecialProtocols { get; set; } = [];
        public string StationURLSpecialProtocols { get; set; } = string.Empty;
        public ulong Time { get; set; }

        public override void Decode(StreamIn stream, int version)
        {
            StationURL = stream.ReadString();
            _ = stream.ReadUInt32(); // Always 0
            StationURLSpecialProtocols = stream.ReadString();
            Time = stream.ReadUInt64();
        }

        public override void Encode(StreamOut stream, int version)
        {
            stream.WriteString(StationURL);
            stream.WriteUInt32(0); // Always 0
            stream.WriteString(StationURLSpecialProtocols);
            stream.WriteUInt64(Time);
        }
    }

    public class NexDateTime
    {
        private readonly ulong _value;

        public NexDateTime(ulong value = 0)
        {
            _value = value;
        }

        public ulong Value => _value;

        public static ulong Make(int year, int month, int day, int hour, int minute, int second)
        {
            return (ulong)(second | (minute << 6) | (hour << 12) | (day << 17) | (month << 22) | (year << 26));
        }

        public static NexDateTime FromTimestamp(DateTimeOffset timestamp)
        {
            ulong val = Make(timestamp.Year, timestamp.Month, timestamp.Day, timestamp.Hour, timestamp.Minute, timestamp.Second);
            return new NexDateTime(val);
        }

        public static NexDateTime Now() => FromTimestamp(DateTimeOffset.UtcNow);
    }

    public class StationURL
    {
        public string Scheme { get; set; } = string.Empty;
        public string AddressVal { get; set; } = string.Empty;
        public string PortVal { get; set; } = string.Empty;
        public string StreamVal { get; set; } = string.Empty;
        public string SidVal { get; set; } = string.Empty;
        public string CidVal { get; set; } = string.Empty;
        public string PidVal { get; set; } = string.Empty;
        public string TransportType { get; set; } = string.Empty;
        public string RvcidVal { get; set; } = string.Empty;
        public string NatmVal { get; set; } = string.Empty;
        public string NatfVal { get; set; } = string.Empty;
        public string UpnpVal { get; set; } = string.Empty;
        public string PmpVal { get; set; } = string.Empty;
        public string ProbeInitVal { get; set; } = string.Empty;
        public string PridVal { get; set; } = string.Empty;

        public void FromString(string str)
        {
            var split = str.Split(":/", 2);
            if (split.Length < 2) return;

            Scheme = split[0];
            var fields = split[1].TrimStart('/');
            var paramsList = fields.Split(';');

            foreach (var param in paramsList)
            {
                var kv = param.Split('=', 2);
                if (kv.Length < 2) continue;

                string name = kv[0];
                string value = kv[1];

                switch (name)
                {
                    case "address": AddressVal = value; break;
                    case "port": PortVal = value; break;
                    case "stream": StreamVal = value; break;
                    case "sid": SidVal = value; break;
                    case "CID": CidVal = value; break;
                    case "PID": PidVal = value; break;
                    case "type": TransportType = value; break;
                    case "RVCID": RvcidVal = value; break;
                    case "natm": NatmVal = value; break;
                    case "natf": NatfVal = value; break;
                    case "upnp": UpnpVal = value; break;
                    case "pmp": PmpVal = value; break;
                    case "probeinit": ProbeInitVal = value; break;
                    case "PRID": PridVal = value; break;
                }
            }
        }

        public string EncodeToString()
        {
            var fields = new List<string>();

            if (!string.IsNullOrEmpty(AddressVal)) fields.Add("address=" + AddressVal);
            if (!string.IsNullOrEmpty(PortVal)) fields.Add("port=" + PortVal);
            if (!string.IsNullOrEmpty(StreamVal)) fields.Add("stream=" + StreamVal);
            if (!string.IsNullOrEmpty(SidVal)) fields.Add("sid=" + SidVal);
            if (!string.IsNullOrEmpty(CidVal)) fields.Add("CID=" + CidVal);
            if (!string.IsNullOrEmpty(PidVal)) fields.Add("PID=" + PidVal);
            if (!string.IsNullOrEmpty(TransportType)) fields.Add("type=" + TransportType);
            if (!string.IsNullOrEmpty(RvcidVal)) fields.Add("RVCID=" + RvcidVal);
            if (!string.IsNullOrEmpty(NatmVal)) fields.Add("natm=" + NatmVal);
            if (!string.IsNullOrEmpty(NatfVal)) fields.Add("natf=" + NatfVal);
            if (!string.IsNullOrEmpty(UpnpVal)) fields.Add("upnp=" + UpnpVal);
            if (!string.IsNullOrEmpty(PmpVal)) fields.Add("pmp=" + PmpVal);
            if (!string.IsNullOrEmpty(ProbeInitVal)) fields.Add("probeinit=" + ProbeInitVal);
            if (!string.IsNullOrEmpty(PridVal)) fields.Add("PRID=" + PridVal);

            return Scheme + "://" + string.Join(";", fields);
        }

        public static StationURL Parse(string str)
        {
            var station = new StationURL();
            if (!string.IsNullOrEmpty(str))
            {
                station.FromString(str);
            }
            return station;
        }
    }

    public class Result
    {
        private uint _code;

        public Result(uint code)
        {
            _code = code;
        }

        public bool IsSuccess() => (_code & Constants.ERROR_MASK) == 0;
        public bool IsError() => (_code & Constants.ERROR_MASK) != 0;

        public void Load(StreamIn stream)
        {
            _code = stream.ReadUInt32();
        }

        public void Save(StreamOut stream)
        {
            stream.WriteUInt32(_code);
        }

        public uint Code => _code;

        public static Result Success(uint code) => new(code & ~Constants.ERROR_MASK);
        public static Result Error(uint code) => new(code | Constants.ERROR_MASK);
    }

    public class ResultRange : Structure
    {
        public uint Offset { get; set; }
        public uint Length { get; set; }
        
        public uint Size 
        { 
            get => Length; 
            set => Length = value; 
        }

        public override void Decode(StreamIn stream, int version)
        {
            Offset = stream.ReadUInt32();
            Length = stream.ReadUInt32();
        }

        public override void Encode(StreamOut stream, int version)
        {
            stream.WriteUInt32(Offset);
            stream.WriteUInt32(Length);
        }
    }
}