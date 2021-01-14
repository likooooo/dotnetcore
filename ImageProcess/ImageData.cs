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
    public struct ImageType
    {
        [JsonInclude]
        public ushort bfType;

        public override string ToString()
        {
            return JsonSerializer.Serialize(this,new JsonSerializerOptions{WriteIndented = true});
        }
    }

    [Serializable]
    public struct ImageFile
    {
        [JsonInclude]
        public uint bfSize;

        [JsonInclude]
        public uint bfReserved;

        [JsonInclude]
        public uint bfOffBits;

        public override string ToString()
        {
            return JsonSerializer.Serialize(this,new JsonSerializerOptions{WriteIndented = true});
        }
    }

    [Serializable]
    public struct ImageFileInfo
    {
        [JsonInclude]
        public uint biSize;

        [JsonInclude]
        public uint biWidth;

        [JsonInclude]
        public uint biHeight;

        //两个 ushort 在字节对齐的时候会放到一起
        [JsonInclude]
        public ushort biPlanes;
        [JsonInclude]
        public ushort biBitCount;

        [JsonInclude]
        public uint biCompression;

        [JsonInclude]
        public uint biSizeImage;

        [JsonInclude]
        public uint biXPelsPerMeter;

        [JsonInclude]
        public uint biYPelsPerMeter;

        [JsonInclude]
        public uint biClrUsed;
        
        [JsonInclude]
        public uint biClrImportant;

        public override string ToString()
        {
            return JsonSerializer.Serialize(this,new JsonSerializerOptions{WriteIndented = true});
        }
    }
    
   #region 用于适配老版本图片，本代码不支持
    [Serializable]
    public struct Section 
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
   #endregion

    [Serializable]
    public struct ColorPalette
    {
        public byte[] palette;

        // //1位二值图
        // public static ColorPalette GetBinaryImagePalette()
        // {
        //     ColorPalette res = new ColorPalette();
        //     res.Count = 8;
        //     res.palette = new byte[]//8
        //     {
        //         0,0,0,0,
        //         1,1,1,0
        //     };
        //     return res;
        // }

        // //4位灰度图
        // public static ColorPalette Get4BitCountPalette()
        // {
        //     ColorPalette res = new ColorPalette();
        //     res.Count = (1<<4)*4;
        //     res.palette = new byte[res.Count];//128

        //     res.palette[0] = 0;
        //     res.palette[1] = 0;
        //     res.palette[2] = 0;
        //     res.palette[3] = 0;
        //     for (int i = 1; i < 8; i++)
        //     {
        //         res.palette[i*4]   = (byte)(32*i -1);
        //         res.palette[i*4+1] = (byte)(32*i -1);
        //         res.palette[i*4+2] = (byte)(32*i -1);
        //         res.palette[i*4+3] = 0;
        //     }
        //     return res;
        // }

        // //8位灰度图调色盘
        // public static ColorPalette GetGrayPalette()
        // {
        //     ColorPalette res = new ColorPalette();
        //     res.Count = (1<<8)*4;
        //     res.palette = new byte[res.Count];//1024

        //     for (int i = 0; i < 256; i++)
        //     {
        //         res.palette[i*4]   = (byte)i;
        //         res.palette[i*4+1] = (byte)i;
        //         res.palette[i*4+2] = (byte)i;
        //         res.palette[i*4+3] = 0;
        //     }
        //     return res;
        // }

        // //8位彩色图调色盘
        // public static ColorPalette GetColorPalette()
        // {
        //     ColorPalette res = new ColorPalette();
        //     res.Count = (1<<8)*4;
        //     res.palette = new byte[res.Count];//1024

        //     for (byte i = 0; i <= 255; i++)
        //     {
        //         res.palette[i]   = i;
        //         res.palette[i+1] = i;
        //         res.palette[i+2] = i;
        //         res.palette[i+3] = 0;
        //     }
        //     return res;
        // }
    }

    [Serializable]
    public struct ImageData
    {
        public byte[] D{get;set;}

        public override string ToString()
        {
            return JsonSerializer.Serialize(this,new JsonSerializerOptions{WriteIndented = true});
        }
    }
}