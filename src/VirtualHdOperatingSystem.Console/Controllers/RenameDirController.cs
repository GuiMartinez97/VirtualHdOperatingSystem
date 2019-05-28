using System.Collections.Generic;
using VirtualHdOperatingSystem.Application.Interfaces;
using VirtualHdOperatingSystem.Console.Helpers;
using VirtualHdOperatingSystem.Console.Interfaces;

namespace VirtualHdOperatingSystem.Console.Controllers
{
    public class RenameDirController : IController
    {
        public string FolderName { get; set; }
        public string NewName { get; set; }

        public RenameDirController(List<string> _parameters)
        {
            RequestHelper.ValidadeParametersNumber(2, _parameters);

            FolderName = _parameters[0];
            NewName = _parameters[1];
        }

        public void Execute(IHdAppService _hdAppService)
        {
            _hdAppService.Rename(ConsoleHelper.GetSelectedHd(), ConsoleHelper.GetLastFileInStack().Block, FolderName, NewName);
        }
    }
}
