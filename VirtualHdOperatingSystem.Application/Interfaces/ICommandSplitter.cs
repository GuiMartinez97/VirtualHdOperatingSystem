using VirtualHdOperatingSystem.Application.Models;

namespace VirtualHdOperatingSystem.Application.Interfaces
{
    public interface ICommandSplitter
    {
        EnteredCommand Split(string _commandEntered);
    }
}
