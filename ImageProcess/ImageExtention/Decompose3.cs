using System;
using System.Linq;
using System.Runtime;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace ImageProcess.ImageExtention
{
    //图像处理扩展方法,没有左数据合法判断
    [ComVisible(false)]
    public static unsafe partial class ExtentionOperateSet
    {
        //1,4,8位RGB图片Decompose3的实现
        public static void Decompose3_RGB_1_4_8(this IImageCore img,out IImageCore r,out IImageCore g,out IImageCore b)
        {
            b = new Image_8BitGray(img.Width,img.Height);
            g = new Image_8BitGray(img.Width,img.Height);
            r = new Image_8BitGray(img.Width,img.Height);

            #region 调色盘赋值
            byte* p  = (byte*)img.Palette.ToPointer();
            byte* bp = (byte*)b.Palette.ToPointer();
            byte* gp = (byte*)g.Palette.ToPointer();
            byte* rp = (byte*)r.Palette.ToPointer();
            for (int i = 0; i < b.PaletteSize; i++)
            {
                *rp++ = *p; 
                *rp++ = *p; 
                *rp++ = *p; 
                rp+=2;

                p++;
                *gp++ = *p; 
                *gp++ = *p; 
                *gp++ = *p; 
                gp+=2;

                p++;
                *bp++ = *p; 
                *bp++ = *p; 
                *bp++ = *p; 
                bp+=2;

                p+=2;
            }
            #endregion

            #region bitmap赋值   
            byte[] d = new byte[img.BitmapSize];
            Marshal.Copy(img.Scan0,d,0,img.BitmapSize);
            Marshal.Copy(d, 0, r.Scan0, r.BitmapSize);
            Marshal.Copy(d, 0, g.Scan0, g.BitmapSize);
            Marshal.Copy(d, 0, b.Scan0, b.BitmapSize);
            #endregion
        }

        //16，24，32位RGB图片Decompose3的实现
        public static void Decompose3_RGB_16_24_32(this IImageCore img,out IImageCore r,out IImageCore g,out IImageCore b)
        {        
            r = new Image_8BitGray(img.Width,img.Height);
            g = new Image_8BitGray(img.Width,img.Height);
            b = new Image_8BitGray(img.Width,img.Height);        
            //实际字节 - 理论字节
            int rgbSkip = img.SkipByteCount;
            int graySkip = r.SkipByteCount;

            byte* bp = (byte*)b.Scan0.ToPointer();
            byte* gp = (byte*)g.Scan0.ToPointer();
            byte* rp = (byte*)r.Scan0.ToPointer();
            byte* p = (byte*)img.Scan0.ToPointer();
            
            int idx = -1;
            int loopCount = img.Width* img.Height;   
            int endLineFlg = 0;

            while (++idx<loopCount)
            {
                *bp++ = *p++;
                *gp++ = *p++;
                *rp++ = *p++;          
                endLineFlg ++;
                if(endLineFlg == img.Width)
                {
                    endLineFlg = 0;

                    p += rgbSkip;
                    bp += graySkip;
                    gp += graySkip;
                    rp += graySkip;
                }
            }
             Console.WriteLine("Decompose3_RGB_16_24_32 end");    
        }
    }
}