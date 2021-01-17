using System;
using System.Runtime.InteropServices;

namespace ImageProcess.Sample
{
  
    public static partial class ExtentionOperateSetSample
    {
        public static void Decompose3_Sample()
        {
            ImageCore bmp = new ImageCore();
            IImageCore r,g,b;
            //24位
            // bmp.ReadImage("Sample/570_554_24.bmp");
            // bmp.Decompose3(out r,out g,out b);
            // r.WriteImage("bin/Debug/net5.0/570_554_24_r.bmp");
            // g.WriteImage("bin/Debug/net5.0/570_554_24_g.bmp");
            // b.WriteImage("bin/Debug/net5.0/570_554_24_b.bmp");
            //8位rgb
            bmp.ReadImage("Sample/637_475_8.bmp");
            bmp.Decompose3(out r,out g,out b);
            r.WriteImage("bin/Debug/net5.0/637_475_8_r.bmp");
            g.WriteImage("bin/Debug/net5.0/637_475_8_g.bmp");
            b.WriteImage("bin/Debug/net5.0/637_475_8_b.bmp");
        }

    }
}