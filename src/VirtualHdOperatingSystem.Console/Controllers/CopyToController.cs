using System;
using System.Collections.Generic;
using VirtualHdOperatingSystem.Application.Interfaces;
using VirtualHdOperatingSystem.Console.Helpers;
using VirtualHdOperatingSystem.Console.Interfaces;

namespace VirtualHdOperatingSystem.Console.Controllers
{
    public class CopyToController : IController
    {
        public string NewImageName { get; set; }
        public string ImageName { get; set; }        

        public CopyToController(List<string> _parameters)
        {
            RequestHelper.ValidadeParametersNumber(2, _parameters);
            NewImageName = _parameters[0];
            ImageName = _parameters[1];

        }
        public void Execute(IHdAppService _hdAppService)
        {
            _hdAppService.CopyTo(ConsoleHelper.GetSelectedHd(), ConsoleHelper.GetLastFileInStack().Block, ImageName, NewImageName);
        }
    }
}
