using VirtualHdOperatingSystem.Application.Helpers;
using VirtualHdOperatingSystem.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;

namespace VirtualHdOperatingSystem.Application.Flows
{
    public class TypeHdFlow : ICommandFlow
    {
        public string HdName { get; set; }
        public Hd Hd { get; set; }

        public TypeHdFlow(List<string> _parameters)
        {
            if (_parameters.Count != 1)
            {
                throw new Exception("Invalid parameters number");
            }

            HdName = _parameters[0];

            Hd = new Hd(HdName, File.ReadAllBytes($"storage/{HdName}"));
        }
        public void Execute()
        {
            for (var i = 0; i < Hd.BlockNumber * Hd.BlockSize; i+= Hd.BlockSize)
            {
                if(i != 0)
                {
                    if(Hd.Bytes[i] == 1)
                    {
                        var block = i / Hd.BlockSize;

                        if (i< Hd.BlockNumber * Hd.BlockSize * 0.4)
                        {
                            var fatherBlockArray = new byte[4];
                            var b = i + 1;
                            for(var a = 0; a< fatherBlockArray.Length; a++, b++)
                            {
                                fatherBlockArray[a] = Hd.Bytes[b];
                            }
                            var fatherBlock = BitConverter.ToInt32(fatherBlockArray, 0)/Hd.BlockNumber;

                            var contentBlockArray = new byte[4];
                            b = i + 5;
                            for (var a = 0; a < contentBlockArray.Length; a++, b++)
                            {
                                contentBlockArray[a] = Hd.Bytes[b];
                            }
                            var contentBlock = BitConverter.ToInt32(contentBlockArray, 0) / Hd.BlockNumber;

                            var nameBlockAarray = new byte[Hd.BlockSize - 13];
                            b = i + 9;
                            for (var a = 0; a < nameBlockAarray.Length; a++, b++)
                            {
                                nameBlockAarray[a] = Hd.Bytes[b];
                            }
                            string cdName = System.Text.Encoding.Default.GetString(nameBlockAarray);
                            cdName = cdName.Replace("\0", string.Empty);

                            Console.WriteLine( block + " --- " + fatherBlock + " --- " + contentBlock + " --- " + cdName);
                        }
                        else
                        {
                            var fatherBlockArray = new byte[4];
                            var b = i + 1;
                            for (var a = 0; a < fatherBlockArray.Length; a++, b++)
                            {
                                fatherBlockArray[a] = Hd.Bytes[b];
                            }
                            var fatherBlock = BitConverter.ToInt32(fatherBlockArray, 0) / Hd.BlockNumber;

                            var nameBlockAarray = new byte[Hd.BlockSize - 5];
                            b = i + 5;
                            for (var a = 0; a < nameBlockAarray.Length; a++, b++)
                            {
                                nameBlockAarray[a] = Hd.Bytes[b];
                            }
                            string cdName = System.Text.Encoding.Default.GetString(nameBlockAarray);
                            cdName = cdName.Replace("\0", string.Empty);

                            Console.WriteLine(block + " --- " + fatherBlock + " --- " + cdName);
                        }
                    }
                }                
            }
        }
    }
}
