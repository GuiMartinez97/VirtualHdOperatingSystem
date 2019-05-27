using System.IO;

namespace VirtualHdOperatingSystem.Data.Context
{
    public class FileContext
    {
        private readonly string __path__;

        public FileContext(string _path)
        {
            __path__ = $"{_path}/";
        }

        public void WriteBytesInFile(string _fileName, byte[] _bytes)
        {
            if (!Directory.Exists(__path__))
            {
                Directory.CreateDirectory(__path__);
            }
            File.WriteAllBytes($"{__path__}{_fileName}", _bytes);
        }

        public byte[] GetBytesFromFile(string _fileName)
        {
            var bytesFromFile = File.ReadAllBytes($"{__path__}{_fileName}");
            return bytesFromFile;
        }
    }
}
