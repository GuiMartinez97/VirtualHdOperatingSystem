using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualHdOperatingSystem.Application.Helpers;
using VirtualHdOperatingSystem.Application.Interfaces;

namespace VirtualHdOperatingSystem.Application.Flows
{
    public class CdFlow : ICommandFlow
    {
        public string FileToSearch { get; set; }
        public CdFlow(List<string> _parameters)
        {
            if (_parameters.Count != 1)
            {
                throw new Exception("Invalid parameters number!");
            }

            FileToSearch = _parameters[0];

        }
        public void Execute()
        {
            var hd = ConsolePathControl.GetSelectedHd();
            int fatherByte = ConsolePathControl.GetLastByteInitFromStack();

            for(var i = 0; i<hd.Bytes.Length; i+= hd.BlockSize)
            {
                if(i != 0)
                {
                    var thisFatherIntByte = new byte[4];

                    var l = 0;
                    for (var v = i + 1; l < thisFatherIntByte.Length; v++, l++)
                    {
                        thisFatherIntByte[l] = hd.Bytes[v];
                    }

                    var thisFatherInt = BitConverter.ToInt32(thisFatherIntByte, 0);

                    if (thisFatherInt == fatherByte)
                    {
                        var candidateNameInByte = new byte[hd.BlockSize - 9];
                        l = i + 9;
                        for (var j = 0; j < candidateNameInByte.Length; j++, l++)
                        {
                            candidateNameInByte[j] = hd.Bytes[l];
                        }

                        string cdName = System.Text.Encoding.Default.GetString(candidateNameInByte);

                        cdName = cdName.Replace("\0", string.Empty);

                        if (cdName == FileToSearch)
                        {
                            ConsolePathControl.PushStack(i, FileToSearch);

                            break;
                        }

                    }
                }                             
            }
        }
    }
}
