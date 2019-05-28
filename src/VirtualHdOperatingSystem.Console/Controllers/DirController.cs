using System;
using System.Collections.Generic;
using VirtualHdOperatingSystem.Application.Interfaces;
using VirtualHdOperatingSystem.Console.Helpers;
using VirtualHdOperatingSystem.Console.Interfaces;

namespace VirtualHdOperatingSystem.Console.Controllers
{
    public class DirController : IController
    {
        public DirController(List<string> _parameters)
        {
            RequestHelper.ValidadeParametersNumber(0, _parameters);
        }

        public void Execute(IHdAppService _hdAppService)
        {
            _hdAppService.Dir(ConsoleHelper.GetSelectedHd(), ConsoleHelper.GetLastFileInStack().Block);
        }
    }
}
