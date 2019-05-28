using System;
using System.Collections.Generic;
using VirtualHdOperatingSystem.Domain.Entities;

namespace VirtualHdOperatingSystem.Console.Helpers
{
    public static class ConsoleHelper
    {
        private static Hd SelectedHd { get; set; }
        private static List<BlockFileName> FileStack = new List<BlockFileName>();

        public static void ClearFileFileStack()
        {
            FileStack.Clear();
        }

        public static void ClearHd()
        {
            SelectedHd = null;
        }

        public static void SetSelectedHd(Hd _hd)
        {
            SelectedHd = _hd;
            FileStack.Clear();
            FileStack.Add(new BlockFileName
            {
                Block = 0,
                FileName = "raiz"
            });
        }

        public static Hd GetSelectedHd()
        {
            if(SelectedHd == null)
            {
                throw new Exception("Select a HD first!");
            }

            return SelectedHd;
        }

        public static void PushStack(int _block, string _fileName)
        {
            FileStack.Add( new BlockFileName { Block = _block, FileName = _fileName });
        }

        public static BlockFileName GetLastFileInStack()
        {
            if(FileStack.Count == 0)
            {
                return null;
            }

            return FileStack[FileStack.Count - 1];
        }

        public static List<BlockFileName> GetFullFileInStack()
        {
            return FileStack;
        }

        public static void PopStack()
        {
            FileStack.RemoveAt(FileStack.Count - 1);
        }
    }

    public class BlockFileName
    {
        public int Block { get; set; }
        public string FileName { get; set; }
    }
}
