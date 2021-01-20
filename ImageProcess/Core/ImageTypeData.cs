namespace ImageProcess.Core
{
    public static class ImageTypeData
    {
        //压缩方式
        public const int rgb = 0;
        public const int rle_8bpp = 1;
        public const int rle_4bpp = 2;
        public const int bitFields = 3;

        public static bool IsRgbImage(this ImageFileCommon img) => img.Compression == rgb;
        public static bool IsGrayImage(this ImageFileCommon img) => img.Compression == rle_8bpp;
        public static bool IsRegion(this ImageFileCommon img) => img.Compression == rle_4bpp;

        public const int bmpFileHead = 19778; //bm 4D 42
        public const int jpgFileHead = 65496; //FFD8    
        public static bool IsBmp(this ImageFileCommon img,int headVal) => headVal == bmpFileHead;
        public static bool IsJpg(this ImageFileCommon img,int headVal) => headVal == jpgFileHead;
    }
}