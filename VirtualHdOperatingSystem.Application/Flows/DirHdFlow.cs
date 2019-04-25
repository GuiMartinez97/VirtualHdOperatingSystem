using VirtualHdOperatingSystem.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualHdOperatingSystem.Application.Flows
{
    public class DirHdFlow : ICommandFlow
    {
        public void Execute()
        {
            try
            {
                var hdsPath = Directory.GetFiles(@"storage");

                var hds = new List<string>();

                foreach(var hdPath in hdsPath)
                {
                    hds.Add(Path.GetFileName(hdPath));
                }


                foreach (var hd in hds)
                {
                    Console.WriteLine(hd);
                }
            }
            catch(Exception ex)
            {
                throw new Exception($"Something went wrong: {ex.Message}");
            }
        }
    }
}
