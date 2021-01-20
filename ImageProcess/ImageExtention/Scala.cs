using System;
using System.Linq;
using System.Runtime;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace ImageProcess.ImageExtention
{
    public static unsafe partial class ExtentionOperateSet
    {
        //输入8位灰度图，返回8位伸缩后的结果
        //mult < 0 : 增强暗画面
        //mult > 0 : 增强亮画面
        public static void ScalaImage_8(this IImageCore img, int mult,int add, out IImageCore scalaedImage)
        {
            scalaedImage = new Image_8BitGray(img.Width,img.Height);
            byte* p = (byte*)img.Scan0.ToPointer();
            byte*  scalaedPtr = (byte*)scalaedImage.Scan0.ToPointer();
            int skip = img.SkipByteCount;
      
            int idx = -1;
            int loopCount = img.Width*img.Height;   
            int endLineFlg = 0;

            while (++idx<loopCount)
            {     
                endLineFlg ++;
                *scalaedPtr++ = (byte)(mult*(*p++) + add);
                if(endLineFlg == img.Width)
                {
                    p += skip;
                    scalaedPtr += skip;
                    endLineFlg = 0;
                }
            }
        }
    }
}