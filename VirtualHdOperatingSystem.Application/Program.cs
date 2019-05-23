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
                    string hdName = "";
                    var hd = ConsolePathControl.GetSelectedHd();
                    if(hd != null)
                    {
                        hdName = hd.HdName;
                    }

                    string files = "";

                    foreach(var file in ConsolePathControl.GetFullFileStack())
                    {
                        files += "/" + file.fileName;
                    }

                    Console.Write($"()==)=======>#{hdName}{files} > ");
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