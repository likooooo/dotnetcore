// #define GrayPrecision_16
// using System;
// using System.Linq;
// using System.Runtime;
// using System.Collections.Generic;
// using System.Runtime.InteropServices;

// namespace ImageProcess.ImageExtention
// {
    
//     public static unsafe partial class ExtentionOperateSet
//     {
//         //8位rgb的像素值==调色盘下标==调色盘的值，不需要获取调色盘
//         public static void Rgb3ToGray_8(this IImageCore img,IImageCore r,IImageCore g,IImageCore b,out IImageCore gray)
//         {
//             gray = new ImageCore(r.Width,r.Height,r.BitCount);
//             byte* rptr = (byte*)r.Scan0.ToPointer();
//             byte* gptr = (byte*)g.Scan0.ToPointer();
//             byte* bptr = (byte*)b.Scan0.ToPointer();
//             byte* grayPtr = (byte*)gray.Scan0.ToPointer();

//             int graySkip = gray.SkipByteCount;
      
//             int idx = -1;
//             int loopCount = gray.Width*gray.Height;   
//             int endLineFlg = 0;

//             while (++idx<loopCount)
//             {
//                 #if GrayPrecision_16
//                     *grayPtr++ = (byte)(((*rptr++)*19595 + (*gptr++)*38469 + (*bptr++)*7472)>>16);
//                 #elif GrayPrecision_8
//                     *grayPtr++ = (byte)(((*rptr++)*76 + (*gptr++)*150 + (*bptr++)*30)>>8);
//                 #endif           
//                 endLineFlg ++;
//                 if(endLineFlg == img.Width)
//                 {
//                     endLineFlg = 0;
//                     grayPtr += graySkip;
//                     rptr += graySkip;
//                     gptr += graySkip;
//                     bptr += graySkip;
//                 }
//             }
//         }
    
//         //24位转8位灰度图
//         public static void Rgb1ToGray_24(this IImageCore img,out IImageCore grayImage)
//         {
//             grayImage = new ImageCore(img.Width,img.Height,8);
//             byte* grayPtr = (byte*)grayImage.Scan0.ToPointer();
//             byte* rgbPtr = (byte*)img.Scan0.ToPointer();
//             //实际字节 - 理论字节
//             int rgbSkip =img.Stride - img.Width*3;
//             int graySkip = grayImage.Stride- grayImage.Width;

//             int loopCount = grayImage.Width*grayImage.Height;
//             int idx = -1;
//             int endLineFlg = 0;
//             while (++idx<loopCount)
//             {
//                  #if GrayPrecision_16
//                     *grayPtr++ = (byte)(((*rgbPtr++)*19595 + (*rgbPtr++)*38469 + (*rgbPtr++)*7472)>>16);
//                 #elif GrayPrecision_8
//                     *grayPtr++ = (byte)(((*rgbPtr++)*76 + (*rgbPtr++)*150 + (*rgbPtr++)*30)>>8);
//                 #endif
                
//                 endLineFlg ++;
//                 if(endLineFlg == img.Width)
//                 {
//                     endLineFlg = 0;
//                     grayPtr += graySkip;
//                     rgbPtr += rgbSkip;
//                 }
//             }

//             // int loopCount = grayImage.Width*grayImage.Height;
//             // int grayOffset = 0;
//             // int rgbOffset = 0;
//             // for (int i = 0; i < loopCount; i++ )
//             // {
//             //     grayOffset = (grayImage.Stride * i/grayImage.Width)>>8 +i%grayImage.Width;
//             //     rgbOffset = ((img.Stride * i/img.Width)>>8 + i%img.Width)>>2;

//             //     *(grayPtr + grayOffset) = (byte)(((*(rgbPtr+rgbOffset))*19595 + (*(rgbPtr+rgbOffset +1 ))*38469+ (*(rgbPtr+rgbOffset+2))*7472)>>16);
//             // }
//         }
    

//     }
// }