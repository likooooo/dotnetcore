using System;
using System.Drawing;
//dotnet add package System.Drawing.Common
namespace ImageProcess
{
    class Program
    {
        static void Main(string[] args)
        {
            // ImageCore bmp = new ImageCore();
            // Span<byte> span;
            // bmp.ReadImage(@"C:\Users\like\Pictures\8位Bmp.bmp",out span);
            // //span.Fill(128);
            // bmp.WriteImage(@"C:\Users\like\Pictures\8位Bmp_copy.bmp");
            ImageObjectTest();
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
            using(ImageCore black = new ImageCore(300,200,1))
            {
                black.WriteImage("bin/Debug/net5.0/300_200_Binaray_Black.bmp");
            }
            using(ImageCore white = new ImageCore(300,200,1,255))
            {
                white.WriteImage("bin/Debug/net5.0/300_200_Binaray_White.bmp");
            }
            using(ImageCore black = new ImageCore(300,200,8))
            {
                black.WriteImage("bin/Debug/net5.0/300_200_8_Black.bmp");
            }
            using(ImageCore white = new ImageCore(300,200,8,255))
            {
                white.WriteImage("bin/Debug/net5.0/300_200_8_White.bmp");
            }
        }
    }
}
