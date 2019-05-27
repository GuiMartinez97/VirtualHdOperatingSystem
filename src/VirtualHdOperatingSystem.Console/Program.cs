using SimpleInjector;
using System;
using VirtualHdOperatingSystem.Application;
using VirtualHdOperatingSystem.Application.Interfaces;
using VirtualHdOperatingSystem.Console.Factories;
using VirtualHdOperatingSystem.Console.Helpers;
using VirtualHdOperatingSystem.Console.Interfaces;
using VirtualHdOperatingSystem.Data.Repositories;
using VirtualHdOperatingSystem.Domain.Interfaces.Repositories;
using VirtualHdOperatingSystem.Domain.Interfaces.Services;
using VirtualHdOperatingSystem.Domain.Services;

namespace VirtualHdOperatingSystem.Console
{
    static class Program
    {
        private static readonly Container __container__;
        static IHdAppService __hdAppService__;

        static Program()
        {
            __container__ = new Container();

            // AppServices
            __container__.Register<IHdAppService, HdAppService>();

            // Services
            __container__.Register<IHdService, HdService>();

            // Repositories
            __container__.Register<IHdRepository, HdRepository>();

            __container__.Verify();
        }

        static void Main(string[] args)
        {
            __hdAppService__ = __container__.GetInstance<IHdAppService>();

            while (true)
            {
                try
                {
                    string selectedHd = "";
                    string filesStackString = "";
                    try
                    {
                        selectedHd = ConsoleHelper.GetSelectedHd().Name + "@";
                        var filesInStack = ConsoleHelper.GetFullFileInStack();
                        foreach(var fileInStack in filesInStack)
                        {
                            if(fileInStack.FileName != "" && fileInStack.FileName != "raiz")
                            {
                                filesStackString += $"{fileInStack.FileName}/";
                            }                            
                        }
                    }
                    catch(Exception ex)
                    {

                    }                    

                    System.Console.Write($"{selectedHd}{filesStackString}> ");

                    string commandString = System.Console.ReadLine();

                    IController command = ControllerFactory.GetCommand(commandString);

                    command.Execute(__hdAppService__);
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
