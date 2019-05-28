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
    public class HelpController : IController
    {
        public string Command { get; set; }
        public HelpController(List<string> _parameters)
        {
            RequestHelper.ValidadeParametersNumber(1, _parameters);

            Command = _parameters[0];
        }

        public void Execute(IHdAppService _hdAppService)
        {
            //Jão
            switch (Command)
            {
                case "cls":
                    System.Console.WriteLine("Limpa a tela.");
                    break;
                case "createfolder":
                    System.Console.WriteLine("Cria um diretorio...");
                    break;
                default:
                    System.Console.WriteLine("Comando não encontrado!");
                    break;
            }
        }
    }
}
