using System;
using System.Runtime;
using System.Collections.Generic;
using System.Runtime.InteropServices;

//https://docs.microsoft.com/zh-cn/dotnet/csharp/language-reference/operators/bitwise-and-shift-operators#logical-exclusive-or-operator-

namespace ImageProcess
{
    public static class Bit
    {
        static readonly int[] intMask = new int[]{15,240,3840,61440};//low->high
        static readonly int[] BitMaskForByte = new int[]{1,2,4,8,16,32,64,128};
        //获取int中指定字节
        public static byte GetByte(int source ,int byteIdx)=>(byte)((source&intMask[byteIdx])<<(8*byteIdx));

        public static int GetBit(byte source,byte idx) =>(BitMaskForByte[idx]&source)>>idx;//(byte)((byte)(source<< (7 -idx))>>7);

        //int 4字节赋值为相同字节值
        public static int SetIntBytesSameVal(byte val) => val + (val<<8) + (val<<16) + (val<< 24);    

    }
}
