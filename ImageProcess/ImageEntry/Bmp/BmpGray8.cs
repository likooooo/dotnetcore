using System;
using System.Runtime.InteropServices;

namespace ImageProcess.ImageEntry.Bmp
{
    public class BmpGray8:BmpImage
    {
        public BmpGray8():base(){}
        public BmpGray8(int width,int height):base(width,height,8,ImageTypeData.rgb)
        {
            Palette = ColorPalette.GrayPalette_256;
        }

        public unsafe BmpGray8(int width,int height,IntPtr mat):this(width,height)
        {
            memcpy(Scan0,mat,new UIntPtr((uint)Count));
        }

        public override unsafe void ReadImage(string filepath)
        {
            byte[] data = System.IO.File.ReadAllBytes(filepath);
            IntPtr p = Marshal.AllocHGlobal(data.Length);
            Marshal.Copy(data,0,p,data.Length);
            ushort* shortHead = (ushort*)p.ToPointer();
            if(!this.IsBmp(*shortHead++))
            {
                Console.WriteLine($"Readed File NOT BMP File:{filepath}");
                return;
            }
            int* intHead = (int*)shortHead;
            
            FileBytesSize = *intHead++;
            intHead++;
            HeadStructSize = *intHead++;
            intHead++;//biSize
            Width = *intHead++;
            Height = *intHead++;
            BitCount = (ushort)( (*intHead++)>>16);
            Compression = *intHead++;
            if((BitCount != 8)||(Compression != ImageTypeData.rgb))
            {
                throw new Exception("Target file is NO Rgb8");
            }
            intHead++; //FileBytesSize - HeadStructSize
            XPelsPermeter = *intHead++;
            YPelsPermeter = *intHead++;
            RefrenceColorCount = *intHead++;
            ImportantColorCount = *intHead++;
            Palette = new byte[1024];
            byte* byteHead = (byte*)intHead;
            for(int i =0;i<Palette.Length;i++)
            {
                Palette[i] = *byteHead++;
            }
            Marshal.FreeHGlobal(p);

            Stride = ((Width*BitCount + 31)>>5)<<2;
            RankBytesCount = (Width*BitCount)>>3;
            Count = RankBytesCount*Height;
            Scan0 = Marshal.AllocHGlobal(Count);
            IntPtr rowHead = Scan0;
            int idx = HeadStructSize;
            for (var i = 0; i < Height; i++)
            {
                Marshal.Copy(data,idx,rowHead,RankBytesCount);
                rowHead = IntPtr.Add(rowHead,RankBytesCount);
                idx += Stride;
            }
        }
        
        public override unsafe void WriteImage(string filepath)
        {
            byte[] d = new byte[FileBytesSize]; 
            IntPtr p = Marshal.AllocHGlobal(HeadStructSize);
            ushort* ushorthead = (ushort*)p.ToPointer();
            *ushorthead++ = ImageTypeData.bmpFileHead;
            int* intHead = (int*)ushorthead;
            *intHead++ = FileBytesSize;
            *intHead++ = 0;
            *intHead++ = HeadStructSize;
           
            *intHead++ = 40;
            *intHead++ = Width;
            *intHead++ = Height;
            *intHead++ = (BitCount<<16) + 1;
            *intHead++ = Compression;
            *intHead++ = FileBytesSize - HeadStructSize;
            *intHead++ = XPelsPermeter;
            *intHead++ = YPelsPermeter;
            *intHead++ = RefrenceColorCount;
            *intHead++ = ImportantColorCount;

            byte* byteHead = (byte*)intHead;
            for(int i =0 ;i<Palette.Length;i++)
            {
                *byteHead++ = Palette[i];
            }

            Marshal.Copy(p,d,0,HeadStructSize);
            Marshal.FreeHGlobal(p);
            
            //bitmap
            IntPtr rowHead = Scan0;
            int idx = HeadStructSize;
            for (var i = 0; i < Height; i++)
            {
                Marshal.Copy(rowHead,d,idx,RankBytesCount);
                rowHead = IntPtr.Add(rowHead,RankBytesCount);
                idx += Stride;
            }
            
            System.IO.File.WriteAllBytes(filepath,d);
        }

        public override unsafe Mat TransBmpToMat()
        {
            Mat mat = new Mat();
            mat.InitMemory(Width,Height,1);
            IntPtr des = mat.Scan0;
            IntPtr src = Scan0;
            for(int i =0;i<Height;i++)
            {
                memcpy(des,src,new UIntPtr((uint)mat.RankSize));
                des = IntPtr.Add(des,mat.RankSize);
                src = IntPtr.Add(src,Stride);
            }
            return mat;
        }
        
        public override void TransMatToBmp(Mat mat)
        {
            if(mat.ElementSize != 1||mat.Width != Width || mat.Height != Height)
            {
                throw new Exception("Input Param NOT Fit Container");
            }
            IntPtr des = Scan0;
            IntPtr src = mat.Scan0;
            for(int i =0;i<Height;i++)
            {
                memcpy(des,src,new UIntPtr((uint)mat.RankSize));
                des = IntPtr.Add(des,Stride);
                src = IntPtr.Add(src,mat.RankSize);
            }
        }
    }
}