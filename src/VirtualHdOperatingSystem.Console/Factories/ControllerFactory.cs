using VirtualHdOperatingSystem.Console.Controllers;
using VirtualHdOperatingSystem.Console.Helpers;
using VirtualHdOperatingSystem.Console.Interfaces;
using VirtualHdOperatingSystem.Console.Models;

namespace VirtualHdOperatingSystem.Console.Factories
{
    public static class ControllerFactory
    {
        public static IController GetCommand(string  _input)
        {
            Request request = RequestHelper.Split(_input);

            switch (request.Root)
            {
                case "createhd":
                    return new CreateHdController(request.Parameters);
                case "selecthd":
                    return new SelectHdController(request.Parameters);
                case "createfolder":
                    return new CreateFolderController(request.Parameters);
                case "createfile":
                    return new CreateFileController(request.Parameters);
                case "typehd":
                    return new TypeHdController(request.Parameters);
                case "cd":
                    return new CdController(request.Parameters);
                case "tree":
                    return new TreeController(request.Parameters);
                case "cls":
                    return new ClsController(request.Parameters);
                case "dir":
                    return new DirController(request.Parameters);
                case "statushd":
                    return new StatusHdController(request.Parameters);
                case "help":
                    return new HelpController(request.Parameters);
                case "rename":
                    return new RenameController(request.Parameters);
                default:
                    return new InvalidRequestController();
            }
        }
    }
}
