using System;
using System.Collections.Generic;
using System.Text;

namespace VirtualHdOperatingSystem.Domain.Entities
{
    public class Hd
    {
        public string Name { get; set; }
        public int BlockNumber { get; set; }
        public int BlockSize { get; set; }
        public byte[] Bytes { get; set; }
        public int MaxBlockForFolderRegion { get; set; }
        public int StartBlockForContentRegion { get; set; }

        public Hd(string _name, int _blockNumber, int _blockSize)
        {
            Name = _name;
            BlockNumber = _blockNumber;
            BlockSize = _blockSize;

            Bytes = new byte[_blockNumber * (_blockSize + 13)];

            WriteHdConfiguration();
        }

        public Hd(string _name, byte[] _bytes)
        {
            Name = _name;
            Bytes = _bytes;

            ReadHdConfiguration();

            MaxBlockForFolderRegion = GetMaxBlockForFolderRegion();
            StartBlockForContentRegion = GetStartBlockForContentRegion();
        }

        public int CreateFolder(string _folderName, int _currentFileBlock)
        {
            int emptyBlock = FindEmptyBlockForFolder();

            SetInUsedBlock(emptyBlock);

            WriteFather(emptyBlock, _currentFileBlock);

            WriteName(emptyBlock, _folderName);

            return emptyBlock;
        }

        public void CreateFile(string _folderName, int _currentFileBlock, string _content)
        {
            var fileBlock = CreateFolder(_folderName, _currentFileBlock);

            int emptyBlockForContent = FindEmptyBlockForContent();

            SetInUsedBlock(emptyBlockForContent);

            WriteContentReference(fileBlock, emptyBlockForContent);

            WriteFather(emptyBlockForContent, fileBlock);

            WriteContent(emptyBlockForContent, _content);
        }

        public void CreateImageFile(string _folderName, int _currentFileBlock, byte[] _content)
        {
            var fileBlock = CreateFolder(_folderName, _currentFileBlock);

            int emptyBlockForContent = FindEmptyBlockForContent();

            SetInUsedBlock(emptyBlockForContent);

            WriteContentReference(fileBlock, emptyBlockForContent);

            WriteFather(emptyBlockForContent, fileBlock);

            WriteContentInByte(emptyBlockForContent, _content);
        }

        public int EnterFolder(string _folderName, int _currentFileBlock)
        {
            for(var blockBeingAnalised = 0 ; blockBeingAnalised < MaxBlockForFolderRegion ; blockBeingAnalised++)
            {
                if(GetIntFromBytes(CutBytes(GetInicialByteOfBlock(blockBeingAnalised) + 1, GetInicialByteOfBlock(blockBeingAnalised) + 4)) == _currentFileBlock)
                {
                    string blockName = GetBlockName(blockBeingAnalised);
                    if (blockName == _folderName)
                    {
                        return blockBeingAnalised;
                    }
                }                
            }

            throw new Exception("Folder not found!");
        }

        public void TypeHd()
        {
            for(int InicialBlock = 0 ; InicialBlock < BlockNumber - 1; InicialBlock++)
            {
                var byteForAnalisedBlock = GetInicialByteOfBlock(InicialBlock);
                if(Bytes[byteForAnalisedBlock] == 1)
                {
                    if (InicialBlock >= BlockNumber * 0.4)
                    {
                        var teste = GetBlockContentInString(InicialBlock);
                        Console.WriteLine($"{InicialBlock} - {Bytes[byteForAnalisedBlock]} - {GetIntFromBytes(CutBytes(byteForAnalisedBlock + 1, byteForAnalisedBlock + 4))} - {GetBlockContentInString(InicialBlock)}");
                    }
                    else
                    {
                        Console.WriteLine($"{InicialBlock} - {Bytes[byteForAnalisedBlock]} - {GetIntFromBytes(CutBytes(byteForAnalisedBlock + 1, byteForAnalisedBlock + 4))} - {GetIntFromBytes(CutBytes(byteForAnalisedBlock + 5, byteForAnalisedBlock + 8))} - {GetBlockName(InicialBlock)}");
                    }                    
                }
            }
        }

        public byte[] GetBlockContentInByte(int _block)
        {
            int inicialBytes = GetInicialByteOfBlock(_block);
            byte[] fullContentIncludingZeros = CutBytes(inicialBytes + 5, inicialBytes + BlockSize - 4);
            List<byte> fullContentIncludingWithZerosList = new List<byte>();

            int zeroTrack = 0;
            foreach(byte byteThatCouldBeZero in fullContentIncludingZeros)
            {
                if (byteThatCouldBeZero == 0)
                {
                    zeroTrack++;
                }
                else
                {
                    if (byteThatCouldBeZero > 0)
                    {
                        zeroTrack = 0;
                    }
                }
                fullContentIncludingWithZerosList.Add(byteThatCouldBeZero);
                if (zeroTrack == 10)
                {
                    break;
                }
            }

            if(zeroTrack > 0)
            {
                for(var i = 0; i <zeroTrack; i++)
                {
                    fullContentIncludingWithZerosList.RemoveAt(fullContentIncludingWithZerosList.Count - 1);
                }
            }

            return fullContentIncludingWithZerosList.ToArray();
        }

        public string GetBlockContentInString(int _block)
        {
            byte[] contentInByte = GetBlockContentInByte(_block);
            return GetStringFromBytes(contentInByte);
        }

        public void Dir(int _currentBlock)
        {
            for (int InicialBlock = 0; InicialBlock < BlockNumber * 0.4; InicialBlock++)
            {
                var byteForAnalisedBlock = GetInicialByteOfBlock(InicialBlock);
                if (Bytes[byteForAnalisedBlock] == 1)
                {
                    var fatherReferenceInBytes = CutBytes(byteForAnalisedBlock + 1, byteForAnalisedBlock + 4);

                    var fatherReference = GetIntFromBytes(fatherReferenceInBytes);
                    
                    if(fatherReference == _currentBlock)
                    {
                        Console.WriteLine($"{GetBlockName(InicialBlock)}");
                    }                    
                }
            }
        }

        public void Tree(int _currentBlock, string level)
        {
            for (int InicialBlock = 0; InicialBlock < BlockNumber * 0.4 ; InicialBlock++)
            {
                var byteForAnalisedBlock = GetInicialByteOfBlock(InicialBlock);
                if (Bytes[byteForAnalisedBlock] == 1)
                {
                    var fatherReferenceInBytes = CutBytes(byteForAnalisedBlock + 1, byteForAnalisedBlock + 4);

                    var fatherReference = GetIntFromBytes(fatherReferenceInBytes);

                    if (fatherReference == _currentBlock)
                    {
                        string blockName = GetBlockName(InicialBlock);
                        if(blockName != "raiz")
                        {
                            Console.WriteLine($"{level}{GetBlockName(InicialBlock)}");
                            if (InicialBlock != fatherReference)
                            {
                                var spaces = "";
                                foreach (var levelChar in level)
                                {
                                    spaces += " ";
                                }
                                Tree(InicialBlock, spaces + "  |---");
                            }
                        }                                              
                    }
                }
            }
        }

        public void StatusHd()
        {
            Console.WriteLine($"Total block: {BlockNumber}");
            Console.WriteLine($"Total bytes: {BlockNumber * BlockSize}");

            int totalUsedBlocks = 0;
            int totalUsedBytes = 0;
            int totalBlockUsedByFolder = 0;
            int totalBlockUsedByFile = 0;

            int totalFolder = 0;
            int totalFile = 0;

            for (int InicialBlock = 0; InicialBlock < BlockNumber - 1; InicialBlock++)
            {
                var byteForAnalisedBlock = GetInicialByteOfBlock(InicialBlock);
                if (Bytes[byteForAnalisedBlock] == 1)
                {
                    for(int i = 0 ; i<BlockSize ; i++)
                    {
                        if(Bytes[byteForAnalisedBlock + i] != 0)
                        {
                            totalUsedBytes++;
                        }
                    }
                    totalUsedBlocks++;

                    if(InicialBlock < BlockNumber * 0.4)
                    {
                        totalBlockUsedByFolder++;

                        var contentReference = GetIntFromBytes(CutBytes(byteForAnalisedBlock + 5, byteForAnalisedBlock + 8));

                        if(contentReference == 0)
                        {
                            totalFolder++;
                        }
                        else
                        {
                            totalFile++;
                        }
                    }
                    else
                    {
                        totalBlockUsedByFile++;
                    }
                }
            }

            Console.WriteLine($"Total used Block: {totalUsedBlocks}");
            Console.WriteLine($"Total used bytes: {totalUsedBytes}");

            Console.WriteLine($"Total used Block by folder: {totalBlockUsedByFolder}");
            Console.WriteLine($"Total used Block by file: {totalBlockUsedByFile}");

            Console.WriteLine($"Total folder: {totalFolder}");
            Console.WriteLine($"Total file: {totalFile}");

        }

        public void RenameFile(int _currentFileBlock, string _fileName, string _newName)
        {
            for (int InicialBlock = 0; InicialBlock < BlockNumber * 0.4; InicialBlock++)
            {
                var byteForAnalisedBlock = GetInicialByteOfBlock(InicialBlock);
                if (Bytes[byteForAnalisedBlock] == 1)
                {
                    var fatherReferenceInBytes = CutBytes(byteForAnalisedBlock + 1, byteForAnalisedBlock + 4);

                    var fatherReference = GetIntFromBytes(fatherReferenceInBytes);

                    if (fatherReference == _currentFileBlock)
                    {
                        string analisedBlockName = GetBlockName(InicialBlock);
                        if(analisedBlockName == _fileName)
                        {
                            byte[] cleaningByteArray = new byte[BlockSize];
                            CopyToBytes(cleaningByteArray, byteForAnalisedBlock + 9);
                            WriteName(InicialBlock, _newName);
                        }
                    }
                }
            }
        }

        public void Copy(int _currentBlock, string _fileToBeCopied, string _destiny)
        {
            // Identificar o bloco do destiny
            int destinyBlock = IdentifyDestinyBlock(_destiny);

            int fileToBeCopiedBlock = EnterFolder(_fileToBeCopied, _currentBlock);

            // Identificamos se é arquivo (caso base)
            if(IsFile(fileToBeCopiedBlock))
            {
                string fileContent = GetContentFromFileBlock(fileToBeCopiedBlock);
                CreateFile(_fileToBeCopied, destinyBlock, fileContent);
            }
            else
            {
                CreateFolder(_fileToBeCopied, destinyBlock);

                List<int> childrenBlock = GetChildrenBlock(fileToBeCopiedBlock);

                foreach(int child in childrenBlock)
                {
                    Copy(fileToBeCopiedBlock, GetBlockName(child), _destiny + $"/{_fileToBeCopied}");
                }
            }
        }

        public void CleanFromThisFileNameOn(int _currentBlock, string _fileToBeCleaned)
        {
            int fileToBeCleanedBlock = EnterFolder(_fileToBeCleaned, _currentBlock);

            Clean(_currentBlock, fileToBeCleanedBlock);
        }

        private void Clean(int _currentBlock, int _fileToBeCleanedBlock)
        {
            // Identificamos se é arquivo (caso base)
            if (IsFile(_fileToBeCleanedBlock))
            {
                int fileContentBlock = GetContentReferenceRegionIntValue(_fileToBeCleanedBlock);
                CleanBlock(fileContentBlock);
            }
            else
            {
                List<int> childrenBlock = GetChildrenBlock(_fileToBeCleanedBlock);

                foreach (int child in childrenBlock)
                {
                    Clean(_fileToBeCleanedBlock, child);
                }
            }

            CleanBlock(_fileToBeCleanedBlock);
        }

        private void CleanBlock(int _block)
        {
            int inicial = GetInicialByteOfBlock(_block);

            for(var i = inicial ; i<inicial+BlockSize ; i++)
            {
                Bytes[i] = 0;
            }
        }

        private string GetContentFromFileBlock(int fileToBeCopiedBlock)
        {
            int inicialBlockByte = GetInicialByteOfBlock(fileToBeCopiedBlock);
            var contentRegionInByte = CutBytes(inicialBlockByte + 5, inicialBlockByte + BlockSize - 4);
            var contentRegionInString = GetStringFromBytes(contentRegionInByte);
            return contentRegionInString;
        }

        private List<int> GetChildrenBlock(int _father)
        {
            var childList = new List<int>();

            for (var blockBeingAnalised = 0; blockBeingAnalised < BlockNumber - 1; blockBeingAnalised++)
            {
                int fatherOfTheBlockBeingAnalised = GetBlockOfTheFather(blockBeingAnalised);
                if (fatherOfTheBlockBeingAnalised == _father)
                {
                    childList.Add(blockBeingAnalised);
                }
            }

            return childList;
        }

        private int GetBlockOfTheFather(int _block)
        {
            int inicialByteOfBlock = GetInicialByteOfBlock(_block);
            byte[] fatherReferenceInBytes = CutBytes(inicialByteOfBlock + 1, inicialByteOfBlock + 4);
            int fatherReferenceInInt = GetIntFromBytes(fatherReferenceInBytes);
            return fatherReferenceInInt;
        }

        private int IdentifyDestinyBlock(string _dentiny)
        {
            string[] filesName = _dentiny.Split('/');

            var currentBlock = 0;
            foreach(string fileName in filesName)
            {
                int block = GetBlockIntByName(fileName, currentBlock);
                currentBlock = block;
            }

            return currentBlock;
        }

        private int GetBlockIntByName(string _fileName, int _currentBlock)
        {
            for (var blockBeingAnalised = 0; blockBeingAnalised < MaxBlockForFolderRegion; blockBeingAnalised++)
            {
                if (GetIntFromBytes(CutBytes(GetInicialByteOfBlock(blockBeingAnalised) + 1, GetInicialByteOfBlock(blockBeingAnalised) + 4)) == _currentBlock)
                {
                    string blockName = GetBlockName(blockBeingAnalised);
                    if (blockName == _fileName)
                    {
                        return blockBeingAnalised;
                    }
                }
            }

            throw new Exception($"Folder/File {_fileName} not found!");
        }

        private bool IsFile(int _block)
        {
            int contentRegionIntValue = GetContentReferenceRegionIntValue(_block);

            if(contentRegionIntValue == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private int GetContentReferenceRegionIntValue(int _block)
        {
            int inicialByte = GetInicialByteOfBlock(_block);
            byte[] contentReferenceRegionInBytes = CutBytes(inicialByte + 5, inicialByte + 8);
            int contentReferenceRegionInInt = GetIntFromBytes(contentReferenceRegionInBytes);
            return contentReferenceRegionInInt;
        }

        private void WriteContentReference(int _fileBlock, int _emptyBlockForContent)
        {
            var inicialByte = GetInicialByteOfBlock(_fileBlock);
            var emptyBlockForContentInByte = GetBytesFromInt(_emptyBlockForContent);

            CopyToBytes(emptyBlockForContentInByte, inicialByte + 5, inicialByte + 8);
        }

        private void WriteContent(int _emptyBlockForContent, string _content)
        {
            var contentInByte = GetBytesFromString(_content);
            CopyToBytes(contentInByte, GetInicialByteOfBlock(_emptyBlockForContent) + 5);
        }

        private void WriteContentInByte(int _emptyBlockForContent, byte[] _content)
        {
            CopyToBytes(_content, GetInicialByteOfBlock(_emptyBlockForContent) + 5);
        }

        private string GetBlockName(int _block)
        {
            int blockInicialByte = GetInicialByteOfBlock(_block);
            byte[] nameInBytes = CutBytes(blockInicialByte + 9, blockInicialByte + BlockSize + 9);
            return GetStringFromBytes(nameInBytes).Replace("\0", "");
        }

        private int GetMaxBlockForFolderRegion()
        {
            return Convert.ToInt32(Math.Ceiling(BlockNumber * 0.4));
        }

        private int GetStartBlockForContentRegion()
        {
            return Convert.ToInt32(Math.Ceiling(BlockNumber * 0.4)) + 1;
        }

        private int FindEmptyBlockForFolder()
        {
            int startByteOfTheBlockBeingAnalized = BlockSize + 13;
            for(int blockBeingCurrentlyAnalized = 0 ; blockBeingCurrentlyAnalized <= MaxBlockForFolderRegion ; blockBeingCurrentlyAnalized++, startByteOfTheBlockBeingAnalized += BlockSize + 13)
            {
                if(Bytes[startByteOfTheBlockBeingAnalized] == 0)
                {
                    return blockBeingCurrentlyAnalized;
                }
            }

            throw new Exception("Not enough space for creating folder!");
        }

        private int FindEmptyBlockForContent()
        {
            int startByteOfTheBlockBeingAnalized = GetInicialByteOfBlock(StartBlockForContentRegion);
            for (int blockBeingCurrentlyAnalized = StartBlockForContentRegion; blockBeingCurrentlyAnalized <= BlockNumber; blockBeingCurrentlyAnalized++, startByteOfTheBlockBeingAnalized += BlockSize + 13)
            {
                if (Bytes[startByteOfTheBlockBeingAnalized] == 0)
                {
                    return blockBeingCurrentlyAnalized;
                }
            }

            throw new Exception("Not enough space for creating folder!");
        }

        public void WriteFather(int _emptyBlock, int _fatherBlock)
        {
            var statingByteOfTheEmptyBlock = GetInicialByteOfBlock(_emptyBlock);

            var fatherBlockInByte = GetBytesFromInt(_fatherBlock);

            CopyToBytes(fatherBlockInByte, statingByteOfTheEmptyBlock + 1, statingByteOfTheEmptyBlock + 4);
        }

        public void WriteName(int _emptyBlock, string _folderName)
        {
            var statingByteOfTheEmptyBlock = GetInicialByteOfBlock(_emptyBlock);

            var folderNameInByte = GetBytesFromString(_folderName);

            CopyToBytes(folderNameInByte, statingByteOfTheEmptyBlock + 9);
        }

        private void WriteHdConfiguration()
        {
            SetInUsedBlock();

            byte[] blockNumberInByte = GetBytesFromInt(BlockNumber);
            CopyToBytes(blockNumberInByte, 1, 4);

            byte[] blockSizeInByte = GetBytesFromInt(BlockSize);
            CopyToBytes(blockSizeInByte, 5, 8);
        }

        private int GetInicialByteOfBlock(int _block)
        {
            return (_block + 1) * (BlockSize + 13);
        }

        private void ReadHdConfiguration()
        {
            byte[] blockNumberBytes = CutBytes(1, 4);
            byte[] blockSizeBytes = CutBytes(5, 8);

            BlockNumber = GetIntFromBytes(blockNumberBytes);
            BlockSize = GetIntFromBytes(blockSizeBytes);
        }

        private void SetInUsedBlock(int _block = -1)
        {
            if(_block == -1)
            {
                Bytes[0] = 1;
            }
            else
            {
                Bytes[GetInicialByteOfBlock(_block)] = 1;
            }
        }


        private void CopyToBytes(byte[] contentToBeCopied, int statingPositionInBytes, int endingPositionInBytes = -1)
        {
            if(endingPositionInBytes == -1)
            {
                endingPositionInBytes = statingPositionInBytes + contentToBeCopied.Length - 1;
            }

            for(int contentToBeCopiedController = 0 ; statingPositionInBytes <= endingPositionInBytes ; statingPositionInBytes++, contentToBeCopiedController++)
            {
                Bytes[statingPositionInBytes] = contentToBeCopied[contentToBeCopiedController];
            }
        }

        private byte[] CutBytes(int statingPositionInBytes, int endingPositionInBytes)
        {
            byte[] result = new byte[endingPositionInBytes - statingPositionInBytes + 1];

            for(int resultController = 0 ; statingPositionInBytes <= endingPositionInBytes ; resultController++, statingPositionInBytes++)
            {
                result[resultController] = Bytes[statingPositionInBytes];
            }

            return result;
        }

        private byte[] GetBytesFromInt(int number)
        {
            return BitConverter.GetBytes(number);
        }

        private int GetIntFromBytes(byte[] number)
        {
            return BitConverter.ToInt32(number, 0);
        }

        private byte[] GetBytesFromString(string message)
        {
            return Encoding.ASCII.GetBytes(message);
        }

        private string GetStringFromBytes(byte[] message)
        {
            return Encoding.Default.GetString(message);
        }
    }
}