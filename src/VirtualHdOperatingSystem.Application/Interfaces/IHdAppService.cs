using VirtualHdOperatingSystem.Domain.Entities;

namespace VirtualHdOperatingSystem.Application.Interfaces
{
    public interface IHdAppService
    {
        void CreateHd(string _name, int _blockNumber, int _blockSize);
        Hd SelectHd(string _name);
        void CreateFolder(Hd _hd, string _folderName, int currentFileBlock = 0);
        int EnterFolder(Hd _hd, string _folderName, int currentFileBlock = 0);
    }
}
