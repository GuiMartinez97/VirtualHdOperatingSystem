using VirtualHdOperatingSystem.Application.Helpers;
using VirtualHdOperatingSystem.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;

namespace VirtualHdOperatingSystem.Application.Flows
{
    public class TypeHdFlow : ICommandFlow
    {
        public string HdName { get; set; }
        public HdInfo HdInfo { get; set; }
        public byte[] Hd { get; set; }

        public TypeHdFlow(List<string> _parameters)
        {
            if (_parameters.Count != 1)
            {
                throw new Exception("Invalid parameters number");
            }

            HdName = _parameters[0];

            Hd = File.ReadAllBytes($"storage/{HdName}");

            //HdInfo = new HdInfo
            //{
            //    BlockNumber = Convert.ToInt32(Hd[0]),
            //    BlockSize = Convert.ToInt32(Hd[1])
            //};

            HdInfo = ConsolePathControl.GetHdByName(HdName);
        }
        public void Execute()
        {
            var index = 0;
            for (var i = 0; i < HdInfo.BlockNumber; i++)
            {
                for (var j = 0; j < HdInfo.BlockSize; j++, index++)
                {
                    Console.Write(" " + Hd[index] + " ");
                }

                Console.WriteLine();
            }
        }
    }
}
