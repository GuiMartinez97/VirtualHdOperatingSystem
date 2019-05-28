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
                case "createhd":
                    System.Console.WriteLine("Cria um HD...");
                    break;
                case "selecthd":
                    System.Console.WriteLine("Seleciona um HD...");
                    break;
                case "createfile":
                    System.Console.WriteLine("Cria um Arquivo...");
                    break;
                case "typehd":
                    System.Console.WriteLine("Exibe Conteudo do HD...");
                    break;
                case "cd":
                    System.Console.WriteLine("Alterna entre pastas...");
                    break;
                case "statushd":
                    System.Console.WriteLine("Exibe propriedades do HD...");
                    break;
                case "removehd":
                    System.Console.WriteLine("Remove HD...");
                    break;
                case "copyfrom":
                    System.Console.WriteLine("Copia arquivo JPG do HD REAL para HD VIRTUAL...");
                    break;
                case "copyto":
                    System.Console.WriteLine("Exibe Conteudo do HD...");
                    break;
                case "renamedir":
                    System.Console.WriteLine("Renomeia diretorio...");
                    break;
                case "copy":
                    System.Console.WriteLine("Copia arquivo ou pasta...");
                    break;
                case "move":
                    System.Console.WriteLine("Move arquivo ou pasta...");
                    break;
                case "help":
                    System.Console.WriteLine("Ajuda sobre comandos...");
                    break;
                case "rename":
                    System.Console.WriteLine("Renomeia arquivo...");
                    break;
                case "rmdir":
                    System.Console.WriteLine("Remove diretorio...");
                    break;
                case "tree":
                    System.Console.WriteLine("Exibe estrutra de diretorios...");
                    break;
                default:
                    System.Console.WriteLine("Comando não encontrado!");
                    break;
            }
        }
    }
}
