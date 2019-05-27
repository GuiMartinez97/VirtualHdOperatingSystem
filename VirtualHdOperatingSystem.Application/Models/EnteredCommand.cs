using System.Collections.Generic;

namespace VirtualHdOperatingSystem.Application.Models
{
    public class Request
    {
        public string Root { get; set; }
        public List<string> Parameters { get; set; }
    }
}
