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
    // /// <summary>
    // /// 1<<1 = 2位深度
    // /// 2^1 = 2色度
    // /// </summary>
    // public class BinarayImage:ImageCore,IImageCore
    // {
    //     public BinarayImage(uint width,uint height,byte initColor = 0):base()
    //     {
    //         FileType = 19778;
    //         this.Width = width;
    //         this.Height = height;
    //         this.BitCount = 1;
    //         Stride = ImageAttribute.GetStride(this);
    //         BitmapSize = (int)(Stride*height);  
    //         HeadStructSize = 64;//54+ (1<<1)*4
    //         FileBytesSize = (uint)BitmapSize + HeadStructSize;
    //         PaletteSize = 8;
    //         Console.WriteLine($"{Stride}-{BitmapSize} - {HeadStructSize}");
    
    //         //1
    //         ImageType type = new ImageType
    //         {
    //             bfType = FileType
    //         };
    //         //2
    //         ImageFile bf = new ImageFile
    //         {
    //             bfSize = FileBytesSize,
    //             bfReserved = 0,
    //             bfOffBits = HeadStructSize
    //         };
    //         //3
    //         ImageFileInfo bfi = new ImageFileInfo
    //         {
    //             biSize = 40,
    //             biWidth = width,
    //             biHeight = height,
    //             biPlanes = 1,
    //             biBitCount = BitCount,
    //             biCompression = 0,
    //             biSizeImage = 0,
    //             biXPelsPerMeter = 0,
    //             biYPelsPerMeter = 0,
    //             biClrUsed = 0,
    //             biClrImportant = 0
    //         };
    //         //4 调色盘实现
    //         ColorPalette cp = new ColorPalette
    //         {
    //             palette = new byte[]{0,0,0,0,255,255,255,0}
    //         };
    //         ImageData data = new ImageData
    //         {
    //             D = new byte[BitmapSize]
    //         };
    //         //Log
    //         DebugMsg.DebugConsoleOut(type.ToString());
    //         DebugMsg.DebugConsoleOut(bf.ToString());
    //         DebugMsg.DebugConsoleOut(bfi.ToString());
    //         DebugMsg.DebugConsoleOut(cp.ToString());

    //         Span<byte> fileData;
    //         ImageMemoryOpereSet.StructToImage(type,bf,bfi,cp,data,out fileData);        
    //         if(initColor>0)
    //         {
    //             (fileData.Slice((int)HeadStructSize,BitmapSize)).Fill(255);
    //         }

    //         IntPtr p = Marshal.AllocHGlobal(fileData.Length);
    //         Marshal.Copy(fileData.ToArray(),0,p,fileData.Length);   
    //         unsafe
    //         {
    //             _head = (byte*)p.ToPointer();
    //             Palette = PaletteSize>0? new IntPtr(_head + 54): default(IntPtr);
    //         } 
    //         GetPalette();
    //         GetScan0();
    //     }
    // }
    
    /// <summary>
    /// 1<<3 = 8
    /// 2^8  = 256分辨率
    /// 灰度图/伪彩色图，区别在于调色板值不同
    /// </summary>
    public class Image_8BitCount:ImageCore,IImageCore
    {
        public Image_8BitCount(uint width,uint height):base(width,height,8)
        {
        }
        public bool IsGrayImage()=>true;
    }
}