using System.Runtime.InteropServices;

namespace ImageProcess.Core
{
    //bmp,jpg,tiff基类，默认类型为bmp
    public abstract class ImageFileCommon:UnmanagedMemory,IUnmanagedMemory
    { 
        public int FileBytesSize{get;protected set;}
        public int HeadStructSize{get;protected set;}
        //压缩方式/文件类型
        public int Compression{get;protected set;}
        public int Stride{get;protected set;}

        public ImageFileCommon(){}
        public ImageFileCommon(int widht,int height,ushort bitcount,int compression):base(widht,height,bitcount)
        {
            this.Compression = compression ;
            Stride = ((Width*BitCount + 31)>>5)<<2;
            HeadStructSize = 54 + (2<<bitcount)*4;
            
            FileBytesSize = HeadStructSize + Stride * Height;
        }

        public virtual void ReadImage(string filepath){ throw new System.Exception("No Realization");}
        public virtual void WriteImage(string filepath){ throw new System.Exception("No Realization");}
    }
}