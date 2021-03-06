﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualHdOperatingSystem.Application.Interfaces;
using VirtualHdOperatingSystem.Console.Helpers;
using VirtualHdOperatingSystem.Console.Interfaces;

namespace VirtualHdOperatingSystem.Console.Controllers
{
    public class RemoveHdController : IController
    {
        public string HdName { get; set; }
        public RemoveHdController(List<string> _parameters)
        {
            RequestHelper.ValidadeParametersNumber(1, _parameters);

            HdName = _parameters[0];
        }
        public void Execute(IHdAppService _hdAppService)
        {
            _hdAppService.RemoveHd(HdName);
            ConsoleHelper.ClearFileFileStack();
            ConsoleHelper.ClearHd();
        }
    }
}
