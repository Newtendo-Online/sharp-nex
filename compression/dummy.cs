using System;

namespace nex
{
    public class DummyCompression
    {
       public static byte[] Compress(byte[] data) 
       {
          return data;
       }

       public static byte[] Decompress(byte[] data)
       {
          return data;
       }
    }
}