using System;
using System.Text;
using System.Linq;
using System.Runtime;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.Json.Serialization;

using RuntimeDebug;

namespace ImageProcess
{
    //https://www.cnblogs.com/wanghao-boke/p/11635179.html
    public interface IOperateSet
    {
        void ReadImage(string filepath);
        void WriteImage(string filePath);
        void Decompose3(out IImageCore r,out IImageCore g,out IImageCore b);
        void Rgb1ToGray(out IImageCore grayImage);
        void Threshold(byte minVal, out IImageCore binaryImg);
    }
}