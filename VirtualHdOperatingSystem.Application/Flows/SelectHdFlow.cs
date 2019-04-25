using VirtualHdOperatingSystem.Application.Helpers;
using VirtualHdOperatingSystem.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            ConsolePathControl.SetSelectedHd(HdName);
        }
    }
}
