using System;
using System.Collections.Generic;
using ImageProcess.ImageEntry.Bmp;
using System.Runtime.InteropServices;

namespace ImageProcess.ImageEntry.RLC
{
   public class RLC_Element_Rect
   {
      public int width,height;
      public int Count;
      public IntPtr ptr;
      
      public RLC_Element_Rect(int width,int height)
      {
         this.width = width;
         this.height = height;
      }

      public void Dispose()
      {
         if(Count > 0)
         {
            Marshal.FreeHGlobal(ptr);
         }
      }
      public unsafe void InitMemory()
      {
         Count = width*height;
         ptr = Marshal.AllocHGlobal(Count);
         byte* p = (byte*)ptr.ToPointer();
         int idx = -1;
         while(++idx <Count)
         {
            *p++ = 1;
         }
      }
   }
}