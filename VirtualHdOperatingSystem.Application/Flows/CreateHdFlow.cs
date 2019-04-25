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

                //bytes[0] = (byte)BlockNumber;
                //bytes[1] = (byte)BlockSize;

                var hdInfo = new HdInfo
                {
                    BlockNumber = BlockNumber,
                    BlockSize = BlockSize,
                    HdName = HdName
                };

                ConsolePathControl.AddHdToList(hdInfo);

                var BlockNumberInByte = BitConverter.GetBytes(BlockNumber);

                int j = 0;
                for(int i = 0; j< BlockNumberInByte.Length; i++, j++)
                {
                    bytes[i] = BlockNumberInByte[j];
                }

                var BlockSizeInByte = BitConverter.GetBytes(BlockSize);

                j = 0;
                for (int i = BlockSize/2; j < BlockSizeInByte.Length; i++, j++)
                {
                    bytes[i] = BlockSizeInByte[j];
                }

                bytes[BlockSize] = Convertions.BinaryStringRepresentationIntoBinary("b.00000001")[0];
                byte[] raizBytes = Encoding.ASCII.GetBytes("raiz");

                j = 0;
                for (var i = BlockSize + 8 ; j< raizBytes.Length ; i++, j++)
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
