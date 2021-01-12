using System;
using System.Drawing;
//dotnet add package System.Drawing.Common
namespace ImageProcess
{
    class Program
    {
        static void Main(string[] args)
        {
            ImageInfo img = default(ImageInfo);
            ImageStream.ReadImage(@"C:\Users\like\Pictures\1.bmp",out img);
            ImageInfo gray = default(ImageInfo);
            ImageStream.RGB2Gray(img,out gray);
            ImageStream.WriteImage(gray,@"C:\Users\like\Pictures\1_gray.bmp");
            ImageStream.WriteImage(img,@"C:\Users\like\Pictures\1_copy.bmp");
            Console.WriteLine("Hello World!");
        }
    }
}
