using VirtualHdOperatingSystem.Application.Helpers;
using VirtualHdOperatingSystem.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace VirtualHdOperatingSystem.Application.Flows
{
    public class SelectHdFlow : ICommandFlow
    {
        public string HdName { get; set; }

        public SelectHdFlow(List<string> _parameters)
        {
            if (_parameters.Count != 1)
            {
                throw new Exception("Invalid parameters number!");
            }

            HdName = _parameters[0];
        }

        public void Execute()
        {
            if (File.Exists($"storage/{HdName}"))
            {
                var bytesFromFile = File.ReadAllBytes($"storage/{HdName}");

                ConsolePathControl.SetSelectedHd(new Hd(HdName, bytesFromFile));
            }
            else
            {
                throw new Exception("Hd não encontrado!");
            }            
        }
    }
}
