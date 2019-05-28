using System;
using System.Collections.Generic;
using VirtualHdOperatingSystem.Application.Interfaces;
using VirtualHdOperatingSystem.Console.Helpers;
using VirtualHdOperatingSystem.Console.Interfaces;

namespace VirtualHdOperatingSystem.Console.Controllers
{
    public class CopyFromController : IController
    {
        public string ImageName { get; set; }
        public string NewImageName { get; set; }

        public CopyFromController(List<string> _parameters)
        {
            RequestHelper.ValidadeParametersNumber(2, _parameters);

            ImageName = _parameters[0];
            NewImageName = _parameters[1];
        }

        public void Execute(IHdAppService _hdAppService)
        {
            _hdAppService.CopyFrom(ConsoleHelper.GetSelectedHd(), ConsoleHelper.GetLastFileInStack().Block, ImageName, NewImageName);
        }
    }
}
