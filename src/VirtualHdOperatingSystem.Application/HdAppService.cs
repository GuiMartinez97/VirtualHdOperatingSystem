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
    }
}
