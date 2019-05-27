using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualHdOperatingSystem.Console.Models
{
    public class Request
    {
        public string Root { get; set; }
        public List<string> Parameters { get; set; }
    }
}
