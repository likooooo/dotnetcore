using System;
using System.Linq;
using System.Runtime;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections.Generic;
using System.Runtime.InteropServices;
namespace ImageProcess
{
    public class ImageStream
    {
        [DllImport("kernel32.dll", EntryPoint = "CopyMemory", SetLastError = false)]
        public static extern void CopyMemory(IntPtr dest, IntPtr src, uint count);
        public static void ReadImage(string filepah,out ImageInfo image)
        {
            if(!System.IO.File.Exists(filepah))
            {
                throw new Exception("图片路径不存中");
            }
            var bmp = new Bitmap(filepah);
            image = new ImageInfo(bmp);           
        }
        
        private static Dictionary<byte,(int r,int g,int b)> RGB2GrayTable = new Dictionary<byte, (int r, int g, int b)>() 
        {
            {8,(76,150,37)},
            {16,(19595,38469,7472)}
        }; 
        /*
        8位 
        0.299 * 256 = 76.544 -0.544 = 76 
        0.587 * 256 + (0.544) = 150.816 -0.816 = 150
        0.114 * 256 + (0.816) =  37.68 + 0.68 = 37
        */
        /*
        16位 
        0.299 * 65536 = 19595.264 ≈ 19595
        0.587 * 65536 + (0.264) = 38469.632 + 0.264 = 38469.896 ≈ 38469
        0.114 * 65536 + (0.896) =   7471.104 + 0.896 = 7472
        */
       
        public static void RGB2Gray(ImageInfo image,out ImageInfo grayImage)
        {
            int pixelCount = image.Width*image.Height;
            if(image.Depth == 3)
            {
                grayImage = new ImageInfo(image.Width,image.Height,1);
                unsafe
                {
                    byte* dest = (byte*)grayImage.Handle.ToPointer();
                    byte* source = (byte*)image.Handle.ToPointer();
                    int idx = 0;
                    while(idx<pixelCount)
                    {
                        (byte r,byte g,byte b) pixel =
                        (
                            (* (source++) ),(* (source++) ),(* (source++) )
                        );
                        *dest =  (byte)((pixel.r*RGB2GrayTable[8].r +pixel.g*RGB2GrayTable[8].g +pixel.b*RGB2GrayTable[8].b) >>8);
                        dest++;
                        ++idx;
                    }               
                }              
            }
            else
            {
                grayImage = null;
            }
        }

        //https://docs.microsoft.com/zh-cn/dotnet/api/system.drawing.imaging.pixelformat?view=net-5.0
        static Dictionary<int,PixelFormat> PixelFormats = new Dictionary<int, PixelFormat>()
        {
            {3,PixelFormat.Format24bppRgb},
            {1,PixelFormat.Format8bppIndexed}
        };
        public static void WriteImage(ImageInfo image,string filePath)
        {
            Rectangle rect = new Rectangle(0, 0, image.Width,image.Height);
            using(Bitmap bmp = new Bitmap(image.Width,image.Height))
            {
                BitmapData bmpData =bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,PixelFormats[image.Depth]);             
                CopyMemory(bmpData.Scan0, image.Handle, (uint)image.DataCount);
                bmp.UnlockBits(bmpData);
                //保存到磁盘文件
                bmp.Save(filePath);
            }
        }
    }
}
