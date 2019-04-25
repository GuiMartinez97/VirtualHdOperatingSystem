using System.Collections.Generic;

namespace VirtualHdOperatingSystem.Application.Models
{
    public class EnteredCommand
    {
        public string Root { get; set; }
        public List<string> Parameters { get; set; }
    }
}
