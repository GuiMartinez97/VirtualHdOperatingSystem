using VirtualHdOperatingSystem.Application.Helpers;
using VirtualHdOperatingSystem.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;

namespace VirtualHdOperatingSystem.Application.Flows
{
    public class FormatHdFlow : ICommandFlow
    {
        public string HdName { get; set; }

        public FormatHdFlow(List<string> _parameters)
        {
            if (_parameters.Count != 1)
            {
                throw new Exception("Invalid parameters number!");
            }

            HdName = _parameters[0];
        }

        public void Execute()
        {
            var bytesFromFile = File.ReadAllBytes($"storage/{HdName}");

            var binary = Convertions.BinaryStringRepresentationIntoBinary("b.00000000")[0];

            for (var i = 2; i < bytesFromFile.Length; i++)
            {
                bytesFromFile[i] = binary;
            }

            File.WriteAllBytes(HdName, bytesFromFile);
        }
    }
}
