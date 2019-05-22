using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualHdOperatingSystem.Application.Helpers
{
    public static class ConsolePathControl
    {
        private static Hd SelectedHd;
        private static List<string> FilesStack { get; set; } = new List<string>();

        public static void SetSelectedHd(Hd _hd)
        {
            FilesStack.Clear();
            FilesStack.Add("raiz");
            SelectedHd = _hd;
        }

        public static Hd GetSelectedHd()
        {
            return SelectedHd;
        }

        public static List<string> GetFullFileStack()
        {
            return FilesStack;
        }

        public static void PushStack(string _fileName)
        {
            FilesStack.Add(_fileName);
        }

        public static void PopStack(string _fileName)
        {
            FilesStack.RemoveAt(FilesStack.Count - 1);
        }
    }
}
