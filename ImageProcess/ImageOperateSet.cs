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
        int ReadImage(string filePath);
        int Decompose(out ImageData r,out ImageData g,out ImageData b);
    }
    public class Rgb16pp:BmpImage,IOperateSet
    {
        public Rgb16pp(ImageData origin):base(){}

        public int Decompose(out ImageData r,out ImageData g,out ImageData b)
        {
            r=null;
            g=null;
            b=null;
            return 0;
        }
    }
}