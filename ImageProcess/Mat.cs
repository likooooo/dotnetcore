using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace ImageProcess
{
    //非托管内存对象
    public class Mat:IDisposable
    {
        public int Width{get;protected set;}
        public int Height{get;protected set;}  
        public int ElementSize{get;protected set;}
        public int RankSize{get;protected set;}
        public int Size{get;protected set;}
        public int Count{get;protected set;}
        public IntPtr Scan0{get;protected set;}    

        public Mat(){}
        public Mat(int width,int height,int elementSize){InitMemory(width,height,elementSize);}
        
        public void InitMemory(int width,int height,int elementSize)
        {
            Width = width;
            Height = height;
            ElementSize = elementSize;
            RankSize = Width*ElementSize;
            Size = RankSize*Height;
            Count = Width* Height;
            Scan0 = Marshal.AllocHGlobal(Size);
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
    }
}