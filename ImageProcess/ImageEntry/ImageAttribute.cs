using System;
using System.Linq;
using System.Runtime;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ImageProcess.ImageEntry
{
    //图像属性的扩展方法
    public static class ImageAttribute
    {
        public static bool IsBmp(this ImageCore img,int headVal)
            => headVal == 19778;

        public static bool IsContainColorPalette(this ImageCore img) => img.BitCount < 9;

        public static string TransToString(this ImageCore img) =>JsonSerializer.Serialize(img,new JsonSerializerOptions{WriteIndented = true});
    }
}