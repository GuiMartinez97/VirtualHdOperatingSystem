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
    }
}
