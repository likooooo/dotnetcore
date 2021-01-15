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
        int Decompose(out IImageCore r,out IImageCore g,out IImageCore b);
        int RgbToGray(IImageCore r,ImageCore g,IImageCore b,out IImageCore grayImage);
    }
}