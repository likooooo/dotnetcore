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
    /// <summary>
    /// 1<<1 = 2位深度
    /// 2^1 = 2色度
    /// </summary>
    public class BinaryImage:ImageCore,IImageCore
    {
        public BinaryImage(int width,int height,byte initColor = 0):base()
        {
            InitImageData(width,height,2);
            
            #region caculate head
            //1
            ImageType type = new ImageType
            {
                bfType = FileType
            };
            //2
            ImageFile bf = new ImageFile
            {
                bfSize = FileBytesSize,
                bfReserved = 0,
                bfOffBits = HeadStructSize
            };
            //3
            ImageFileInfo bfi = new ImageFileInfo
            {
                biSize = 40,
                biWidth = width,
                biHeight = height,
                biPlanes = 1,
                biBitCount = BitCount,
                biCompression = 0,
                biSizeImage = 0,
                biXPelsPerMeter = 0,
                biYPelsPerMeter = 0,
                biClrUsed = 0,
                biClrImportant = 0
            };
            //4 调色盘实现
            ColorPalette cp = new ColorPalette
            {
                palette = new byte[]{0,0,0,0,255,255,255,0}
            };
            ImageData data = new ImageData
            {
                D = new byte[BitmapSize]
            };
            //Log
            DebugMsg.DebugConsoleOut(type.ToString());
            DebugMsg.DebugConsoleOut(bf.ToString());
            DebugMsg.DebugConsoleOut(bfi.ToString());
            DebugMsg.DebugConsoleOut(cp.ToString());
            #endregion
            
            Span<byte> fileData;
            #region heap->stack
            ImageMemoryOpereSet.StructToSpan(type,bf,bfi,cp,data,out fileData);        
            if(initColor>0)
            {
                (fileData.Slice((int)HeadStructSize,BitmapSize)).Fill(initColor);
            }
            #endregion
            
            InitUnmanagedMen(fileData.ToArray());
        }
    
        public override void Decompose3(out IImageCore r,out IImageCore g,out IImageCore b)
        {
            throw new Exception("BinaryImage CANNOT Use Decompose3");
        }

        public override void Rgb1ToGray(out IImageCore gray)
        {
            throw new Exception("BinaryImage CANNOT Use Rgb1ToGray");
        }
    }
}