using RuntimeDebug;

namespace ImageProcess.Sample
{
    public static partial class ExtentionOperateSetSample
    {
        public static void Threshold_Sample()
        {
            ImageCore bmp = new ImageCore();
            bmp.ReadImage("Sample/570_554_24.bmp");
            IImageCore gray;
            bmp.Rgb1ToGray(out gray);
            IImageCore bin;
            gray.Threshold(200,out bin);
            bin.WriteImage("bin/Debug/net5.0/570_554_24_Threshold.bmp");
            DebugMsg.DebugConsoleOut("Threshold_Sample end");
            bin.Dispose();
            gray.Dispose();
            bmp.Dispose();
        }

    }
}