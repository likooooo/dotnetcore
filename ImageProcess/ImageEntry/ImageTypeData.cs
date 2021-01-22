namespace ImageProcess.ImageEntry
{
    public static class ImageTypeData
    {
        //压缩方式
        public const int rgb = 0;
        public const int rle_8bpp = 1;
        public const int rle_4bpp = 2;
        public const int bitFields = 3;

        public const int bmpFileHead = 19778; //bm 4D 42
        public const int jpgFileHead = 65496; //FFD8    
    }
}