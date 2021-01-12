using System;
using System.Runtime;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace ImageProcess
{
    public enum PixelFormats
    {
        Gray8 = 8,
        Rgb24 = 24
    }

    public class ImageInfo
    {
        public int Width{get;protected set;}
        public int Height{get;protected set;}
        public int Depth{get;private set;}
        public int Stride{get;private set;}
        public IntPtr Handle{get;private set;}

        public int DataCount{get;private set;}

        #region 构造函数
        public ImageInfo(){}
        public ImageInfo(int width,int height,int depth)
        {
            Width = width;
            Height = height;
            Depth = depth;
            Stride = width * depth;
            Stride = Stride + Stride%4>0?1:0;
            DataCount = width*height*Depth;
            Handle = Marshal.AllocHGlobal(DataCount);
        }
        
        /// <summary>
        /// 参数bmp不能释放
        /// </summary>
        /// <param name="bmp"></param>
        public ImageInfo(Bitmap bmp)
        {
            Width = bmp.Width;
            Height =  bmp.Height;
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            BitmapData bmpData =bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,bmp.PixelFormat);
            Depth = Image.GetPixelFormatSize(bmpData.PixelFormat)/8;
            Stride =bmpData.Stride;
            Handle = bmpData.Scan0;
            DataCount = Width*Height*Depth;
            bmp.UnlockBits(bmpData);
        }
        #endregion
        public void CreateSpan(ref Span<byte> span)
        {
            unsafe
            {
                span = new Span<byte>(Handle.ToPointer(), DataCount);
            }
        }

        public void Dispose(){Marshal.FreeHGlobal(Handle);}

        public ImageInfo Clone()
        {
            ImageInfo res = new ImageInfo(); 
            res.Width = Width;
            res.Height = Height;
            res.Depth = Depth;
            res.Stride = Stride;
            Span<byte> span = default(Span<byte>);
            CreateSpan(ref span);
            res.Handle = IntPtr.Zero;
            Marshal.Copy(span.ToArray(), 0, res.Handle, span.Length);
            return res;
        }
    }      
}
