using System;
using ImageProcess.ImageEntry;
using ImageProcess.ImageEntry.Bmp;

namespace ImageProcess.Sample
{
    public static partial class ExtentionOperateSetSample
    {
        public static void BmpMatConvet_Sample()
        {
            //rgb24
            using(BmpRgb24 rgb24 = new BmpRgb24())
            {
                rgb24.ReadImage("Sample/570_554_24.bmp");
                Mat mat = rgb24.TransBmpToMat();
                rgb24.TransMatToBmp(mat);
                rgb24.WriteImage("bin/Debug/net5.0/637_475_24_mat.bmp");
            }
            //rgb8
            using(BmpRgb8 rgb8 = new BmpRgb8())
            {
                rgb8.ReadImage("Sample/637_475_8.bmp");
                Mat mat = rgb8.TransBmpToMat();
                rgb8.TransMatToBmp(mat);
                rgb8.WriteImage("bin/Debug/net5.0/637_475_8_mat.bmp");
            }
            // gray8
            using(BmpGray8 gray8 = new BmpGray8())
            {
                gray8.ReadImage("Sample/637_475_8.bmp");
                //由于读取的图片本身是8位伪彩色图，因此需要重新刷新调色盘
                gray8.SetPalette(ColorPalette.GrayPalette_256);
                Mat mat = gray8.TransBmpToMat();
                gray8.TransMatToBmp(mat);
                gray8.WriteImage("bin/Debug/net5.0/637_475_8_gray_mat.bmp");
            }
            //rgb1
            using(BmpRgb1 rgb1 = new BmpRgb1())
            {
                rgb1.ReadImage("Sample/637_475_1.bmp");
                // Mat mat = rgb1.TransBmpToMat();
                // rgb1.TransMatToBmp(mat);
                rgb1.WriteImage("bin/Debug/net5.0/637_475_1_mat.bmp");
            }
            Console.WriteLine("End");
        }
    }
}