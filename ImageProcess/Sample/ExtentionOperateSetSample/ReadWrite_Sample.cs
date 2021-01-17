namespace ImageProcess.Sample
{
    public static partial class ExtentionOperateSetSample
    {
        public static void ReadWrite_Sample()
        {
            ImageCore bmp = new ImageCore();
            bmp.ReadImage("Sample/570_554_24.bmp");
            bmp.WriteImage("bin/Debug/net5.0/570_554_24_copy.bmp");
        }

    }
}