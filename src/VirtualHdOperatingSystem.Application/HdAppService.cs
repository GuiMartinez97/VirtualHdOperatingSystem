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

        public void CreateFolder(Hd _hd , string _folderName, int currentFileBlock = 0)
        {
            __hdService__.CreateFolder(_hd, _folderName, currentFileBlock);
        }

        public int EnterFolder(Hd _hd, string _folderName, int currentFileBlock = 0)
        {
            return _hd.EnterFolder(_folderName, currentFileBlock);
        }
    }
}
