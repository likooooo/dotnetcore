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
    using ImageProcess.ImageExtention;

    public static class ImageMemoryOpereSet
    {
        #region struct <-> byte[]
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
            
        public static readonly List<ushort> FileFilter = new List<ushort>
        {
            {19778}
        };
        public static int SpanToStruct(Span<byte> head,out ImageType type,out ImageFile bf,out ImageFileInfo bfi,out ColorPalette cp,out ImageData data)
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
            data.D = new byte[bf.bfSize - bf.bfOffBits];
            data.D = head.Slice((int)bf.bfOffBits,data.D.Length).ToArray(); 
            return res;
        }
        
        public static bool StructToSpan(ImageType type,ImageFile bf,ImageFileInfo bfi,ColorPalette cp,ImageData data,out Span<byte> fileSpan)
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
        
        #region hardDisk <-> memory
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
      
        #endregion  
        
        #region unmanagedMemory <->struct
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
        #endregion
       
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

        #region Image ->struct    
        public static void ImageToStruct(IImageCore img,out ImageType type,out ImageFile bf,out ImageFileInfo bfi,out ColorPalette cp,out ImageData data)
        {
            //1
            type = new ImageType
            {
                bfType = img.FileType
            };
            //2
            bf = new ImageFile
            {
                bfSize = img.FileBytesSize,
                bfReserved = 0,
                bfOffBits = img.HeadStructSize
            };
            //3
            bfi = new ImageFileInfo
            {
                biSize = 40,
                biWidth = img.Width,
                biHeight = img.Height,
                biPlanes = 1,
                biBitCount = img.BitCount,
                biCompression = 0,
                biSizeImage = 0,
                biXPelsPerMeter = 0,
                biYPelsPerMeter = 0,
                biClrUsed = 0,
                biClrImportant = 0
            };
            //4 调色盘实现
            cp = new ColorPalette
            {
                palette = new byte[img.PaletteSize]
            };
            byte step = (byte)(255*1/(1<<img.BitCount-1));   
            int idx = 0; 
            byte vale = 0;
            while(idx < img.PaletteSize)
            {
                cp.palette[idx++] = vale; 
                cp.palette[idx++] = vale; 
                cp.palette[idx++] = vale; 
                cp.palette[idx++] = 0; 
                vale += step;
            }
            //5 Data
            data = new ImageData
            {
                D = new byte[img.BitmapSize]
            };
        }
        #endregion

    }

    //通过继承重写
    public interface IImageCore:IDisposable
    {
        ushort FileType{get;}
        uint FileBytesSize{get;}
        uint HeadStructSize{get;}
        ImageCompressionType Compression{get;}
        int PaletteSize{get;}
        int BitmapSize{get;}
        int Width{get;}
        int Height{get;}
        ushort BitCount{get;}
        int ColumnByteSize{get;}
        int Stride{get;}
        int SkipByteCount{get;}

        IntPtr Palette{get;}
        IntPtr Scan0{get;}

        int GetPixelOffset(int x,int y);

        void ReadImage(string filePath);
        void WriteImage(string filePath);
    }

    //bmp文件的IImageCore的实现
    public unsafe class ImageCore:IImageCore,IOperateSet
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

        /// <summary>
        /// 图像配置文件大小
        /// </summary>
        /// <value></value>
        public uint HeadStructSize{get;protected set;}

        //压缩方式
        public ImageCompressionType Compression{get;protected set;}

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

        public int Width{get;protected set;}

        public int Height{get;protected set;}

        /// <summary>
        /// 一个像素的位深度
        /// </summary>
        /// <value></value>
        public ushort BitCount{get;protected set;}

        /// <summary>
        /// 一行像素的字节数
        /// </summary>
        /// <value></value>
        public int ColumnByteSize{get;protected set;}

        /// <summary>
        /// 一行像素的字节数 + 4字节对齐额外补充的1~3个字节
        /// </summary>
        /// <value></value>
        public int Stride{get;protected set;}

        public int SkipByteCount{get;protected set;}

        /// <summary>
        /// 调色盘指针
        /// </summary>
        /// <value></value>
        public IntPtr Palette{get;protected set;}

        /// <summary>
        /// 位图指针
        /// </summary>
        /// <value></value>
        public IntPtr Scan0{get;protected set;}
       
        public int GetPixelOffset(int x,int y) => y*Stride + x*BitCount;

        public void ReadImage(string filePath)
        {
            byte[] data = ImageMemoryOpereSet.FileToBytes(filePath);   
            //创建非托管内存
            IntPtr p = Marshal.AllocHGlobal(data.Length);
            Marshal.Copy(data,0,p,data.Length);
            //初始化指针
            _head = (byte*)p.ToPointer();     
            InitImageData(_head);
            Palette = PaletteSize > 0? new IntPtr(_head + 54): default(IntPtr);
            Scan0 = new IntPtr(_head + HeadStructSize);
        }

        public void WriteImage(string filePath)
        {
            Span<byte> span = new Span<byte>((void*)_head,(int)FileBytesSize);
            ImageMemoryOpereSet.BytesToFile(filePath,span.ToArray());
            DebugMsg.DebugConsoleOut($"WriteImage=>Writed data length :{FileBytesSize}");
        }

        #endregion

        #region IOperateSet
        //需要重载以提高效率
        public virtual void Decompose3(out IImageCore r,out IImageCore g,out IImageCore b)
        {    
            //数据合法判断
            if(BitCount == 1||Compression != ImageCompressionType.rgb/*只实现了RGB的Decompose3*/)
            {
                r = null;
                g = null;
                b = null; 
                return;
            }
            
            if(this.IsContainColorPalette())
            {   
                this.Decompose3_RGB_1_4_8(out r,out g,out b);
            }           
            else
            {
                this.Decompose3_RGB_16_24_32(out r,out g,out b);
            }
        }
        
        //24位转8位灰度图
        public virtual void Rgb1ToGray(out IImageCore grayImage)
        {
            this.Rgb1ToGray_24(out grayImage);
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
            Marshal.FreeHGlobal(new IntPtr(_head));
        }
        #endregion

        #region 初始化
        public ImageCore(){}

        // 初始化一张空图片，并设定初始像素,创建指定深度的图像建议调用ImageType/Image_*
        public ImageCore(int width,int height,ushort bitcount,byte initColor = 0)
        {
            InitImageData(width,height,bitcount);
            ImageType type;
            ImageFile bf; 
            ImageFileInfo bfi;
            ColorPalette cp;
            ImageData data;
            ImageMemoryOpereSet.ImageToStruct(this,out type,out bf,out bfi,out cp,out data);
            Span<byte> fileData;
            #region heap->stack
            ImageMemoryOpereSet.StructToSpan(type,bf,bfi,cp,data,out fileData);        
            if(initColor>0)
            {
                (fileData.Slice((int)HeadStructSize,BitmapSize)).Fill(initColor);
            }
            #endregion
            InitUnmanagedMen(fileData.ToArray());
        }
        
        //按照指定位图信息初始化图片对象
        public ImageCore(int width,int height,ushort bitcount,byte[] rgbData)
        {
            InitImageData(width,height,bitcount);
            IntPtr p = Marshal.AllocHGlobal((int)FileBytesSize);
            _head = (byte*)p.ToPointer();

            *((ushort*)_head)    = FileType;
            *((uint*)(_head+2))  = FileBytesSize;          
            *((uint*)(_head+10)) = FileBytesSize;
            *((int*)(_head+18)) = Width;
            *((int*)(_head+22)) = Height;
            *((ushort*)(_head+28))=BitCount;
            Marshal.Copy(rgbData.ToArray(),(int)HeadStructSize,p,BitmapSize);
        }

        //通过位图参数计算系统其他参数
        protected virtual void InitImageData(int width,int height,ushort bitcount)
        {                  
            FileType = 19778;
            this.Width = width;
            this.Height = height;
            this.BitCount = bitcount;
            Compression = ImageCompressionType.rgb;

            ColumnByteSize = Width*BitCount>>8;  
            Stride = ((Width*BitCount + 31)>>5)<<2;
            SkipByteCount = Stride - ColumnByteSize;
            BitmapSize = (int)(Stride*height);  
            PaletteSize = (1<<bitcount)*4;
            HeadStructSize = 54 + (uint)PaletteSize;
            FileBytesSize = (uint)BitmapSize + HeadStructSize;
        }
        
        //通过头指针读取系统其他参数
        protected virtual void InitImageData(byte* _head)
        {           
            FileType = *((ushort*)_head); 
            FileBytesSize = *((uint*)(_head+2));          
            HeadStructSize = *((uint*)(_head+10));
            Width =  *((int*)(_head+18));
            Height =  *((int*)(_head+22));
            BitCount =  *((ushort*)(_head+28));
            Compression = *(ImageCompressionType*)(_head+30);

            ColumnByteSize = Width*BitCount>>8;  
            Stride = ((Width*BitCount + 31)>>5)<<2;
            SkipByteCount = Stride - ColumnByteSize;
            PaletteSize = (int)HeadStructSize - 54;
            BitmapSize = (int)(FileBytesSize - HeadStructSize);  
        }

        //statck memory -> unmanaged memory
        protected virtual void InitUnmanagedMen(byte[] fileData)
        {
            //managed memory to unmanaged memory
            IntPtr p = Marshal.AllocHGlobal(fileData.Length);
            Marshal.Copy(fileData,0,p,fileData.Length);   
            
            _head = (byte*)p.ToPointer();
            Palette = PaletteSize > 0? new IntPtr(_head + 54): default(IntPtr);
            Scan0 = new IntPtr(_head + HeadStructSize);
        }
        #endregion

        #region data converter
        //y(row)
        //^
        //|
        //|----->x(col)

        // public virtual IntPtr GetPixel(int x,int y) => IntPtr.Add(Scan0,y*Stride + x*BitCount);
  
        // public virtual IntPtr this[int rowIdx] => IntPtr.Add(Scan0,rowIdx*Stride); 

        // public virtual Span<byte> GetBmpSpan() => new Span<byte>(Scan0.ToPointer(),BitmapSize);
        #endregion         
    }

}