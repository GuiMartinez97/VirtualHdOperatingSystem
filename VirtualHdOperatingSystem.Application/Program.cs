using VirtualHdOperatingSystem.Application.CommandSplitters;
using VirtualHdOperatingSystem.Application.Factories;
using VirtualHdOperatingSystem.Application.Helpers;
using System;
using System.IO;
using System.Text;

namespace VirtualHdOperatingSystem.Application
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var factory = new CommandFactory();

                var splitter = new DefaultCommandSplitter();                

                while (true)
                {
                    Console.Write($"()==)=======>#{ConsolePathControl.GetSelectedHd()} > ");
                    var commandString = Console.ReadLine();

                    var splitedCommand = splitter.Split(commandString);

                    if(splitedCommand.Root == "cls")
                    {
                        Console.Clear();
                    }
                    else
                    {
                        var command = factory.GetCommand(splitedCommand);

                        try
                        {
                            command.Execute();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}