using System;
using System.Collections.Generic;
using VirtualHdOperatingSystem.Application.Interfaces;
using VirtualHdOperatingSystem.Console.Helpers;
using VirtualHdOperatingSystem.Console.Interfaces;

namespace VirtualHdOperatingSystem.Console.Controllers
{
    public class CreateFileController : IController
    {
        public string FileName { get; set; }    
        public string FileContent { get; set; }

        public CreateFileController(List<string> _parameters)
        {
            RequestHelper.ValidadeParametersNumber(2, _parameters);

            FileName = _parameters[0];
            FileContent = _parameters[1];
        }

        public void Execute(IHdAppService _hdAppService)
        {
            _hdAppService.CreateFile(ConsoleHelper.GetSelectedHd(), FileName, ConsoleHelper.GetLastFileInStack().Block, FileContent);
        }
    }
}
