using System;
using System.Runtime.InteropServices;

namespace ImageProcess.Core.Bmp
{
    ////https://blog.csdn.net/u013066730/article/details/82625158
    public unsafe abstract class BmpImage:ImageFileCommon
    {
        byte[] palette;
        public int XPelsPermeter{get;protected set;}
        public int YPelsPermeter{get;protected set;}
        public int RefrenceColorCount{get;protected set;}
        public int ImportantColorCount{get;protected set;}

        public BmpImage(){}

        public  BmpImage(int widht,int height,ushort bitcount,int compression = ImageTypeData.rgb):base(widht,height,bitcount,compression)
        {
            XPelsPermeter = 11911;
            YPelsPermeter = 11911;
            RefrenceColorCount = 2<<bitcount;
            ImportantColorCount = 0;
        }

        public override void ReadImage(string filepath)
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
            intHead++; //FileBytesSize - HeadStructSize
            XPelsPermeter = *intHead++;
            YPelsPermeter = *intHead++;
            RefrenceColorCount = *intHead++;
            ImportantColorCount = *intHead++;
            byte* byteHead = (byte*)intHead;
            //调色盘
            if(BitCount < 9)
            {
                palette = new byte[HeadStructSize - 54];
                for(int i =0;i<palette.Length;i++)
                {
                    palette[i] = *byteHead++;
                }
            }
            else
            {
                palette = new byte[0];
            }
            Marshal.FreeHGlobal(p);

            Stride = ((Width*BitCount + 31)>>5)<<2;
            RankBytesCount = Width*(BitCount>>3);
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

        public override void WriteImage(string filepath)
        {           
            byte[] d = new byte[FileBytesSize]; 
            IntPtr p = Marshal.AllocHGlobal(HeadStructSize);
            ushort* ushorthead = (ushort*)p.ToPointer();
            *ushorthead++ = 19778;
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
            //调色盘，默认是灰色或者没有调色盘，需要重载
            if(palette.Length > 0&&BitCount <9)
            {
                for(int i =0 ;i<palette.Length;i++)
                {
                    *byteHead++ = palette[i];
                }
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

    }
}