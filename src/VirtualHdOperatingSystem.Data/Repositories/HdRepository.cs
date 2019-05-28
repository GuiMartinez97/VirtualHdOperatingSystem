using System;
using VirtualHdOperatingSystem.Data.Context;
using VirtualHdOperatingSystem.Domain.Entities;
using VirtualHdOperatingSystem.Domain.Interfaces.Repositories;

namespace VirtualHdOperatingSystem.Data.Repositories
{
    public class HdRepository : IHdRepository
    {
        protected FileContext __fileContext__ = new FileContext("storage");

        public void UpsertHd(Hd _hd)
        {
            __fileContext__.WriteBytesInFile(_hd.Name, _hd.Bytes);
        }

        public byte[] SelectHd(string name)
        {
            try
            {
                return __fileContext__.GetBytesFromFile(name);
            }
            catch(Exception ex)
            {
                throw new Exception("HD not found!");
            }            
        }

        public byte[] GetImageByteArray(string _imageName)
        {
            return __fileContext__.GetImageByteArray(_imageName);
        }

        public void SaveImage(byte[] contentInByte, string _newImageName)
        {
            __fileContext__.SaveImage(contentInByte, _newImageName);
        }

        public void RemoveHd(string HdName)
        {
            __fileContext__.RemoveHd(HdName);
        }
    }
}
