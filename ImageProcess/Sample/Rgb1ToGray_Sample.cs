// using RuntimeDebug;

// namespace ImageProcess.Sample
// {
  
//     public static partial class ExtentionOperateSetSample
//     {
//         public static void Rgb1ToGray_Sample()
//         {
//             DebugMsg.DebugConsoleOut("Rgb1ToGray_Sample");
//             ImageCore bmp = new ImageCore();
//             bmp.ReadImage("Sample/570_554_24.bmp");
//             IImageCore gray;
//             bmp.Rgb1ToGray(out gray);
//             gray.WriteImage("bin/Debug/net5.0/570_554_24_gray.bmp");
//             gray.Dispose();
//             bmp.Dispose();
//         }

//     }
// }