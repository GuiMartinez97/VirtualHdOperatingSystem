using VirtualHdOperatingSystem.Application.Flows;
using VirtualHdOperatingSystem.Application.Interfaces;
using VirtualHdOperatingSystem.Application.Models;

namespace VirtualHdOperatingSystem.Application.Factories
{
    public class CommandFactory
    {
        public ICommandFlow GetCommand(EnteredCommand command)
        {
            switch(command.Root)
            {
                case "write":
                    return new WriteDataInHdFlow(command.Parameters);
                case "typehd":
                    return new TypeHdFlow(command.Parameters);
                case "help":
                    return new HelpFlow(command.Parameters);
                case "createhd":
                    return new CreateHdFlow(command.Parameters);
                case "formathd":
                    return new FormatHdFlow(command.Parameters);
                case "dirhd":
                    return new DirHdFlow();
                case "selecthd":
                    return new SelectHdFlow(command.Parameters);
                case "createfile":
                    return new CreateFileFlow(command.Parameters);
                case "type":
                    return new TypeFlow(command.Parameters);
                case "mkdir":
                    return new CreateFileFlow(command.Parameters);
                default:
                    return new InvalidFlow(command.Parameters);
            }
        }
    }
}
