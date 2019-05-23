using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualHdOperatingSystem.Application.Helpers;
using VirtualHdOperatingSystem.Application.Interfaces;

namespace VirtualHdOperatingSystem.Application.Flows
{
    public class DirFlow : ICommandFlow
    {
        private Hd Hd { get; set; }
        public DirFlow()
        {
            Hd = ConsolePathControl.GetSelectedHd();
        }

        public void Execute()
        {
            for(var i = 0; i<Hd.BlockNumber * Hd.BlockNumber * 0.4; i+= Hd.BlockSize)
            {
                if (i != 0)
                {
                    var fatherByte = new byte[4];
                    var b = i + 1;
                    for(var a = 0; a<fatherByte.Length; a++, b++)
                    {
                        fatherByte[a] = Hd.Bytes[b];
                    }
                    var thisFatherInt = BitConverter.ToInt32(fatherByte, 0);
                    if (thisFatherInt == ConsolePathControl.GetLastByteInitFromStack())
                    {
                        var fileName = new byte[Hd.BlockSize - 13];
                        b = i + 9;
                        for (var a = 0; a < fileName.Length; a++, b++)
                        {
                            fileName[a] = Hd.Bytes[b];
                        }
                        string cdName = System.Text.Encoding.Default.GetString(fileName);
                        Console.WriteLine(cdName);
                    }
                }
            }
        }
    }
}
