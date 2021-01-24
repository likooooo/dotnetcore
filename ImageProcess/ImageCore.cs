using System;
using System.Runtime.InteropServices;

namespace ImageProcess
{
    //bmp,jpg,tiff基类，默认类型为bmp
    public abstract class ImageCore:IDisposable
    { 
        public int Width{get;protected set;}
        public int Height{get;protected set;}
        public ushort BitCount{get;protected set;}
        //压缩方式/文件类型
        public int Compression{get;protected set;}

        public int ElementSize{get;protected set;}
        //一行文件字节数
        public int Stride{get;protected set;}
        //一行有效像素字节数
        public int RankBytesCount{get;protected set;}
        //整个图像有效像素字节数
        public int Count{get;protected set;}
        //位图起始索引，RankBytesCount * Height
        public IntPtr Scan0{get;protected set;}   

        public ImageCore(){}
        public ImageCore(int width,int height,ushort bitcount,int compression)
        {
            InitImageHead(width,height,bitcount,compression);
        }

        public virtual void InitImageHead(int width,int height,ushort bitcount,int compression)
        {
            
            Width = width;
            Height = height;
            BitCount = bitcount;    
            Compression = compression ; 
            ElementSize = bitcount > 7 ? bitcount>>3 : 1;
            Stride = ((Width*BitCount + 31)>>5)<<2;
            RankBytesCount = (Width*BitCount)>>3;
            Count = RankBytesCount*Height;
        }
       
        public virtual void InitMemory(){Scan0 = Marshal.AllocHGlobal(Count);}


        #region IDisposable接口实现     
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        
        protected virtual void Dispose(bool disposeing)
        {
            if(Count > 0)
            {              
                Marshal.FreeHGlobal(Scan0);
            }
        }
        #endregion    
 
        public virtual void ReadImage(string filepath){ throw new System.Exception("No Realization");}
        public virtual void WriteImage(string filepath){ throw new System.Exception("No Realization");}

        //非托管内存之间Copy
        [DllImport("msvcrt.dll", EntryPoint = "memcpy",CallingConvention = CallingConvention.Cdecl, SetLastError = false)]
        public static extern IntPtr memcpy(IntPtr dest, IntPtr src, UIntPtr count);
    
        // public virtual Mat RefrenceMat(){ throw new System.Exception("No Realization");}
    }
}