using System;
using ImageProcess.ImageEntry;
using ImageProcess.ImageEntry.Bmp;

namespace ImageProcess.Sample
{
    //测试通过
    public static partial class ExtentionOperateSetSample
    {
        private static void ShowFileDifrence(string srcFilepath,string desFilepath)
        {
            Console.WriteLine("###");
            var src = System.IO.File.ReadAllBytes(srcFilepath);
            var des =  System.IO.File.ReadAllBytes(desFilepath);
            for(int i = 0;i<src.Length;i++)
            {
                if(src[i]!=des[i])
                {
                    Console.WriteLine(i);
                }
            }
        }
        public static void ReadWrite_Sample()
        {
            //rgb24
            using(BmpRgb24 rgb24 = new BmpRgb24())
            {             
                rgb24.ReadImage("Sample/570_544_24.bmp");
                Console.WriteLine(rgb24.TransToString());
                rgb24.WriteImage("bin/Debug/net5.0/570_544_24.bmp");
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
                source.ReadImage("Sample/570_544_24.bmp");
                BmpRgb24 copy = new BmpRgb24(570,544,source.Scan0);
                Console.WriteLine(copy.TransToString());
                copy.WriteImage("bin/Debug/net5.0/570_544_24_copy.bmp");
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
            // rgb1 copy
            using(BmpRgb1 source = new BmpRgb1())
            {
                source.ReadImage("Sample/637_475_1.bmp");
                BmpRgb1 copy = new BmpRgb1(637,475,source.Scan0);
                Console.WriteLine(source.TransToString());
                Console.WriteLine(copy.TransToString());
                copy.WriteImage("bin/Debug/net5.0/637_475_1_copy.bmp");
                copy.Dispose();
            }
            ShowFileDifrence("Sample/637_475_1.bmp","bin/Debug/net5.0/637_475_1_copy.bmp");
        }
    
        public static void CreateEmptyBmp_Sampe()
        {
              //rgb24
            using(BmpRgb24 rgb24 = new BmpRgb24(570,554))
            {            
                rgb24.InitMemory(); 
                Console.WriteLine(rgb24.TransToString());
                rgb24.WriteImage("bin/Debug/net5.0/570_544_24_empty.bmp");
            }
            
            //rgb8
            using(BmpRgb8 rgb8 = new BmpRgb8(637,475))
            {        
                rgb8.InitMemory(); 
                Console.WriteLine(rgb8.TransToString());
                rgb8.WriteImage("bin/Debug/net5.0/637_475_8_empty.bmp");
            }

            // Bin
            using(BmpRgb1 bin = new BmpRgb1(637,475))
            {        
                bin.InitMemory(); 
                Console.WriteLine(bin.TransToString());
                bin.WriteImage("bin/Debug/net5.0/637_475_1_empty.bmp");
            }
        }
    
    }
}