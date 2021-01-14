using System;
using System.Drawing;
//dotnet add package System.Drawing.Common
namespace ImageProcess
{
    class Program
    {
        static void Main(string[] args)
        {
            ImageCore bmp = new ImageCore();
            // bmp.CreateImage(1024,720,8);
            // Console.WriteLine(bmp.WriteImage(@"C:\Users\like\Pictures\1024_720_8.bmp"));
            // bmp = new BmpImage();
            // bmp.CreateImage(1024,720,24);
            // Console.WriteLine(bmp.WriteImage(@"C:\Users\like\Pictures\1024_720_24.bmp"));
            Span<byte> span;
            bmp.ReadImage(@"C:\Users\like\Pictures\8位Bmp.bmp",out span);
            //span.Fill(128);
            bmp.WriteImage(@"C:\Users\like\Pictures\8位Bmp_copy.bmp");
        }
    }
}
