
using VirtualHdOperatingSystem.Domain.Entities;

namespace VirtualHdOperatingSystem.Domain.Interfaces.Services
{
    public interface IHdService
    {
        void CreateHd(string _name, int _blockNumber, int _blockSize);
        Hd SelectHd(string _name);
        void CreateFolder(Hd _hd, string _folderName, int _currentFileBlock);
        void CreateFile(Hd _hd, string _fileName, int _currentFileBlock, string _content);
    }
}
