using System;
using System.Collections.Generic;
using VirtualHdOperatingSystem.Application.Interfaces;
using VirtualHdOperatingSystem.Console.Helpers;
using VirtualHdOperatingSystem.Console.Interfaces;

namespace VirtualHdOperatingSystem.Console.Controllers
{
    public class CreateFolderController : IController
    {
        public string FolderName { get; set; }

        public CreateFolderController(List<string> _parameters)
        {
            RequestHelper.ValidadeParametersNumber(1, _parameters);

            FolderName = _parameters[0];
        }

        public void Execute(IHdAppService _hdAppService)
        {
            _hdAppService.CreateFolder(ConsoleHelper.GetSelectedHd(), FolderName, ConsoleHelper.GetLastFileInStack().Block);
        }
    }
}
