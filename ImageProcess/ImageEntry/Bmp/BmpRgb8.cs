using System;
using System.Runtime.InteropServices;

namespace ImageProcess.ImageEntry.Bmp
{
    public class BmpRgb8:BmpImage
    {
        public BmpRgb8():base(){}
        public BmpRgb8(int width,int height):base(width,height,8,ImageTypeData.rgb)
        {
            Palette = ColorPalette.RgbPalette_256;
        }

        public unsafe BmpRgb8(int width,int height,IntPtr mat):this(width,height)
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
            mat.InitMemory(Width,Height,3);
            byte* src = (byte*)Scan0.ToPointer();
            byte* des = (byte*)mat.Scan0.ToPointer();
            int skip = Stride - RankBytesCount;
            int lineIdx = Width;
            //i = 0
            int cpIdx = (*src++) << 2;//4 *(*src++)        
            *des++ = Palette[cpIdx  ];
            *des++ = Palette[cpIdx+1];
            *des++ = Palette[cpIdx+2];
            // i = [1 ~ Count)
            for(int i = 1;i<Count;i++)
            {
                if(i == lineIdx)
                {
                    lineIdx += Width;
                    src += skip;
                }
                cpIdx = (*src++) << 2;
                *des++ = Palette[cpIdx  ];
                *des++ = Palette[cpIdx+1];
                *des++ = Palette[cpIdx+2];
            }
            return mat;
        }
        
        public override void TransMatToBmp(Mat mat)
        {
            if(mat.ElementSize != 3||mat.Width != Width || mat.Height != Height)
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