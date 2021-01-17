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
    using ImageProcess.ImageExtention;

    /// <summary>
    /// 1<<3 = 8
    /// 2^8  = 256分辨率
    /// 灰度图/伪彩色图，区别在于调色板值不同
    /// </summary>
    public class Image_4BitGray:ImageCore,IImageCore
    {
        public Image_4BitGray(){}
        public Image_4BitGray(int width,int height,byte initColor):base()
        {
            InitImageData(width,height,4);
            
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
                palette = new byte[]
                 #region 0 - 64
                {
                    0,0,0,0,   
                    1,1,1,0,   
                    2,2,2,0,   
                    3,3,3,0,   
                    4,4,4,0,   
                    5,5,5,0,   
                    6,6,6,0,   
                    7,7,7,0,   
                    8,8,8,0,   
                    9,9,9,0,   
                    10,10,10,0,
                    11,11,11,0,
                    12,12,12,0,
                    13,13,13,0,
                    14,14,14,0,
                    15,15,15,0,
                    16,16,16,0,
                    17,17,17,0,
                    18,18,18,0,
                    19,19,19,0,
                    20,20,20,0,
                    21,21,21,0,
                    22,22,22,0,
                    23,23,23,0,
                    24,24,24,0,
                    25,25,25,0,
                    26,26,26,0,
                    27,27,27,0,
                    28,28,28,0,
                    29,29,29,0,
                    30,30,30,0,
                    31,31,31,0,
                    32,32,32,0,
                    33,33,33,0,
                    34,34,34,0,
                    35,35,35,0,
                    36,36,36,0,
                    37,37,37,0,
                    38,38,38,0,
                    39,39,39,0,
                    40,40,40,0,
                    41,41,41,0,
                    42,42,42,0,
                    43,43,43,0,
                    44,44,44,0,
                    45,45,45,0,
                    46,46,46,0,
                    47,47,47,0,
                    48,48,48,0,
                    49,49,49,0,
                    50,50,50,0,
                    51,51,51,0,
                    52,52,52,0,
                    53,53,53,0,
                    54,54,54,0,
                    55,55,55,0,
                    56,56,56,0,
                    57,57,57,0,
                    58,58,58,0,
                    59,59,59,0,
                    60,60,60,0,
                    61,61,61,0,
                    62,62,62,0,
                    63,63,63,0
                }
                #endregion
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
            this.Decompose3_RGB_1_4_8(out r,out g,out b);
        }

        public override void Rgb1ToGray(out IImageCore gray)
        {
            IImageCore r,g,b;
            this.Decompose3_RGB_1_4_8(out r,out g,out b);
            this.Rgb3ToGray_8(r,g,b,out gray);
        }
    }
}