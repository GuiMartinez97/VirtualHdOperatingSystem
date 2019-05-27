using System;
using VirtualHdOperatingSystem.Application.Interfaces;
using VirtualHdOperatingSystem.Console.Interfaces;

namespace VirtualHdOperatingSystem.Console.Controllers
{
    public class InvalidRequestController : IController
    {
        public void Execute(IHdAppService _hdAppService)
        {
            throw new Exception("Invalid command!");
        }
    }
}
