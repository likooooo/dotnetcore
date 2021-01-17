using System;
using System.Linq;
using System.Runtime;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace ImageProcess.ImageExtention
{
    //图像属性的扩展方法
    public static class ImageAttribute
    {
        public static bool IsBmp(this ImageCore img)
            => img.FileType == 19778;

        public static bool IsContainColorPalette(this ImageCore img) => img.BitCount < 9;
    }
}