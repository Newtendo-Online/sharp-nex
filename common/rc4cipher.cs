using System;

namespace nex
{
    public class RC4Cipher
    {
        private readonly byte[] s = new byte[256];
        private int x, y;

        public RC4Cipher(ReadOnlySpan<byte> key)
        {
            for (int i = 0; i < 256; i++) s[i] = (byte)i;
            int j = 0;
            for (int i = 0; i < 256; i++)
            {
                j = (j + s[i] + key[i % key.Length]) & 255;
                (s[i], s[j]) = (s[j], s[i]);
            }
        }

        public void ProcessBytes(Span<byte> data)
        {
            for (int p = 0; p < data.Length; p++)
            {
                x = (x + 1) & 255;
                y = (y + s[x]) & 255;
                (s[x], s[y]) = (s[y], s[x]);
                int k = s[(s[x] + s[y]) & 255];
                data[p] ^= (byte)k;
            }
        }

        public byte[] Transform(ReadOnlySpan<byte> data)
        {
            byte[] result = data.ToArray();
            ProcessBytes(result);
            return result;
        }
    }
}