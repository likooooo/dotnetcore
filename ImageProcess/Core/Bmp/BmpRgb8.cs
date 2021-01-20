namespace ImageProcess.Core.Bmp
{
    public class BmpRgb8:BmpImage
    {
        public BmpRgb8():base(){}
        public BmpRgb8(int width,int height):base(width,height,8,ImageTypeData.rgb)
        {
        }
    }
}