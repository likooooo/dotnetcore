using System;
using System.Drawing;
using ImageProcess.Sample;

namespace ImageProcess
{
    class Program
    {
        static void Main(string[] args)
        {
            ExtentionOperateSetSample.Threshold_Sample();
        }

        static void CreateImage()
        {
            
            ImageCore bmp = new ImageCore(300,200,24);
            bmp.WriteImage("bin/Debug/net5.0/300_200_24_black.bmp");
            unsafe
            {
                Span<byte> span = new Span<byte>(bmp.Scan0.ToPointer(),(int)bmp.BitmapSize);
                Console.WriteLine($"{bmp.Scan0},{bmp.BitmapSize},{bmp.Stride},{bmp.Height}");
                for(int i = 0;i<bmp.BitmapSize;i++)
                {
                    span[i]=(byte)(i%255);
                }  
                bmp.WriteImage("bin/Debug/net5.0/300_200_24_wave.bmp");
                for(int i = 0;i<bmp.Height;i++)
                {
                    span.Slice(i*bmp.Stride,bmp.Stride).Fill((byte)i);
                }  
                bmp.WriteImage("bin/Debug/net5.0/300_200_24_Line.bmp");
            }    

        }

        static void ImageObjectTest()
        { 
            Image_8BitGray gray = new Image_8BitGray(300,200);
            // using(ImageCore black = new ImageCore(300,200,1))
            // {
            //     black.WriteImage("bin/Debug/net5.0/300_200_Binaray_Black.bmp");
            // }
            // using(ImageCore white = new ImageCore(300,200,1,255))
            // {
            //     white.WriteImage("bin/Debug/net5.0/300_200_Binaray_White.bmp");
            // }
            // using(ImageCore gray = new ImageCore(300,200,24))
            // {
            //     Span<byte> scan0;
            //     unsafe
            //     {
            //        scan0 = new Span<byte>(gray.Scan0.ToPointer(),gray.BitmapSize);
            //     } 
            //     for (int i = 0; i < gray.Height; i++)
            //     {
            //         scan0.Slice(i*gray.Stride,gray.Stride).Fill((byte)i);
            //     }
            //     gray.WriteImage("bin/Debug/net5.0/300_200_24_gray.bmp");
            // }
        }

        //通过disk->memory->disk测试了读写的正确性
        static void ReadWriteImage()
        {      
            ImageCore bmp = new ImageCore();
            bmp.ReadImage("bin/Debug/net5.0/300_200_24_gray.bmp");
            bmp.WriteImage("bin/Debug/net5.0/300_200_24_gray_copy.bmp");
        }
    }
}
