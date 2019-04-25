using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualHdOperatingSystem.Application.Helpers
{
    public static class Convertions
    {
        public static byte[] BinaryStringRepresentationIntoBinary(string _binaryStringRepresentation)
        {
            byte[] binary;

            _binaryStringRepresentation = _binaryStringRepresentation.Split('.')[1];

            int numOfBytes = _binaryStringRepresentation.Length / 8;
            binary = new byte[numOfBytes];
            for (int i = 0; i < numOfBytes; ++i)
            {
                binary[i] = Convert.ToByte(_binaryStringRepresentation.Substring(8 * i, 8), 2);
            }

            return binary;
        }
    }
}
