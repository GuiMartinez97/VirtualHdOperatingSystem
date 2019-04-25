

using VirtualHdOperatingSystem.Application.Interfaces;
using System;
using System.Collections.Generic;

namespace VirtualHdOperatingSystem.Application.Flows
{
    public class InvalidFlow : ICommandFlow
    {
        public InvalidFlow(List<string> _parameters)
        {

        }
        public void Execute()
        {
            throw new Exception("Invalid command!");
        }
    }
}
