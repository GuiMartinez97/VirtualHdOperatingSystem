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
        private static List<BlockAndName> FilesStack { get; set; } = new List<BlockAndName>();

        public static void SetSelectedHd(Hd _hd)
        {
            FilesStack.Clear();
            FilesStack.Add(new BlockAndName(_hd.BlockSize, "raiz"));
            SelectedHd = _hd;
        }

        public static Hd GetSelectedHd()
        {
            return SelectedHd;
        }

        public static List<BlockAndName> GetFullFileStack()
        {
            return FilesStack;
        }

        public static void PushStack(int _exactByteInit, string _fileName)
        {
            FilesStack.Add(new BlockAndName(_exactByteInit, _fileName));
        }

        public static void PopStack(string _fileName)
        {
            FilesStack.RemoveAt(FilesStack.Count - 1);
        }

        public static int GetLastByteInitFromStack()
        {
            return FilesStack[FilesStack.Count - 1].exactByteInit;
        }
    }

    public class BlockAndName
    {
        public BlockAndName(int _exactByteInit, string _fileName)
        {
            exactByteInit = _exactByteInit;
            fileName = _fileName;            
        }
        public string fileName { get; set; }
        public int exactByteInit { get; set; }
    }
}
