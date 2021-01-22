using System;
using System.Collections.Generic;
using ImageProcess.ImageEntry.Bmp;
using System.Runtime.InteropServices;

namespace ImageProcess.ImageEntry.RLC
{
    // public unsafe struct RLC_RgbNode
    // {
    //     public IntPtr ptr;
    //     public int Length;
    // }

    // public struct RLC_Data
    // {
    //     public IntPtr ptr;
    //     public int Length;
    //     public int Type;//rle_8bpp ; rle_4bpp
    // }
    //Run Length Code,游程编码
    // public static class RLC
    // {   

    //     //数据满足（0，N）分布，只保存N的起始地址和和数据长度
    //      public unsafe static void Compression_1(this BmpRgb1 img,out RLC_Data rlc)
    //      {

    //      }
    //     //未测试
    //     [Obsolete]
    //     public unsafe static void Compression(this BmpRgb1 img,out RLC_Data rlc)
    //     {
    //         rlc = new RLC_Data
    //         {
    //             Type = ImageTypeData.rle_8bpp
    //         };
    //         byte* head = (byte*)img.Scan0.ToPointer();
    //         byte[] d = new byte[]{0,1};
    //         int[] container = new int[img.Width * img.Height];

    //         int idx = 0;
    //         int[] res = new int[4];
    //         for(int i =0;i<img.Height;i++)
    //         {
    //             GetBit(ref head,ref res);
    //             container[idx++] = res[0];
    //             container[idx++] = res[1];
    //             container[idx++] = res[2];
    //             container[idx++] = res[3];
    //         }

    //         int[] rlcData = new int[container.Length];
    //         int rlcCount = -1;
    //         idx = 0;
    //         int startVal = container[0];
    //         int length = 1;
    //         fixed(int* p = &rlcData[0])
    //         {
    //             while(++idx < container.Length)
    //             {
    //                 if(startVal == container[idx])
    //                 {
    //                     length ++;
    //                 }
    //                 else
    //                 {
    //                     p[rlcCount++] = length;
    //                     p[rlcCount++] = startVal;
    //                     length = 1;
    //                     startVal = container[idx];
    //                 }
    //             }
    //         }    
    //         rlc.Length = rlcCount++;
    //         Marshal.Copy(rlcData,0,rlc.ptr,rlcCount);
    //     }
    //     private unsafe static void GetBit(ref byte* head,ref int[] res)
    //     {
    //         res[0] = (*head>>3)<<3;
    //         res[1] = (*head>>2)<<3;
    //         res[2] = (*head>>1)<<3;
    //         res[3] = *head<<3;
    //     }
    // }
}