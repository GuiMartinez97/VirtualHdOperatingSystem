using VirtualHdOperatingSystem.Domain.Entities;
using VirtualHdOperatingSystem.Domain.Interfaces.Repositories;
using VirtualHdOperatingSystem.Domain.Interfaces.Services;

namespace VirtualHdOperatingSystem.Domain.Services
{
    public class HdService : IHdService
    {
        private readonly IHdRepository __hdRepository__;

        public HdService(IHdRepository _hdRepository)
        {
            __hdRepository__ = _hdRepository;
        }

        public void CreateHd(string _name, int _blockNumber, int _blockSize)
        {
            var hd = new Hd(_name, _blockNumber, _blockSize);
            hd.CreateFolder("raiz", 0);
            __hdRepository__.UpsertHd(hd);
        }

        public Hd SelectHd(string _name)
        {
            var hdBytes = __hdRepository__.SelectHd(_name);
            var hd = new Hd(_name, hdBytes);
            return hd;
        }

        public void CreateFolder(Hd _hd, string _folderName, int _currentFileBlock)
        {
            _hd.CreateFolder(_folderName, _currentFileBlock);
            __hdRepository__.UpsertHd(_hd);
        }

        public void CreateFile(Hd _hd, string _fileName, int _currentFileBlock, string _content)
        {
            _hd.CreateFile(_fileName, _currentFileBlock, _content);
            __hdRepository__.UpsertHd(_hd);
        }

        public void Copy(Hd _hd, int _currentBlock, string _fileToBeCopied, string _destiny)
        {
            _hd.Copy(_currentBlock,  _fileToBeCopied, _destiny);
            __hdRepository__.UpsertHd(_hd);
        }

        public void Upsert(Hd _hd)
        {
            __hdRepository__.UpsertHd(_hd);
        }

        public void Move(Hd _hd, int _currentBlock, string _fileToBeMoved, string _destiny)
        {
            _hd.Copy(_currentBlock, _fileToBeMoved, _destiny);
            _hd.CleanFromThisFileNameOn(_currentBlock, _fileToBeMoved);
            __hdRepository__.UpsertHd(_hd);
        }

        public void RmDir(Hd _hd, int _currentBlock, string _fileToBeRemoved)
        {
            _hd.CleanFromThisFileNameOn(_currentBlock, _fileToBeRemoved);
            __hdRepository__.UpsertHd(_hd);
        }

        public void CopyFrom(Hd _hd, int _currentBlock, string _imageName, string _newImageName)
        {
            byte[] imageByteArray = __hdRepository__.GetImageByteArray(_imageName);
            _hd.CreateImageFile(_newImageName, _currentBlock, imageByteArray);
            __hdRepository__.UpsertHd(_hd);
        }
    }
}
