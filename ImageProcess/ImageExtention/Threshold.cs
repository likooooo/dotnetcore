using System;
using System.Linq;
using System.Runtime;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace ImageProcess.ImageExtention
{
    public static unsafe partial class ExtentionOperateSet
    {
        //灰度图阈值分割，输入8位灰度图，返回8位二值图
        public static void Threshold_8(this IImageCore img, byte minVal, out IImageCore binaryImg)
        {
            binaryImg = new Image_8BitGray(img.Width,img.Height);
            byte* grayPtr = (byte*)img.Scan0.ToPointer();
            byte* binptr = (byte*)binaryImg.Scan0.ToPointer();
            int skip = img.SkipByteCount;
      
            int idx = -1;
            int loopCount = img.Width*img.Height;   
            int endLineFlg = 0;

            while (++idx<loopCount)
            {     
                endLineFlg ++;
                if(*grayPtr++ >= minVal) 
                {
                    *binptr = 255;
                } 
                binptr++;
                if(endLineFlg == img.Width)
                {
                    grayPtr += skip;
                    binptr += skip;
                    endLineFlg = 0;
                }
            }
        }
    }
}