using System;

namespace VirtualHdOperatingSystem.Application.Helpers
{
    public class Hd
    {
        public string HdName { get; set; }
        public byte[] Bytes { get; set; }

        public int BlockNumber { get; set; }
        public int BlockSize { get; set; }

        public Hd(string _hdName, byte[] _bytes)
        {
            HdName = _hdName;
            Bytes = _bytes;

            int j = 0;
            var blockNumberInByte = new byte[4];
            for (int i = 1; i < 5 ; i++, j++)
            {
                blockNumberInByte[j] = Bytes[i];
            }
            BlockNumber = BitConverter.ToInt32(blockNumberInByte, 0);

            j = 0;
            var blockSizeInByte = new byte[4];
            for (int i = 5; i < 9; i++, j++)
            {
                blockSizeInByte[j] = Bytes[i];
            }

            BlockSize = BitConverter.ToInt32(blockSizeInByte, 0);
        }
    }
}
