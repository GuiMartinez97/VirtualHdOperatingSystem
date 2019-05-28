using System.Drawing;
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

        public byte[] GetImageByteArray(string _imageName)
        {
            Image img = Image.FromFile($"{_imageName}");
            byte[] arr;
            using (MemoryStream ms = new MemoryStream())
            {
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                arr = ms.ToArray();
            }

            return arr;
        }

        public void SaveImage(byte[] contentInByte, string _newImageName)
        {
            MemoryStream ms = new MemoryStream(contentInByte);
            Image returnImage = Image.FromStream(ms);
            returnImage.Save(_newImageName);
        }

        public void RemoveHd(string HdName)
        {
            File.Delete($"{__path__}{HdName}");
        }
    }
}
