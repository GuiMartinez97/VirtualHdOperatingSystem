using System;
using System.Collections.Generic;
using VirtualHdOperatingSystem.Application.Interfaces;
using VirtualHdOperatingSystem.Console.Helpers;
using VirtualHdOperatingSystem.Console.Interfaces;

namespace VirtualHdOperatingSystem.Console.Controllers
{
    public class RenameController : IController
    {
        public string FileName { get; set; }
        public string NewName { get; set; }

        public RenameController(List<string> _parameters)
        {
            RequestHelper.ValidadeParametersNumber(2, _parameters);

            FileName = _parameters[0];
            NewName = _parameters[1];
        }

        public void Execute(IHdAppService _hdAppService)
        {
            _hdAppService.Rename(ConsoleHelper.GetSelectedHd(), ConsoleHelper.GetLastFileInStack().Block, FileName, NewName);
        }
    }
}
