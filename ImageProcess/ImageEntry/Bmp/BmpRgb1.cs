using System;
using System.Runtime.InteropServices;
namespace ImageProcess.ImageEntry.Bmp
{
    public class BmpRgb1:BmpImage
    {
        public BmpRgb1():base(){}
        public BmpRgb1(int width,int height):base(width,height,1,ImageTypeData.rgb)
        {
            Palette = ColorPalette.RgbPalette_1;
        }

        public unsafe BmpRgb1(int width,int height,IntPtr mat):this(width,height)
        {
            memcpy(this.Scan0,mat,new UIntPtr((uint)Count));
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
            if((BitCount != 1)||(Compression != ImageTypeData.rgb))
            {
                throw new Exception("Target file is NO Rgb1");
            }
            intHead++; //FileBytesSize - HeadStructSize
            XPelsPermeter = *intHead++;
            YPelsPermeter = *intHead++;
            RefrenceColorCount = *intHead++;
            ImportantColorCount = *intHead++;
            Palette = new byte[8];
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

        //10100101 =>0001 0000 0001 0000 0000 0001 0000 0001
        static Int64 ByteToInt64(byte val)
        {
            Int64 res;
            res =  val&1;
            res += (val&2)<<3;
            res += (val&4)<<6;
            res += (val&8)<<9;
            res += (val&16)<<12;
            res += (val&32)<<15;
            res += (val&64)<<18;
            res += (val&128)<<21;
            return res;
        }
        static byte Int64ToByte(Int64 val)
        {
            byte res;
            res =  (byte)( val&1             );
            res += (byte)((val&16)       >> 3);
            res += (byte)((val&256)      >> 6);
            res += (byte)((val&4096)     >> 9);
            res += (byte)((val&65536)    >>12);
            res += (byte)((val&1048576)  >>15);
            res += (byte)((val&16777216) >>18);
            res += (byte)((val&268435456)>>21);
            return res;
        }
        //https://www.docin.com/p-1331706333.html
        public override unsafe Mat TransBmpToMat()
        {
            Mat mat = new Mat();
            mat.InitMemory(Width,Height,1);
            byte* src =(byte*) Scan0.ToPointer();
            Int64* des = (Int64*)mat.Scan0.ToPointer();

            byte skip = (byte)(Width%8);
            int srcSkip = Stride - RankBytesCount; 
            //到达尾行触发
            int lineIdx = Width - 1;
            for(int i = 0;i<Count;i++)
            {
                if(i == lineIdx)//负责最后一个字节的二值数据玻璃，再换行
                {
                    byte* byteHead = (byte*)src;
                    byte rowEndByte = *src++;
                    for(byte j = 0;j<skip;j++)
                    {
                        *byteHead++ = (byte)Bit.GetBit(rowEndByte,j);
                    }

                    des = (Int64*)byteHead;
                    lineIdx += Width;
                    src += srcSkip;
                    continue;
                }
                //一次存8字节
                *des++ = ByteToInt64(*src++);
            }
            return mat;
        }
        
        public override unsafe void TransMatToBmp(Mat mat)
        {
            if(mat.ElementSize != 1||mat.Width != Width || mat.Height != Height)
            {
                Console.WriteLine("Input Param NOT Fit Container");
                throw new Exception("Input Param NOT Fit Container");
            }
            byte*  des = (byte*) Scan0.ToPointer();
            Int64* src = (Int64*)mat.Scan0.ToPointer();

            byte skip = (byte)(Width%8);
            int srcSkip = Stride - RankBytesCount; 
            //到达尾行触发
            int lineIdx = Width - 1;
            for(int i =0;i<mat.Count;i += 8)
            {
                if(i == lineIdx)//负责最后一个字节的二值数据玻璃，再换行
                {
                    byte* byteHead = (byte*)src;
                    byte rowEndByte = 0;
                    for(byte j = 0;j<skip;j++)
                    {
                        rowEndByte += (byte)(*byteHead<<j);
                        byteHead++;
                    }
                    *des ++ = rowEndByte;
                    src = (Int64*)byteHead;
                    lineIdx += Width;
                    src += srcSkip;
                    continue;
                }
                *des++ = Int64ToByte(*src++); 
            }
        }
    }
}