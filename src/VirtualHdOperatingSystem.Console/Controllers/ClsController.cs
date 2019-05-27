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
    public class ClsController : IController
    {
        public ClsController(List<string> _parameters)
        {
            RequestHelper.ValidadeParametersNumber(0, _parameters);
        }

        public void Execute(IHdAppService _hdAppService)
        {
            System.Console.Clear();
        }
    }
}
