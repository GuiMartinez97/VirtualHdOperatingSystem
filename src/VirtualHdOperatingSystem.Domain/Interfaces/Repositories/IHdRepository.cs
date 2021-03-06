﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualHdOperatingSystem.Domain.Entities;

namespace VirtualHdOperatingSystem.Domain.Interfaces.Repositories
{
    public interface IHdRepository
    {
        void UpsertHd(Hd _hd);
        byte[] SelectHd(string name);
        byte[] GetImageByteArray(string _imageName);
        void SaveImage(byte[] contentInByte, string _newImageName);
        void RemoveHd(string HdName);
    }
}
