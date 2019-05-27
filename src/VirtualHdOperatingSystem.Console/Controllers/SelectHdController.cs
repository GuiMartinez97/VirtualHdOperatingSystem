using System.Collections.Generic;
using VirtualHdOperatingSystem.Application.Interfaces;
using VirtualHdOperatingSystem.Console.Helpers;
using VirtualHdOperatingSystem.Console.Interfaces;
using VirtualHdOperatingSystem.Domain.Entities;

namespace VirtualHdOperatingSystem.Console.Controllers
{
    public class SelectHdController : IController
    {
        public string HdName { get; set; }

        public SelectHdController(List<string> _parameters)
        {
            RequestHelper.ValidadeParametersNumber(1, _parameters);

            HdName = _parameters[0];
        }

        public void Execute(IHdAppService _hdAppService)
        {
            Hd hd = _hdAppService.SelectHd(HdName);
            ConsoleHelper.SetSelectedHd(hd);
        }
    }
}
