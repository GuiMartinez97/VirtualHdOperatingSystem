﻿
using VirtualHdOperatingSystem.Domain.Entities;

namespace VirtualHdOperatingSystem.Domain.Interfaces.Services
{
    public interface IHdService
    {
        void CreateHd(string _name, int _blockNumber, int _blockSize);
        Hd SelectHd(string _name);
        void CreateFolder(Hd _hd, string _folderName, int _currentFileBlock);
        void CreateFile(Hd _hd, string _fileName, int _currentFileBlock, string _content);
        void Copy(Hd _hd, int _currentBlock, string _fileToBeCopied, string _destiny);
        void Move(Hd _hd, int _currentBlock, string _fileToBeMoved, string _destiny);
        void Upsert(Hd _hd);
        void RmDir(Hd _hd, int _currentBlock, string _fileToBeRemoved);
        void CopyFrom(Hd _hd, int _currentBlock, string _imageName, string _newImageName);
        void CopyTo(Hd _hd, int _currentBlock, string _imageName, string _newImageName);
        void RemoveHd(string HdName);
    }
}