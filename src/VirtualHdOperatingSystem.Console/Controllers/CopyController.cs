using System;
using System.Collections.Generic;
using VirtualHdOperatingSystem.Application.Interfaces;
using VirtualHdOperatingSystem.Console.Helpers;
using VirtualHdOperatingSystem.Console.Interfaces;

namespace VirtualHdOperatingSystem.Console.Controllers
{
    public class CopyController : IController
    {
        public string FileToBeCopied { get; set; }
        public string Destiny { get; set; }

        public CopyController(List<string> _parameters)
        {
            FileToBeCopied = _parameters[0];
            Destiny = _parameters[1];
        }
        public void Execute(IHdAppService _hdAppService)
        {
            _hdAppService.Copy(ConsoleHelper.GetSelectedHd(), ConsoleHelper.GetLastFileInStack().Block, FileToBeCopied, Destiny);
        }
    }
}
