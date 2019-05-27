using System.Collections.Generic;
using VirtualHdOperatingSystem.Application.Interfaces;
using VirtualHdOperatingSystem.Console.Helpers;
using VirtualHdOperatingSystem.Console.Interfaces;

namespace VirtualHdOperatingSystem.Console.Controllers
{
    public class CreateHdController : IController
    {
        public string HdName { get; set; }
        public int BlockNumber { get; set; }
        public int BlockSize { get; set; }

        public CreateHdController(List<string> _parameters)
        {
            RequestHelper.ValidadeParametersNumber(3, _parameters);

            HdName = _parameters[0];
            BlockNumber = RequestHelper.ValidadeParametersIntFormat(_parameters[1]);
            BlockSize = RequestHelper.ValidadeParametersIntFormat(_parameters[2]);
        }

        public void Execute(IHdAppService _hdAppService)
        {
            _hdAppService.CreateHd(HdName, BlockNumber, BlockSize);
        }
    }
}
