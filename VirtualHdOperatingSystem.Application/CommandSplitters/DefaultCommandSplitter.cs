using VirtualHdOperatingSystem.Application.Interfaces;
using VirtualHdOperatingSystem.Application.Models;
using System;
using System.Linq;

namespace VirtualHdOperatingSystem.Application.CommandSplitters
{
    public class DefaultCommandSplitter : ICommandSplitter
    {
        public EnteredCommand Split(string _commandEntered)
        {
            var members = _commandEntered.Split(' ');

            var enteredCommand = new EnteredCommand
            {
                Root = members[0],
                Parameters = members.ToList().GetRange(1, members.Length - 1)
            };

            return enteredCommand;
        }
    }
}
