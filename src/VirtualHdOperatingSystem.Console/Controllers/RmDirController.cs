using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualHdOperatingSystem.Application.Interfaces;
using VirtualHdOperatingSystem.Console.Helpers;
using VirtualHdOperatingSystem.Console.Interfaces;

namespace VirtualHdOperatingSystem.Console.Controllers
{
    public class RmDirController : IController
    {
        public string FolderToBeRemoved { get; set; }
        public RmDirController(List<string> _parameters)
        {
            RequestHelper.ValidadeParametersNumber(1, _parameters);

            FolderToBeRemoved = _parameters[0];
        }

        public void Execute(IHdAppService _hdAppService)
        {
            _hdAppService.RmDir(ConsoleHelper.GetSelectedHd(), ConsoleHelper.GetLastFileInStack().Block, FolderToBeRemoved);
        }
    }
}
