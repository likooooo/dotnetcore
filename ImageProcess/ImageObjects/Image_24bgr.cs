// using System;
// using System.Text;
// using System.Linq;
// using System.Runtime;
// using System.Collections.Generic;
// using System.Runtime.InteropServices;
// using System.Text.Json;
// using System.Text.Json.Serialization;

// using RuntimeDebug;

// namespace ImageProcess
// {

//     using ImageProcess.ImageExtention;
//     public class Image_24bgr:ImageCore,IImageCore
//     {
//         public Image_24bgr(int width,int height,byte initColor = 0):base(width,height,24,initColor){}
   
//         public override void Decompose3(out IImageCore r,out IImageCore g,out IImageCore b)
//         {
//             this.Decompose3_RGB_16_24_32(out r,out g,out b);
//         }

//         public override void Rgb1ToGray(out IImageCore gray)
//         {
//             base.Rgb1ToGray(out gray);
//         }
//     }
// }