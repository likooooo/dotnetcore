using System;
using ImageProcess.ImageEntry;
using ImageProcess.ImageEntry.Bmp;

namespace ImageProcess.Sample
{
    //测试通过
    public static partial class ExtentionOperateSetSample
    {
        public static void ReadWrite_Sample()
        {
            //rgb24
            using(BmpRgb24 rgb24 = new BmpRgb24())
            {             
                rgb24.ReadImage("Sample/570_554_24.bmp");
                Console.WriteLine(rgb24.TransToString());
                rgb24.WriteImage("bin/Debug/net5.0/570_554_24.bmp");
            }
            
            //rgb8
            using(BmpRgb8 rgb8 = new BmpRgb8())
            {
                rgb8.ReadImage("Sample/637_475_8.bmp");
                Console.WriteLine(rgb8.TransToString());
                rgb8.WriteImage("bin/Debug/net5.0/637_475_8.bmp");
            }

            // Bin
            using(BmpRgb1 bin = new BmpRgb1())
            {
                bin.ReadImage("Sample/637_475_1.bmp");
                Console.WriteLine(bin.TransToString());
                bin.WriteImage("bin/Debug/net5.0/637_475_1.bmp");
            }

            //rgb24 copy
            using(BmpRgb24 source = new BmpRgb24())
            {
                source.ReadImage("Sample/570_554_24.bmp");
                BmpRgb24 copy = new BmpRgb24(570,554,source.Scan0);
                copy.WriteImage("bin/Debug/net5.0/570_554_24_copy.bmp");
                copy.Dispose();
            }

            //rgb8 copy
            using(BmpRgb8 source = new BmpRgb8())
            {
                source.ReadImage("Sample/637_475_8.bmp");
                BmpRgb8 copy = new BmpRgb8(637,475,source.Scan0);
                copy.WriteImage("bin/Debug/net5.0/637_475_8_copy.bmp");
                source.WriteImage("bin/Debug/net5.0/637_475_8.bmp");
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
        public static void BmpImageReadWrite_Sample()
        {
            //rgb24
            using(BmpImage rgb24 = new BmpImage())
            {             
                rgb24.ReadImage("Sample/570_554_24.bmp");
                Console.WriteLine(rgb24.TransToString());
                rgb24.WriteImage("bin/Debug/net5.0/570_554_24.bmp");
            }
            
            //rgb8
            using(BmpImage rgb8 = new BmpImage())
            {
                rgb8.ReadImage("Sample/637_475_8.bmp");
                Console.WriteLine(rgb8.TransToString());
                rgb8.WriteImage("bin/Debug/net5.0/637_475_8.bmp");
            }

            // Bin
            using(BmpImage bin = new BmpImage())
            {
                bin.ReadImage("Sample/637_475_1.bmp");
                Console.WriteLine(bin.TransToString());
                bin.WriteImage("bin/Debug/net5.0/637_475_1.bmp");
            }

            //rgb24 copy
            using(BmpImage source = new BmpImage())
            {
                source.ReadImage("Sample/570_554_24.bmp");
                BmpRgb24 copy = new BmpRgb24(570,554,source.Scan0);
                copy.WriteImage("bin/Debug/net5.0/570_554_24_copy.bmp");
                copy.Dispose();
            }

            //rgb8 copy
            using(BmpImage source = new BmpImage())
            {
                source.ReadImage("Sample/637_475_8.bmp");
                BmpRgb8 copy = new BmpRgb8(637,475,source.Scan0);
                copy.WriteImage("bin/Debug/net5.0/637_475_8_copy.bmp");
                source.WriteImage("bin/Debug/net5.0/637_475_8.bmp");
                copy.Dispose();
            }

            using(BmpImage source = new BmpImage())
            {
                source.ReadImage("Sample/637_475_1.bmp");
                BmpRgb1 copy = new BmpRgb1(637,475,source.Scan0);
                copy.WriteImage("bin/Debug/net5.0/637_475_1_copy.bmp");
                copy.Dispose();
            }
        }

        public static void CreateEmptyBmp_Sampe()
        {
              //rgb24
            using(BmpRgb24 rgb24 = new BmpRgb24(570,554))
            {             
                Console.WriteLine(rgb24.TransToString());
                rgb24.WriteImage("bin/Debug/net5.0/570_554_24_empty.bmp");
            }
            
            //rgb8
            using(BmpRgb8 rgb8 = new BmpRgb8(637,475))
            {
                Console.WriteLine(rgb8.TransToString());
                rgb8.WriteImage("bin/Debug/net5.0/637_475_8_empty.bmp");
            }

            // Bin
            using(BmpRgb1 bin = new BmpRgb1(637,475))
            {
                Console.WriteLine(bin.TransToString());
                bin.WriteImage("bin/Debug/net5.0/637_475_1_empty.bmp");
            }
        }
    
    }
}