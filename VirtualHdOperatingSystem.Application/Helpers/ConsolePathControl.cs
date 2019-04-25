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
        private static string SelectedHd { get; set; }
        private static List<string> FilesStack { get; set; } = new List<string>();
        private static List<HdInfo> HdInfoList { get; set; } = new List<HdInfo>();

        public static void SetSelectedHd(string _hdName)
        {
            if (File.Exists($"storage/{_hdName}"))
            {
                SelectedHd = _hdName;
                FilesStack = new List<string>();
                FilesStack.Add("raiz");
            }
        }

        public static void AddHdToList(HdInfo _hdInfo)
        {
            HdInfoList.Add(_hdInfo);
        }

        public static HdInfo GetHdByName(string _hdName)
        {
            foreach(var hd in HdInfoList)
            {
                if(hd.HdName == _hdName)
                {
                    return hd;
                }
            }

            return null;
        }

        public static string GetSelectedHd()
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
