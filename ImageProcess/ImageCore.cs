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
    public class ImageAttributeJudge
    {
        public static bool IsContainColorPalette(IImageCore image)
            => image.BitCount<9;

        public static bool IsBmp(IImageCore image)
            => image.FileType == 19778;

        public static int GetStride(IImageCore image) 
            =>(((int)(image.Width*image.BitCount + 31))>>5)<<2;
        
        #region bytes ->struct    
        public static readonly List<ushort> FileFilter = new List<ushort>
        {
            {19778},{0},{1}
        };

        public static int GetImageStruct(Span<byte> head,out ImageType type,out ImageFile bf,out ImageFileInfo bfi,out ColorPalette cp,out ImageData data)
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
                cp = ImageMemoryOpereSet.BytesToStruct<ColorPalette>(head.Slice(54,bf.bfOffBits - 54).ToArray());
                
            }
            else
            {
                res = 2;
            }
            if(head.Length == bf.bfOffBits)
            {
                
                data.D = new byte[bf.bfSize - bf.bfOffBits];
                data.D = head.Slice(bf.bfOffBits,data.D.Length).ToArray();
            }
            else
            {
                res = 3;
            }
            return res;
        }
        #endregion
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
        public static byte[] StructToBytes<TStruct>(TStruct t,int structSize) where TStruct : struct
        {
            byte[] data = new byte[structSize];

            IntPtr p = Marshal.AllocHGlobal(structSize);
            Marshal.StructureToPtr<TStruct>(t,p ,false);
            Marshal.Copy(p,data,0,structSize);
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
    }

    //通过继承重写
    public interface IImageCore:IDisposable
    {
        ushort FileType{get;}

        uint FileBytesSize{get;}
        uint GetFileBytesSize();

        uint HeadStructSize{get;}
        uint GetHeadStructSize();

        uint Width{get;}
        uint GetWidth();

        uint Height{get;}
        uint GetHeight();

        ushort BitCount{get;}
        ushort GetBitCount();

        int Stride{get;}

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
        protected byte* _head;

        public ushort FileType{get;protected set;}

        public uint FileBytesSize{get;protected set;}
        public uint GetFileBytesSize()
        {
            FileBytesSize = *((uint*)(_head+2));
            return FileBytesSize;
        }

        public uint HeadStructSize{get;protected set;}
        public uint GetHeadStructSize()
        {
            HeadStructSize = *((uint*)(_head+10));
            return HeadStructSize;
        }

        public uint Width{get;protected set;}
        public uint GetWidth()=>(Width =  *((uint*)(_head+18)));

        public uint Height{get;protected set;}
        public uint GetHeight()
        {
            Height =  *((uint*)(_head+22));
            return Height;
        }

        public ushort BitCount{get;protected set;}
        public ushort GetBitCount()
        {
            BitCount =  *((ushort*)(_head+28));
            return BitCount;
        }

        public int Stride{get;protected set;}

        public IntPtr Scan0{get;protected set;}
        public IntPtr GetScan0()
        {
            Scan0 = new IntPtr((void*)(*(_head + HeadStructSize)));
            return Scan0;
        }


        public ImageCore(){}
        public ImageCore(uint width,uint height,ushort bitcount)
        {
            
            this.Width = width;
            this.Height = height;
            this.BitCount = bitcount;
            Stride = ImageAttributeJudge.GetStride(this);

            
            FileType = 19778;
            HeadStructSize = 54 + bitcount<9?(bitcount<<2)*4:0;
            FileBytesSize = Stride*height + HeadStructSize;
            
            ImageType type = new ImageType
            {
                bfType = FileType
            };
            ImageFile bf = new ImageFile
            {
                bfSize = FileBytesSize,
                bfReserved = 0,
                bfOffBits = HeadStructSize
            };
            ImageFileInfo bfi = new ImageFileInfo
            {
                bfSize = FileBytesSize,
                bfReserved = 0,
                bfOffBits = HeadStructSize
            };
            //out ImageFile bf,out ImageFileInfo bfi,out ColorPalette cp,out ImageData data
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

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
            GetScan0();
        }

        public void WriteImage(string filePath)
        {
            Span<byte> span = new Span<byte>((void*)_head,(int)FileBytesSize);
            ImageMemoryOpereSet.BytesToFile(filePath,span.ToArray());
            DebugMsg.DebugConsoleOut($"WriteImage=>Writed data length :{FileBytesSize}");
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
    }
}