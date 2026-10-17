using System;
using System.IO;
using System.IO.Compression;

namespace nex
{
    public class ZlibCompression
    {
        public byte[] Compress(byte[] data)
        {
            byte[] compressed;
            using (var memoryStream = new MemoryStream())
            {
                using (var zlibStream = new ZLibStream(memoryStream, CompressionMode.Compress))
                {
                    zlibStream.Write(data, 0, data.Length);
                }
                compressed = memoryStream.ToArray();
            }

            int ratio = (data.Length / compressed.Length) + 1;

            byte[] result = new byte[1 + compressed.Length];
            result[0] = (byte)ratio;
            Array.Copy(compressed, 0, result, 1, compressed.Length);

            return result;
        }

        public byte[] Decompress(byte[] data)
        {
            if (data == null || data.Length == 0)
            {
                throw new ArgumentException("Data cannot be empty.");
            }

            byte expectedRatio = data[0];
            if (expectedRatio == 0)
            {
                return data[1..];
            }

            byte[] compressedData = data[1..];

            byte[] decompressed;
            using (var inputStream = new MemoryStream(compressedData))
            using (var zlibStream = new ZLibStream(inputStream, CompressionMode.Decompress))
            using (var outputStream = new MemoryStream())
            {
                zlibStream.CopyTo(outputStream);
                decompressed = outputStream.ToArray();
            }

            int ratio = (decompressed.Length / compressedData.Length) + 1;
            if (ratio != expectedRatio)
            {
                throw new Exception($"Unexpected compression ratio (expected {ratio}, got {expectedRatio})");
            }

            return decompressed;
        }
    }
}