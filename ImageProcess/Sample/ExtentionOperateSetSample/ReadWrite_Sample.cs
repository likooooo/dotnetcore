using System;
using ImageProcess.Core;
using ImageProcess.Core.Bmp;

namespace ImageProcess.Sample
{
    public static partial class ExtentionOperateSetSample
    {
        public static void ReadWrite_Sample()
        {
            //rgb24
            using(BmpRgb24 rgb24 = new BmpRgb24())
            {             
                rgb24.ReadImage("Sample/570_554_24.bmp");
                Console.WriteLine(rgb24.TransToString());
                rgb24.WriteImage("bin/Debug/net5.0/570_554_24_copy1.bmp");
            }
            
            //rgb8
            using(BmpRgb8 rgb8 = new BmpRgb8())
            {
                rgb8.ReadImage("Sample/637_475_8.bmp");
                Console.WriteLine(rgb8.TransToString());
                rgb8.WriteImage("bin/Debug/net5.0/637_475_8_copy.bmp");
            }


            // Bin
            using(BmpRgb1 bin = new BmpRgb1())
            {
                bin.ReadImage("Sample/637_475_1.bmp");
                Console.WriteLine(bin.TransToString());
                bin.WriteImage("bin/Debug/net5.0/637_475_1_copy.bmp");
            }

            //rgb8 copy
            using(BmpRgb8 source = new BmpRgb8())
            {
                source.ReadImage("Sample/637_475_8.bmp");
                BmpRgb8 copy = new BmpRgb8(637,475,source.Scan0);
                copy.WriteImage("bin/Debug/net5.0/637_475_8_copy.bmp");
                source.WriteImage("bin/Debug/net5.0/637_475_8_copy1.bmp");
                copy.Dispose();
            }

            using(BmpRgb1 source = new BmpRgb1())
            {
                source.ReadImage("Sample/637_475_1.bmp");
                BmpRgb1 copy = new BmpRgb1(637,475,source.Scan0);
                copy.WriteImage("bin/Debug/net5.0/637_475_1_copy.bmp");
                copy.Dispose();
            }
        }

    }
}