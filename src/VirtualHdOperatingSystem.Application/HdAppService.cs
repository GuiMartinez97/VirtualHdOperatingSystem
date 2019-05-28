using VirtualHdOperatingSystem.Application.Interfaces;
using VirtualHdOperatingSystem.Domain.Entities;
using VirtualHdOperatingSystem.Domain.Interfaces.Services;

namespace VirtualHdOperatingSystem.Application
{
    public class HdAppService : IHdAppService
    {
        private readonly IHdService __hdService__;

        public HdAppService(IHdService _hdService)
        {
            __hdService__ = _hdService;
        }

        public void CreateHd(string _name, int _blockNumber, int _blockSize)
        {
            __hdService__.CreateHd(_name, _blockNumber, _blockSize);
        }

        public Hd SelectHd(string _name)
        {
            return __hdService__.SelectHd(_name);
        }

        public void CreateFolder(Hd _hd , string _folderName, int _currentFileBlock)
        {
            __hdService__.CreateFolder(_hd, _folderName, _currentFileBlock);
        }
        public void CreateFile(Hd _hd, string _folderName, int _currentFileBlock, string _content)
        {
            __hdService__.CreateFile(_hd, _folderName, _currentFileBlock, _content);
        }

        public int EnterFolder(Hd _hd, string _folderName, int currentFileBlock)
        {
            return _hd.EnterFolder(_folderName, currentFileBlock);
        }

        public void Dir(Hd _hd, int currentFileBlock)
        {
            _hd.Dir(currentFileBlock);
        }

        public void Tree(Hd _hd, int currentFileBlock)
        {
            _hd.Tree(currentFileBlock, "");
        }

        public void StatusHd(string _hdName)
        {
            var hd = __hdService__.SelectHd(_hdName);
            hd.StatusHd();
        }

        public void Rename(Hd _hd, int _currentFileBlock, string _fileName, string _newName)
        {
            _hd.RenameFile(_currentFileBlock, _fileName, _newName);
            __hdService__.Upsert(_hd);
        }

        public void Copy(Hd _hd, int _currentBlock, string _fileToBeCopied, string _destiny)
        {
            __hdService__.Copy(_hd, _currentBlock, _fileToBeCopied, _destiny);
        }

        public void Move(Hd _hd, int _currentBlock, string _fileToBeMoved, string _destiny)
        {
            __hdService__.Move(_hd, _currentBlock, _fileToBeMoved, _destiny);
        }

        public void RmDir(Hd _hd, int _currentBlock, string _fileToBeRemoved)
        {
            __hdService__.RmDir(_hd, _currentBlock, _fileToBeRemoved);
        }

        public void CopyFrom(Hd _hd, int _currentBlock, string _imageName, string _newImageName)
        {
            __hdService__.CopyFrom(_hd, _currentBlock, _imageName, _newImageName);
        }
    }
}
