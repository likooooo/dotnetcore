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
            // BmpImage rgb24 = new BmpImage();
            // rgb24.ReadImage("Sample/570_554_24.bmp");
            // Console.WriteLine(rgb24.TransToString());
            // rgb24.WriteImage("bin/Debug/net5.0/570_554_24_copy.bmp");
            // rgb24.Dispose();

            //rgb8
            // BmpImage rgb8 = new BmpImage();
            // rgb8.ReadImage("Sample/637_475_8.bmp");
            // Console.WriteLine(rgb8.TransToString());
            // rgb8.WriteImage("bin/Debug/net5.0/637_475_8_copy.bmp");
            // rgb8.Dispose();

            //gray8
            //TODO:

            //Bin
            BmpBinary bin = new BmpBinary();
            bin.ReadImage("Sample/637_475_1.bmp");
            Console.WriteLine(bin.TransToString());
            bin.WriteImage("bin/Debug/net5.0/637_475_1_copy.bmp");
        }

    }
}