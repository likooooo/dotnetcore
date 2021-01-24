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

        //将二值图信息拆分为bmp，或者将bmp合并为二值图信息的 位拆分合并私有方法
        //10100101 =>0001 0000 0001 0000 0000 0001 0000 0001
        static Int64 ByteToInt64(byte val)
        {
            Int64 res;
            res =  val&1;
            res += (val&2)<<3;
            res += (val&4)<<6;
            res += (val&8)<<9;
            res += (val&16)<<12;
            res += (val&32)<<15;
            res += (val&64)<<18;
            res += (val&128)<<21;
            return res;
        }
        static byte Int64ToByte(Int64 val)
        {
            byte res;
            res =  (byte)( val&1             );
            res += (byte)((val&16)       >> 3);
            res += (byte)((val&256)      >> 6);
            res += (byte)((val&4096)     >> 9);
            res += (byte)((val&65536)    >>12);
            res += (byte)((val&1048576)  >>15);
            res += (byte)((val&16777216) >>18);
            res += (byte)((val&268435456)>>21);
            return res;
        }
    }
}
