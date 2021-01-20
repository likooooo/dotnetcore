namespace ImageProcess.Core.Bmp
{
    public class BmpBinary:BmpImage
    {
        public BmpBinary():base(){}
        public BmpBinary(int width,int height):base(width,height,1,ImageTypeData.rgb)
        {
        }
    }
}