using System;

namespace nex
{
    public class DummyEncryption
    {
        private byte[] key;

        public DummyEncryption()
        {
            key = Array.Empty<byte>();
        }

        public byte[] Key()
        {
            return key;
        }

        public void SetKey(byte[] key)
        {
            this.key = key ?? Array.Empty<byte>();
        }

        public byte[] Encrypt(byte[] payload)
        {
            return payload;
        }

        public byte[] Decrypt(byte[] payload)
        {
            return payload;
        }

        public DummyEncryption Copy()
        {
            var copied = NewDummyEncryption();
            copied.SetKey(this.key);
            return copied;
        }

        public static DummyEncryption NewDummyEncryption()
        {
            return new DummyEncryption();
        }
    }
}