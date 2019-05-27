
using VirtualHdOperatingSystem.Application.Interfaces;

namespace VirtualHdOperatingSystem.Console.Interfaces
{
    public interface IController
    {
        void Execute(IHdAppService _hdAppService);
    }
}
