using VirtualHdOperatingSystem.Application.Helpers;
using VirtualHdOperatingSystem.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualHdOperatingSystem.Application.Flows
{
    public class TypeFlow : ICommandFlow
    {
        public string FileName { get; set; }
        public byte[] Hd { get; set; }
        private int CurrentBlockByte { get; set; }
        public HdInfo HdInfo { get; set; }
        private List<string> FilesStack { get; set; }
        private bool HasBrother { get; set; }

        public TypeFlow(List<string> _parameters)
        {
            if (_parameters.Count != 1)
            {
                throw new Exception("Invalid parameters number!");
            }

            FileName = _parameters[0];
            Hd = File.ReadAllBytes($"storage/{ConsolePathControl.GetSelectedHd()}");
            
            HdInfo = ConsolePathControl.GetHdByName(ConsolePathControl.GetSelectedHd());

            CurrentBlockByte = HdInfo.BlockSize;

            FilesStack = ConsolePathControl.GetFullFileStack();

        }
        public void Execute()
        {
            try
            {
                CurrentBlockByte = HdInfo.BlockSize;

                for (var i = 0; i < FilesStack.Count; i++)
                {
                    var fileNameInByte = GetFileNameFromByteArray((CurrentBlockByte + 8), (CurrentBlockByte + HdInfo.BlockSize - 1));
                    var fileNameGenerated = Encoding.UTF8.GetString(fileNameInByte);

                    if (FilesStack[i] == fileNameGenerated)
                    {
                        if (i == FilesStack.Count - 1)
                        {
                            CurrentBlockByte = Hd[CurrentBlockByte + 1];
                            SearchFile();
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private byte[] GetFileNameFromByteArray(int _start, int _finish)
        {
            var stringSize = 0;
            for (var i = _start; i < _finish && Hd[i] != 0; i++)
            {
                stringSize++;
            }

            var stringInByte = new byte[stringSize];

            var j = 0;
            for (var i = _start; i < _finish && Hd[i] != 0; i++, j++)
            {
                stringInByte[j] = Hd[i];
            }

            return stringInByte;
        }

        private void SearchFile()
        {
            var fileNameInByte = GetFileNameFromByteArray((CurrentBlockByte + 8), (CurrentBlockByte + HdInfo.BlockSize - 1));
            var fileNameGenerated = Encoding.UTF8.GetString(fileNameInByte);

            if (fileNameGenerated == FileName)
            {
                var contentStartInBytes = new Byte[4];
                int j = 0;
                for(var i = CurrentBlockByte + 3; i< CurrentBlockByte + 7; i++, j++)
                {
                    contentStartInBytes[j] = Hd[i];
                }

                var contentStart = BitConverter.ToInt32(contentStartInBytes, 0);
                var contentInStringFormat = GetFileContentInStringFormat(contentStart);
                Console.WriteLine(contentInStringFormat);
            }
            else
            {
                CurrentBlockByte = Hd[CurrentBlockByte + 2];
                SearchFile();
            }
        }

        private string GetFileContentInStringFormat(int inicialByte)
        {
            var stringSize = 0;
            var startByte = inicialByte;
            for (var i = startByte + 1; i < startByte + HdInfo.BlockSize - 1 && Hd[i] != 0; i++)
            {
                if(i == startByte + HdInfo.BlockSize - 1)
                {
                    startByte = Hd[i];
                }
                else
                {
                    stringSize++;
                }
            }

            var contentInString = new byte[stringSize];

            var j = 0;
            for (var i = inicialByte + 1; i < inicialByte + HdInfo.BlockSize - 1 && Hd[i] != 0; i++, j++)
            {
                if (i == inicialByte + HdInfo.BlockSize - 1)
                {
                    inicialByte = Hd[i];
                }
                else
                {
                    contentInString[j] = Hd[i];
                }
            }


            return Encoding.UTF8.GetString(contentInString);
        }
    }
}
