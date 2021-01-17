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
    // 0 —— RGB方式
    // 1 —— 8bpp的run-length-encoding方式
    // 2 —— 4bpp的run-length-encoding方式
    // 3 —— bit-fields方式
    public enum ImageCompressionType
    {
        rgb = 0,
        rle_8bpp,
        rle_4bpp,
        bitFields
    }  
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
        public int biWidth;

        [JsonInclude]
        public int biHeight;

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