using System;

namespace nex
{
    public class RC4Encryption
    {
        private RC4Cipher rc4enc;
        private RC4Cipher rc4dec;

        public RC4Encryption(byte[] key)
        {
            rc4enc = new RC4Cipher(key);
            rc4dec = new RC4Cipher(key);
        }

        public void SetKey(byte[] key)
        {
            rc4enc = new RC4Cipher(key);
            rc4dec = new RC4Cipher(key);
        }

        public byte[] Encrypt(byte[] data)
        {
            return rc4enc.Transform(data);
        }

        public byte[] Decrypt(byte[] data)
        {
            return rc4dec.Transform(data);
        }
    }
}