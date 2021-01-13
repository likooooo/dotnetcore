#pragma warning disable 0649
using System;
using System.Text;
using System.Linq;
using System.Runtime;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.Json.Serialization;

using RuntimeDebug;

//https://blog.csdn.net/u013066730/article/details/82625158
//https://www.cnblogs.com/wanghao-boke/p/11635179.html
namespace ImageProcess
{
    [Serializable]
    internal struct ImageType
    {
        [JsonInclude]
        public ushort bfType;

        public override string ToString()
        {
            return JsonSerializer.Serialize(this,new JsonSerializerOptions{WriteIndented = true});
        }
    }

    [Serializable]
    internal struct ImageFile
    {
        [JsonInclude]
        public int bfSize;

        [JsonInclude]
        public int bfReserved;

        [JsonInclude]
        public int bfOffBits;

        public override string ToString()
        {
            return JsonSerializer.Serialize(this,new JsonSerializerOptions{WriteIndented = true});
        }
    }


    [Serializable]
    internal struct ImageFileInfo
    {
        [JsonInclude]
        public int biSize;

        [JsonInclude]
        public int biWidth;

        [JsonInclude]
        public int biHeight;

        //两个 ushort 在字节对齐的时候会放到一起
        [JsonInclude]
        public ushort biPlanes;
        [JsonInclude]
        public ushort biBitCount;

        [JsonInclude]
        public int biCompression;

        [JsonInclude]
        public int biSizeImage;

        [JsonInclude]
        public int biXPelsPerMeter;

        [JsonInclude]
        public int biYPelsPerMeter;

        [JsonInclude]
        public int biClrUsed;
        
        [JsonInclude]
        public int biClrImportant;

        public override string ToString()
        {
            return JsonSerializer.Serialize(this,new JsonSerializerOptions{WriteIndented = true});
        }
    }
    
    [Serializable]
    internal struct Section 
    {
        [JsonInclude]
        public int a;

        [JsonInclude]
        public int b;
        
        [JsonInclude]
        public int c;

        public override string ToString()
        {
            return JsonSerializer.Serialize(this,new JsonSerializerOptions{WriteIndented = true});
        }
    }

    [Serializable]
    internal class ColorPalette
    {
        [NonSerialized]
        public int[] palette;
        public int Count{get;private set;}
        public ColorPalette(ImageFileInfo info)
        {
            Count = info.biBitCount<<2;
            palette = new int[Count];
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this,new JsonSerializerOptions{WriteIndented = true});
        }
    }

    [Serializable]
    public class ImageData
    {
        public byte[] D{get;set;}
        public int Stride{get;protected set;}
        public int Count{get;protected set;}
        protected ImageData(){}

        public override string ToString()
        {
            return JsonSerializer.Serialize(this,new JsonSerializerOptions{WriteIndented = true});
        }
    }

    public class ImageCommon
    {
        //p为非托管内存
        public static TStruct PtrToStruct<TStruct>(IntPtr p) where TStruct : struct
        {
            int structSize = Marshal.SizeOf(typeof(TStruct));
            TStruct t = default(TStruct);
            t = Marshal.PtrToStructure<TStruct>(p);
            return t;
        }

        public static int[] BytesToInt32Arry(byte[] bytes)
        {
            int resCount = bytes.Length/4;
            int[] res = new int[resCount];
            IntPtr p = Marshal.AllocHGlobal(bytes.Length);
            Marshal.Copy(bytes,0,p,bytes.Length);
            Marshal.Copy(p,res,0,bytes.Length);
            Marshal.FreeHGlobal(p);
            return res;
        }

        /// <summary>
        /// 4字节对齐
        /// </summary>
        /// <param name="width"></param>
        /// <param name="bitCount"></param>
        /// <returns></returns>
        public static int GetStride(int width,ushort bitCount) =>(((width*bitCount) + 31)>>5)<<2;
    
    }

    public class BmpImage:ImageData
    {
        internal ImageType type;
        internal ImageFile bf;
        internal ImageFileInfo bi;
        internal Section s;
        internal ColorPalette cp;

        public BmpImage():base(){}

        public int ReadImage(string filePath)
        {
            int currentIdx = 0;
            Span<byte> span = new Span<byte>(StreamHelper.FileToBytes(filePath));
            Span<byte> tempSpan = span.Slice(currentIdx,2);
            type = StreamHelper.BytesToStruct<ImageType>(tempSpan.ToArray());
            if(!IsBmp())
            {
                DebugMsg.DebugConsoleOut($"Is not bmp:{type.bfType}");
                return 1;
            }
            currentIdx += 2;

            tempSpan = span.Slice(currentIdx,12);
            bf = StreamHelper.BytesToStruct<ImageFile>(tempSpan.ToArray());
            DebugMsg.DebugConsoleOut(bf.ToString());
            currentIdx += 12;

            tempSpan = span.Slice(currentIdx,40);
            bi = StreamHelper.BytesToStruct<ImageFileInfo>(tempSpan.ToArray());
            DebugMsg.DebugConsoleOut(bi.ToString());
            currentIdx += 40;

            //可有可无
            if(bi.biSize == 56)
            {
                tempSpan = span.Slice(currentIdx,16);
                s = StreamHelper.BytesToStruct<Section>(tempSpan.ToArray());
                DebugMsg.DebugConsoleOut(s.ToString());
                currentIdx += 16;
            }
            
            if(bi.biBitCount == 1 ||bi.biBitCount == 4 ||bi.biBitCount == 8)
            {
                cp = new ColorPalette(bi);
                tempSpan = span.Slice(currentIdx,cp.Count*4);
                cp.palette = ImageCommon.BytesToInt32Arry(tempSpan.ToArray());
                Console.WriteLine($"{cp.Count}:{cp.palette.Length}:{tempSpan.Length}");
                DebugMsg.DebugConsoleOut(cp.ToString());
                currentIdx += tempSpan.Length;             
            }
            else
            {
                cp = null;
            }
            base.Stride = ImageCommon.GetStride(bi.biWidth,bi.biBitCount);
            base.Count = Stride*bi.biHeight;
            DebugMsg.DebugConsoleOut(base.ToString());
            
            if(Count == span.Length - currentIdx)
            {
                DebugMsg.DebugConsoleOut($"Count:{Count} == ExistDataLength:{span.Length - currentIdx}");
                base.D = new byte[Count];
                base.D = span.Slice(currentIdx,Count).ToArray();
                currentIdx += Count;
                return 0;
            }
            else
            {
                DebugMsg.DebugConsoleOut($"Count:{Count} != ExistDataLength:{span.Length - currentIdx}");
                return 1;
            }
        }

        /// <summary>
        /// 'BM'
        /// </summary>
        /// <returns></returns>
         public bool IsBmp()=> 19778 == type.bfType;
        
    }
}