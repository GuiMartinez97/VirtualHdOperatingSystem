using VirtualHdOperatingSystem.Domain.Entities;

namespace VirtualHdOperatingSystem.Application.Interfaces
{
    public interface IHdAppService
    {
        void CreateHd(string _name, int _blockNumber, int _blockSize);
        Hd SelectHd(string _name);
        void CreateFolder(Hd _hd, string _folderName, int currentFileBlock);
        void CreateFile(Hd _hd, string _fileName, int _currentFileBlock, string _content);
        int EnterFolder(Hd _hd, string _folderName, int currentFileBlock);
        void Dir(Hd _hd, int currentFileBlock);
        void Tree(Hd _hd, int currentFileBlock);
        void StatusHd(string _hdName);
        void Rename(Hd _hd, int _currentFileBlock, string _fileName, string _newName);
        void Copy(Hd _hd, int _currentBlock, string _fileToBeCopied, string _destiny);
        void Move(Hd _hd, int _currentBlock, string _fileToBeMoved, string _destiny);
    }
}
