using VirtualHdOperatingSystem.Application.Helpers;
using VirtualHdOperatingSystem.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace VirtualHdOperatingSystem.Application.Flows
{
    public class CreateFileFlow : ICommandFlow
    {
        public string FileName { get; set; }
        public string FileContent { get; set; }
        public Hd Hd { get; set; }
        public int CurrentByteInit { get; set; }

        public CreateFileFlow(List<string> _parameters)
        {
            if (_parameters.Count != 1 && _parameters.Count != 2)
            {
                throw new Exception("Invalid parameters number!");
            }
            FileContent = null;
            FileName = _parameters[0];

            if (_parameters.Count == 2)
            {
                FileContent = _parameters[1];
            }

            Hd = ConsolePathControl.GetSelectedHd();
            CurrentByteInit = ConsolePathControl.GetLastByteInitFromStack();
        }

        public void Execute()
        {
            try
            {
                FindEmptyBlockForFile();
                //Encontrar um lugar vazio

                //Inserir como preenchido no primeiro byte

                //Inserir como pai o CurrrentBlock

                //Encontrar um lugar vazio para o conteudo

                //Gravar o bloco do conteudo no bloco do file

                //Gravar o conteudo


                //for (var i = 0; i < FilesStack.Count; i++)
                //{
                //    var fileNameInByte = GetFileNameFromByteArray((CurrentBlockByte + 8), (CurrentBlockByte + HdInfo.BlockSize - 1));
                //    var fileNameGenerated = Encoding.UTF8.GetString(fileNameInByte);

                //    if (FilesStack[i] == fileNameGenerated)
                //    {
                //        if (i == FilesStack.Count - 1)
                //        {
                //            VerifyBrothers();
                //        }
                //    }
                //}

                //CreateFilePointer();

                //File.WriteAllBytes($"storage/{ConsolePathControl.GetSelectedHd()}", Hd);
            }
            catch (Exception ex)
            {
                //throw new Exception($"Something went wrong: {ex.Message}");
            }
        }

        private void FindEmptyBlockForFile()
        {
            int j = 0;
            for (var i = 0; i < Hd.BlockNumber; i++, j += Hd.BlockSize)
            {
                if (Hd.Bytes[j] == 0)
                {
                    Hd.Bytes[j] = 1;

                    var currentByteInitInBytes = BitConverter.GetBytes(CurrentByteInit);

                    int l = 0;
                    for (var k = j + 1; l < currentByteInitInBytes.Length; k++, l++)
                    {
                        Hd.Bytes[k] = currentByteInitInBytes[l];
                    }

                    byte[] nameBytes = Encoding.ASCII.GetBytes(FileName);

                    l = 0;
                    for (var k = j + 9; l < nameBytes.Length; k++, l++)
                    {
                        Hd.Bytes[k] = nameBytes[l];
                    }

                    if(FileContent != null)
                    {
                        FindEmptyBlockForContent(j);
                    }

                    File.WriteAllBytes($"storage/{Hd.HdName}", Hd.Bytes);

                    break;
                }
            }
        }

        private void FindEmptyBlockForContent(int _father)
        {
            int j = (int)Math.Ceiling(Hd.BlockNumber * 0.4) * Hd.BlockSize;
            for (var i = (int)Math.Ceiling(Hd.BlockNumber * 0.4); i < Hd.BlockNumber; i++, j += Hd.BlockSize)
            {
                if (Hd.Bytes[j] == 0)
                {
                    Hd.Bytes[j] = 1;

                    var _fatherBytes = BitConverter.GetBytes(_father);

                    int l = 0;
                    for (var k = j + 1; l < _fatherBytes.Length; k++, l++)
                    {
                        Hd.Bytes[k] = _fatherBytes[l];
                    }

                    byte[] fileContentBytes = Encoding.ASCII.GetBytes(FileContent);

                    l = 0;
                    for (var k = j + 5; l < fileContentBytes.Length; k++, l++)
                    {
                        Hd.Bytes[k] = fileContentBytes[l];
                    }

                    var contentBytes = BitConverter.GetBytes(j);
                    l = 0;
                    for (var k = _father + 5; l < fileContentBytes.Length; k++, l++)
                    {
                        Hd.Bytes[k] = contentBytes[l];
                    }

                    break;
                }
            }
        }

        //private void CreateFilePointer()
        //{
        //    var emptyBlock = FindAEmptyBlockForFilePointer();

        //    if (HasBrother)
        //    {
        //        Hd[CurrentBlockByte + 2] = BitConverter.GetBytes(emptyBlock)[0];
        //    }
        //    else
        //    {
        //        Hd[CurrentBlockByte + 1] = BitConverter.GetBytes(emptyBlock)[0];
        //    }
        //    Hd[emptyBlock] = Convertions.BinaryStringRepresentationIntoBinary("b.00000001")[0];

        //    var fileNameInBytes = Encoding.ASCII.GetBytes(FileName);

        //    var j = 0;
        //    for (var i = emptyBlock + 8; j < fileNameInBytes.Length; i++, j++)
        //    {
        //        Hd[i] = fileNameInBytes[j];
        //    }

        //    if (FileContent != null)
        //    {
        //        WriteFileContent(emptyBlock);
        //    }
        //}

        //private void VerifyBrothers()
        //{
        //    if(HasBrother == false)
        //    {
        //        if(Hd[CurrentBlockByte + 1] != 0)
        //        {
        //            HasBrother = true;
        //            CurrentBlockByte = Hd[CurrentBlockByte + 1];
        //            VerifyBrothers();
        //        }
        //    }
        //    else
        //    {
        //        if (Hd[CurrentBlockByte + 2] != 0)
        //        {
        //            CurrentBlockByte = Hd[CurrentBlockByte + 2];
        //            VerifyBrothers();
        //        }
        //    }

        //}

        //private void WriteFileContent(int _pointer)
        //{
        //    var emptyBlock = FindAEmptyBlockForFileContent();


        //    var pointToContent = BitConverter.GetBytes(emptyBlock);
        //    int j = 0;
        //    for(int i = _pointer + 3; j < pointToContent.Length; i++, j++)
        //    {
        //        Hd[i] = pointToContent[j];
        //    }

        //    Hd[emptyBlock] = Convertions.BinaryStringRepresentationIntoBinary("b.00000001")[0];

        //    var fileContentInBytes = Encoding.ASCII.GetBytes(FileContent);
        //    j = 0;
        //    for (var i = emptyBlock + 1; j < fileContentInBytes.Length; i++, j++)
        //    {
        //        if(emptyBlock + HdInfo.BlockSize - 1 == i)
        //        {
        //            var emptyBlockForTheRestOfTheContent = FindAEmptyBlockForFileContent();
        //            Hd[i] = BitConverter.GetBytes(emptyBlockForTheRestOfTheContent)[0];
        //            Hd[emptyBlockForTheRestOfTheContent] = Convertions.BinaryStringRepresentationIntoBinary("b.00000001")[0];

        //            i = emptyBlockForTheRestOfTheContent + 3;
        //        }
        //        else
        //        {
        //            Hd[i] = fileContentInBytes[j];
        //        }
        //    }
        //}

        //private int FindAEmptyBlockForFileContent()
        //{
        //    for (var i = Convert.ToInt32(HdInfo.BlockNumber * 0.4) + 1 ; i < HdInfo.BlockNumber; i++)
        //    {
        //        if (Hd[HdInfo.BlockSize * i] == 0)
        //        {
        //            return HdInfo.BlockSize * i;
        //        }
        //    }

        //    throw new Exception("Not enought space!");
        //}

        //private int FindAEmptyBlockForFilePointer()
        //{
        //    for(var i = 1; i< HdInfo.BlockNumber * 0.4; i++)
        //    {
        //        if(Hd[HdInfo.BlockSize * i] == 0)
        //        {
        //            return HdInfo.BlockSize * i;
        //        }
        //    }

        //    throw new Exception("Not enought space!");
        //}

        //private byte[] GetFileNameFromByteArray(int _start, int _finish)
        //{
        //    var stringSize = 0;
        //    for(var i = _start; i < _finish && Hd[i] != 0; i++)
        //    {
        //        stringSize++;
        //    }

        //    var stringInByte = new byte[stringSize];

        //    var j = 0;
        //    for(var i = _start; i< _finish && Hd[i] != 0; i++, j++)
        //    {
        //        stringInByte[j] = Hd[i];
        //    }

        //    return stringInByte;
        //}
    }
}
