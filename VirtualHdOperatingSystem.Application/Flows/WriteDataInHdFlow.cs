using VirtualHdOperatingSystem.Application.Helpers;
using VirtualHdOperatingSystem.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace VirtualHdOperatingSystem.Application.Flows
{
    public class WriteDataInHdFlow : ICommandFlow
    {

        public string HdName { get; set; }
        public int Block { get; set; }
        public int Byte { get; set; }
        public string Message { get; set; }
        
        public WriteDataInHdFlow(List<string> _parameters)
        {
            if(_parameters.Count != 4)
            {
                throw new Exception("Invalid parameters number");
            }

            HdName = _parameters[0];

            try
            {
                Block = Convert.ToInt32(_parameters[1]);
                Byte = Convert.ToInt32(_parameters[2]);
            }
            catch
            {
                throw new Exception("Invalid parameter input format!");
            }

            Message = _parameters[3];
        }

        public void Execute()
        {
            byte[] binary;

            if (Message.StartsWith("b."))
            {
                binary = Convertions.BinaryStringRepresentationIntoBinary(Message);
            }
            else
            {
                binary = Encoding.UTF8.GetBytes(Message);
            }

            var bytesFromFile = File.ReadAllBytes(HdName);

            var index = (Block * Byte) - Byte;

            for (var i = 0; i < binary.Length; i++, index++)
            {
                bytesFromFile[index + Convert.ToInt32(Byte)] = binary[i];
            }

            File.WriteAllBytes(HdName, bytesFromFile);
        }
    }
}
