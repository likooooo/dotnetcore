#pragma warning disable 0649
using System;
using System.Text;
using System.Linq;
using System.Runtime;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.Json.Serialization;

using RuntimeDebug;

//https://blog.csdn.net/u013066730/article/details/82625158
//https://www.cnblogs.com/wanghao-boke/p/11635179.html
namespace ImageProcess
{
    public class ImageAttribute
    {
        public static bool IsContainColorPalette(IImageCore image)
            => image.BitCount < 9;

        public static bool IsBmp(IImageCore image)
            => image.FileType == 19778;

        public static int GetStride(IImageCore image) 
            =>(((int)(image.Width*image.BitCount + 31))>>5)<<2;
    }

    public static class ImageMemoryOpereSet
    {
        /// <summary>
        /// 结构体转byte[]
        /// </summary>
        public static byte[] StructToBytes<TStruct>(TStruct t) where TStruct : struct
        {
            int structSize = Marshal.SizeOf(typeof(TStruct));
            byte[] data = new byte[structSize];

            IntPtr p = Marshal.AllocHGlobal(structSize);
            Marshal.StructureToPtr<TStruct>(t,p ,false);
            Marshal.Copy(p,data,0,structSize);
            Marshal.FreeHGlobal(p);
            return data;
        }
        public static byte[] StructToBytes<TStruct>(TStruct t,int size) where TStruct : struct
        {
            byte[] data = new byte[size];

            IntPtr p = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr<TStruct>(t,p ,false);
            Marshal.Copy(p,data,0,size);
            Marshal.FreeHGlobal(p);
            return data;
        }

        /// <summary>
        /// byte[]转结构体
        /// </summary>
        public static TStruct BytesToStruct<TStruct>(byte[] data) where TStruct : struct
        {
            int structSize = Marshal.SizeOf(typeof(TStruct));
            if(structSize>data.Length)
            {
                throw new Exception($"结构体尺寸超出源数据{structSize} > {data.Length}");
            }
            IntPtr p = Marshal.AllocHGlobal(structSize);
            TStruct t = default(TStruct);

            Marshal.Copy(data,0,p,data.Length);
            t = Marshal.PtrToStructure<TStruct>(p);
            Marshal.FreeHGlobal(p);
            return t;
        }

        /// <summary>
        /// memory->Disk
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="{System.IO.File.WriteAllBytes(filepath"></param>
        public static void BytesToFile(string filepath,byte[] data) {System.IO.File.WriteAllBytes(filepath,data);}
        
        /// <summary>
        /// disk->memory
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public static byte[] FileToBytes(string filepath)  =>System.IO.File.ReadAllBytes(filepath);
        
        //p为非托管内存
        public static TStruct PtrToStruct<TStruct>(IntPtr p) where TStruct : struct
        {
            int structSize = Marshal.SizeOf(typeof(TStruct));
            TStruct t = default(TStruct);
            t = Marshal.PtrToStructure<TStruct>(p);
            return t;
        }

        public static IntPtr StructToPtr<TStruct>(TStruct t,int structSize)where TStruct:struct
        {
            IntPtr p = Marshal.AllocHGlobal(structSize);
            Marshal.StructureToPtr<TStruct>(t,p ,false);
            return p;
        }
        public static int[] BytesToInt32Arry(byte[] bytes)
        {
            int resCount = bytes.Length/4;
            int[] res = new int[resCount];
            IntPtr p = Marshal.AllocHGlobal(bytes.Length);
            Marshal.Copy(bytes,0,p,bytes.Length);
            Marshal.Copy(p,res,0,bytes.Length);
            Marshal.FreeHGlobal(p);
            return res;
        }

        #region bytes ->struct    
        public static readonly List<ushort> FileFilter = new List<ushort>
        {
            {19778},{0},{1}
        };

        public static int ImageToStruct(Span<byte> head,out ImageType type,out ImageFile bf,out ImageFileInfo bfi,out ColorPalette cp,out ImageData data)
        {
            int res = 0;
            bf = default(ImageFile);
            bfi = default(ImageFileInfo);
            cp = default(ColorPalette);
            data = default(ImageData);
            type = ImageMemoryOpereSet.BytesToStruct<ImageType>(head.Slice(0,2).ToArray());
            if(!FileFilter.Contains(type.bfType)) return 1;
            bf = ImageMemoryOpereSet.BytesToStruct<ImageFile>(head.Slice(2,12).ToArray());
            bfi = ImageMemoryOpereSet.BytesToStruct<ImageFileInfo>(head.Slice(14,40).ToArray());
            if(bfi.biBitCount<9)
            {
                cp = ImageMemoryOpereSet.BytesToStruct<ColorPalette>(head.Slice(54,(int)bf.bfOffBits - 54).ToArray());
                
            }
            else
            {
                res = 2;
            }
            if(head.Length == bf.bfOffBits)
            {
                
                data.D = new byte[bf.bfSize - bf.bfOffBits];
                data.D = head.Slice((int)bf.bfOffBits,data.D.Length).ToArray();
            }
            else
            {
                res = 3;
            }
            return res;
        }
        public static bool StructToImage(ImageType type,ImageFile bf,ImageFileInfo bfi,ColorPalette cp,ImageData data,out Span<byte> fileSpan)
        {  
            fileSpan = new Span<byte>(new byte[(int)bf.bfSize]);
            byte[] tempBytes = ImageMemoryOpereSet.StructToBytes<ImageType>(type,2);
            int idx = 0;
            foreach(var b in tempBytes)
            {
                fileSpan[idx++] = b;
            }
            tempBytes = ImageMemoryOpereSet.StructToBytes<ImageFile>(bf,12);
            foreach(var b in tempBytes)
            {
                fileSpan[idx++] = b;
            }
            tempBytes = ImageMemoryOpereSet.StructToBytes<ImageFileInfo>(bfi,40);
            foreach(var b in tempBytes)
            {
                fileSpan[idx++] = b;
            }
            if(cp.palette.Length>0)
            {
                 foreach(var b in cp.palette)
                {
                    fileSpan[idx++] = b;
                }
            }
            foreach(var b in data.D)
            {
                fileSpan[idx++] = b;
            }
            return idx == bf.bfSize;
        }
        #endregion

    }

    //通过继承重写
    public interface IImageCore:IDisposable
    {
        ushort FileType{get;}

        uint FileBytesSize{get;}
        uint GetFileBytesSize();

        uint HeadStructSize{get;}
        uint GetHeadStructSize();
        
        int PaletteSize{get;}
        int BitmapSize{get;}

        uint Width{get;}
        uint GetWidth();

        uint Height{get;}
        uint GetHeight();

        ushort BitCount{get;}
        ushort GetBitCount();

        int Stride{get;}

        IntPtr FileHead{get;}

        IntPtr Palette{get;}
        IntPtr GetPalette();

        IntPtr Scan0{get;}
        IntPtr GetScan0();

        void ReadImage(string filepath,out Span<byte> span);
        void WriteImage(string filePath);
        void GenEmptyImage(uint width,uint height,ushort bitcount,out IImageCore img);
        //void Decompose3(out IImageCore r,out IImageCore g,out IImageCore b);
    }

    //bmp文件的IImageCore的实现
    public unsafe class ImageCore:IImageCore
    {
        //文件内存的非托管指针
        protected byte* _head;
        #region IImageCore属性接口的实现
        /// <summary>
        /// 文件类型
        /// </summary>
        /// <value></value>
        public ushort FileType{get;protected set;}

        /// <summary>
        /// 整个文件大小
        /// </summary>
        /// <value></value>
        public uint FileBytesSize{get;protected set;}
        public uint GetFileBytesSize()
        {
            FileBytesSize = *((uint*)(_head+2));
            return FileBytesSize;
        }

        /// <summary>
        /// 图像配置文件大小
        /// </summary>
        /// <value></value>
        public uint HeadStructSize{get;protected set;}
        public uint GetHeadStructSize()
        {
            HeadStructSize = *((uint*)(_head+10));
            return HeadStructSize;
        }

        /// <summary>
        /// 调色盘长度，没有调色盘 则为0
        /// </summary>
        /// <value></value>
        public int PaletteSize{get;protected set;}

        /// <summary>
        /// 图像区域的大小
        /// </summary>
        /// <value></value>
        public int BitmapSize{get;protected set;}

        public uint Width{get;protected set;}
        public uint GetWidth()=>(Width =  *((uint*)(_head+18)));

        public uint Height{get;protected set;}
        public uint GetHeight()
        {
            Height =  *((uint*)(_head+22));
            return Height;
        }

        /// <summary>
        /// 一个像素的位深度
        /// </summary>
        /// <value></value>
        public ushort BitCount{get;protected set;}
        public ushort GetBitCount()
        {
            BitCount =  *((ushort*)(_head+28));
            return BitCount;
        }

        /// <summary>
        /// 一整行的位深度
        /// </summary>
        /// <value></value>
        public int Stride{get;protected set;}

        public IntPtr FileHead{get => new IntPtr((void*)_head);}

        /// <summary>
        /// 调色盘指针
        /// </summary>
        /// <value></value>
        public IntPtr Palette{get;protected set;}
        public IntPtr GetPalette()
        {
            Palette = PaletteSize>0? new IntPtr(_head + 54): default(IntPtr);
            return Palette;
        }

        /// <summary>
        /// 位图指针
        /// </summary>
        /// <value></value>
        public IntPtr Scan0{get;protected set;}
        public IntPtr GetScan0()
        {  
            Scan0 = new IntPtr(_head + HeadStructSize);
            return Scan0;
        }

        #endregion

        #region 构造函数
        public ImageCore(){}

        /// <summary>
        /// 初始化一张空图片，并设定初始像素
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="bitcount"></param>
        /// <param name="initColor"></param>
        public ImageCore(uint width,uint height,ushort bitcount,byte initColor = 0)
        {
            FileType = 19778;
            this.Width = width;
            this.Height = height;
            this.BitCount = bitcount;
            Stride = ImageAttribute.GetStride(this);
            BitmapSize = (int)(Stride*height);  
            PaletteSize = (1<<bitcount)*4;
            HeadStructSize = 54 + (uint)PaletteSize;
            FileBytesSize = (uint)BitmapSize + HeadStructSize;
            Console.WriteLine($"{Stride}-{BitmapSize} - {HeadStructSize}");
    
            //1
            ImageType type = new ImageType
            {
                bfType = FileType
            };
            //2
            ImageFile bf = new ImageFile
            {
                bfSize = FileBytesSize,
                bfReserved = 0,
                bfOffBits = HeadStructSize
            };
            //3
            ImageFileInfo bfi = new ImageFileInfo
            {
                biSize = 40,
                biWidth = width,
                biHeight = height,
                biPlanes = 1,
                biBitCount = BitCount,
                biCompression = 0,
                biSizeImage = 0,
                biXPelsPerMeter = 0,
                biYPelsPerMeter = 0,
                biClrUsed = 0,
                biClrImportant = 0
            };
            //4 调色盘实现
            ColorPalette cp = new ColorPalette
            {
                palette = new byte[PaletteSize]
            };
            byte step = (byte)(255*1/(1<<bitcount-1));   
            int idx = 0; 
            byte vale = 0;
            while(idx < PaletteSize)
            {
                cp.palette[idx++] = vale; 
                cp.palette[idx++] = vale; 
                cp.palette[idx++] = vale; 
                cp.palette[idx++] = 0; 
                vale += step;
            }
            //5 Data
            ImageData data = new ImageData
            {
                D = new byte[BitmapSize]
            };
            //Log
            DebugMsg.DebugConsoleOut(type.ToString());
            DebugMsg.DebugConsoleOut(bf.ToString());
            DebugMsg.DebugConsoleOut(bfi.ToString());
            DebugMsg.DebugConsoleOut(cp.ToString());

            Span<byte> fileData;
            ImageMemoryOpereSet.StructToImage(type,bf,bfi,cp,data,out fileData);        
            if(initColor>0)
            {
                (fileData.Slice((int)HeadStructSize,BitmapSize)).Fill(initColor);//1111 1111
            }

            IntPtr p = Marshal.AllocHGlobal(fileData.Length);
            Marshal.Copy(fileData.ToArray(),0,p,fileData.Length);   
            unsafe
            {
                _head = (byte*)p.ToPointer();
                Palette = PaletteSize>0? new IntPtr(_head + 54): default(IntPtr);
            } 
            GetPalette();
            GetScan0();
        }
        #endregion

        #region IDisposable接口实现
        
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        
        protected virtual void Dispose(bool disposeing)
        {
            Scan0 = IntPtr.Zero;
            Stride = 0;
            BitCount = 0;
            Height = 0;
            HeadStructSize = 0;
            FileBytesSize = 0;
            Marshal.FreeHGlobal(new IntPtr(_head));
        }
        #endregion

        public void GenEmptyImage(uint width,uint height,ushort bitcount,out IImageCore img)
        {
            img = null;
        }

        public void ReadImage(string filePath,out Span<byte> span)
        {
            byte[] data = ImageMemoryOpereSet.FileToBytes(filePath);
            IntPtr p = Marshal.AllocHGlobal(data.Length);
            Marshal.Copy(data,0,p,data.Length);     
            span = new Span<byte>(p.ToPointer(),data.Length);
            _head = (byte*)p.ToPointer();
            DebugMsg.DebugConsoleOut($"ReadImage=>readed data length :{span.Length}");

            GetFileBytesSize();
            GetHeadStructSize();
            GetWidth();
            GetHeight();
            GetBitCount();
            GetPalette();
            GetScan0();
        }

        public void WriteImage(string filePath)
        {
            Span<byte> span = new Span<byte>((void*)_head,(int)FileBytesSize);
            ImageMemoryOpereSet.BytesToFile(filePath,span.ToArray());
            DebugMsg.DebugConsoleOut($"WriteImage=>Writed data length :{FileBytesSize}");
        }
    }
}