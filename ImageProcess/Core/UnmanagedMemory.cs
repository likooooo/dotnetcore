using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace ImageProcess.Core
{
    public interface IUnmanagedMemory:IDisposable
    {
        IntPtr Scan0{get;}      
        int Width{get;}
        int Height{get;}
        ushort BitCount{get;}
        int RankBytesCount{get;}
        int Count{get;}        
    }

    //非托管内存对象
    public class UnmanagedMemory:IUnmanagedMemory
    {
        public IntPtr Scan0{get;protected set;}    
        public int Width{get;protected set;}
        public int Height{get;protected set;}  
        public ushort BitCount{get;protected set;}
        public int RankBytesCount{get;protected set;}
        public int Count{get;protected set;}

        public UnmanagedMemory(){}
        public UnmanagedMemory(int width,int height,ushort bitCount){InitMemory(width,height,bitCount);}
        
        public void InitMemory(int width,int height,ushort bitCount)
        {
            Width = width;
            Height = height;
            BitCount = bitCount;
            RankBytesCount = (Width*BitCount)>>3;
            Count = RankBytesCount*Height;
            Scan0 = Marshal.AllocHGlobal(Count);
        }
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
    
        /// <summary>
        /// 非托管内存之间Copy
        /// </summary>
        /// <param name="dest"></param>
        /// <param name="src"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        [DllImport("msvcrt.dll", EntryPoint = "memcpy",CallingConvention = CallingConvention.Cdecl, SetLastError = false)]
        public static extern IntPtr memcpy(IntPtr dest, IntPtr src, UIntPtr count);
    }
}