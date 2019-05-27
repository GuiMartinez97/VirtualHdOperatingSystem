using System.Collections.Generic;
using VirtualHdOperatingSystem.Application.Interfaces;
using VirtualHdOperatingSystem.Console.Helpers;
using VirtualHdOperatingSystem.Console.Interfaces;

namespace VirtualHdOperatingSystem.Console.Controllers
{
    public class CdController : IController
    {
        public string FolderName { get; set; }

        public CdController(List<string> _parameters)
        {
            RequestHelper.ValidadeParametersNumber(1, _parameters);

            FolderName = _parameters[0];
        }

        public void Execute(IHdAppService _hdAppService)
        {
            if(FolderName != "..")
            {
                var block = _hdAppService.EnterFolder(ConsoleHelper.GetSelectedHd(), FolderName, ConsoleHelper.GetLastFileInStack().Block);
                ConsoleHelper.PushStack(block, FolderName);
            }
            else
            {
                ConsoleHelper.PopStack();
            }
        }
    }
}
