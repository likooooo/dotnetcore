using System;
using System.Runtime.InteropServices;
using ImageProcess.ImageEntry.Bmp;
using ImageProcess.ImageEntry.RLC;

namespace ImageProcess.Sample
{
  
    public static partial class ExtentionOperateSetSample
    {
        public static void PreProcess_Sample()
        {
            ImageCore bmp = new BmpRgb24();
            BmpGray8 r,g,b,gray,region,region1;
            RLC_NodeList<byte> rlc;
            //24位
            bmp.ReadImage("Sample/570_544_24.bmp");
            bmp.WriteImage("bin/Debug/net5.0/570_544_24_copy.bmp");
            IOperatorSet<byte>.Decompose3(bmp,out r,out g,out b);   
            IOperatorSet<byte>.Rgb3ToGray(r,g,b,out gray);
            IOperatorSet<byte>.Threshold(gray,out region,200,255);
            IOperatorSet<byte>.Threshold(gray,out rlc,200,255);
            region1 = BmpExtention.RLCListToImage(rlc,gray.Width,gray.Height);
            region1.WriteImage("bin/Debug/net5.0/570_554_24_region1.bmp");
            region.WriteImage("bin/Debug/net5.0/570_554_24_region.bmp");
            ShowFileDifrence("bin/Debug/net5.0/570_554_24_region.bmp","bin/Debug/net5.0/570_554_24_region1.bmp");
            gray.WriteImage("bin/Debug/net5.0/570_554_24_gray.bmp");
            r.WriteImage("bin/Debug/net5.0/570_554_24_r.bmp");
            g.WriteImage("bin/Debug/net5.0/570_554_24_g.bmp");
            b.WriteImage("bin/Debug/net5.0/570_554_24_b.bmp");
            
            b.Dispose();
            g.Dispose();
            r.Dispose();
            gray.Dispose();
            region.Dispose();
            bmp.Dispose();
            Console.WriteLine("End Decompose3 Test");

            // // //8位rgb
            // ImageCore rgb8 = new BmpRgb24();
            // rgb8.ReadImage("Sample/637_475_8.bmp");
            // IOperatorSet<byte>.Decompose3(rgb8,out r,out g,out b);   
            // r.WriteImage("bin/Debug/net5.0/637_475_8_r.bmp");
            // g.WriteImage("bin/Debug/net5.0/637_475_8_g.bmp");
            // b.WriteImage("bin/Debug/net5.0/637_475_8_b.bmp");
        }

    }
}