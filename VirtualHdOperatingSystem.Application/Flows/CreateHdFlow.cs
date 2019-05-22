using VirtualHdOperatingSystem.Application.Helpers;
using VirtualHdOperatingSystem.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace VirtualHdOperatingSystem.Application.Flows
{
    public class CreateHdFlow : ICommandFlow
    {

        public string HdName { get; set; }
        public int BlockNumber { get; set; }
        public int BlockSize { get; set; }

        public CreateHdFlow(List<string> _parameters)
        {
            if(_parameters.Count != 3)
            {
                throw new Exception("Invalid parameters number!");
            }

            HdName = _parameters[0];

            try
            {
                BlockNumber = Convert.ToInt32(_parameters[1]);
                BlockSize = Convert.ToInt32(_parameters[2]);
            }
            catch
            {
                throw new Exception("Invalid parameter input format!");
            }
        }

        public void Execute()
        {
            try
            {
                var bytes = new byte[BlockNumber * BlockSize];

                var blockNumberInByte = BitConverter.GetBytes(BlockNumber);
                var blockSizeInByte= BitConverter.GetBytes(BlockSize);

                if(blockNumberInByte.Length > 4 || blockSizeInByte.Length > 4)
                {
                    throw new Exception("Invalid parameter input format!");
                }

                bytes[0] = (byte)1;
                int j = 0;
                for(int i = 1; j< blockNumberInByte.Length; i++, j++)
                {
                    bytes[i] = blockNumberInByte[j];
                }

                j = 0;
                for (int i = 5; j < blockSizeInByte.Length; i++, j++)
                {
                    bytes[i] = blockSizeInByte[j];
                }

                bytes[BlockSize] = Convertions.BinaryStringRepresentationIntoBinary("b.00000001")[0];
                byte[] raizBytes = Encoding.ASCII.GetBytes("raiz");

                j = 0;
                for (var i = BlockSize + 9 ; j< raizBytes.Length ; i++, j++)
                {
                    bytes[i] = raizBytes[j];
                }


                File.WriteAllBytes($"storage/{HdName}", bytes);
            }
            catch(Exception ex)
            {
                throw new Exception($"Something went wrong: {ex.Message}");
            }            
        }
    }
}
