using System;
using System.Collections.Generic;
using VirtualHdOperatingSystem.Application.Interfaces;
using VirtualHdOperatingSystem.Console.Helpers;
using VirtualHdOperatingSystem.Console.Interfaces;

namespace VirtualHdOperatingSystem.Console.Controllers
{
    public class MoveController : IController
    {
        public string FileToBeMoved { get; set; }
        public string Destiny { get; set; }

        public MoveController(List<string> _parameters)
        {
            FileToBeMoved = _parameters[0];
            Destiny = _parameters[1];
        }

        public void Execute(IHdAppService _hdAppService)
        {
            _hdAppService.Move(ConsoleHelper.GetSelectedHd(), ConsoleHelper.GetLastFileInStack().Block, FileToBeMoved, Destiny);
        }
    }
}
