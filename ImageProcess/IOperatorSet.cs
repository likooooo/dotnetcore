#define GrayPrecision_16
using System;
using ImageProcess.ImageEntry.Bmp;
using ImageProcess.ImageEntry.RLC;
namespace ImageProcess
{
    public static unsafe class IOperatorSet<T> where T:unmanaged
    {
        //将非托管类型转为指定类型指针，*T+=idx
        public static ref T GetElement(ImageCore img,int idx)
        {
            T* p = (T*)img.Scan0.ToPointer();
            return ref *(p+=idx);
        }

        //对矩阵元素值的缩放，leftMove 代表原始值放大2^leftMove次方，rightMove代表值缩小2^righMove次方
        //cellVal = (cellVal * 2^leftMove) / 2^righMove + add;
        public static void ScalaMat(ImageCore img,int leftMove,int rightMove,int add)
        {
            int loopCount = img.Count;
            byte* p = (byte*)img.Scan0.ToPointer();
            Int64 tempVal = 0;
            while (--loopCount > -1)
            {
                tempVal = (*p<<leftMove)>>rightMove + add;
                *p = tempVal <256?(byte)tempVal:255;
                p += img.ElementSize;
            }
        }

        //填充内存
        public static void Fill(ImageCore img,T val)
        {
            int loopCount = img.Count;//元素个数
            T* p = (T*)img.Scan0.ToPointer();
            while (--loopCount > -1)
            {
                *p = val;
                Console.WriteLine($"{(int)p}-{*p}");
                p += img.ElementSize;
            }
        }

        //将原始矩阵元素拆分为3份，分别为m0,m1,m2
        public static void Decompose3(ImageCore img,out BmpGray8 r,out BmpGray8 g,out BmpGray8 b)
        {
            if(img.ElementSize%3 != 0)
            {
                throw new Exception("Source Mat NOT 3 Channel");
            }
            r = new BmpGray8(img.Width,img.Height);
            r.InitMemory();
            g = new BmpGray8(img.Width,img.Height);
            g.InitMemory();
            b = new BmpGray8(img.Width,img.Height);
            b.InitMemory();
            T* src = (T*)img.Scan0.ToPointer();
            T* p0 = (T*)r.Scan0.ToPointer();
            T* p1 = (T*)g.Scan0.ToPointer();
            T* p2 = (T*)b.Scan0.ToPointer();
            int loopCount = r.Count;
            T* p = (T*)img.Scan0.ToPointer();
            while (--loopCount > -1)
            {
                *p2++ = *p++;
                *p1++ = *p++;
                *p0++ = *p++;
            }
        }

        //
        public static void Rgb3ToGray(BmpGray8 r,BmpGray8 g,BmpGray8 b,out BmpGray8 gray)
        {
            gray = new BmpGray8(r.Width,r.Height);
            gray.InitMemory();
            byte* grayPtr = (byte*)gray.Scan0.ToPointer();
            byte* rptr = (byte*)r.Scan0.ToPointer();
            byte* gptr = (byte*)g.Scan0.ToPointer();
            byte* bptr = (byte*)b.Scan0.ToPointer();
            int loopCount = r.Count;
            while (--loopCount > -1)
            {
                #if GrayPrecision_16
                    *grayPtr++ = (byte)(((*rptr++)*19595 + (*gptr++)*38469 + (*bptr++)*7472)>>16);
                #elif GrayPrecision_8
                    *grayPtr++ = (byte)(((*rptr++)*76 + (*gptr++)*150 + (*bptr++)*30)>>8);
                #endif
            }
        }

        public static void Threshold(ImageCore grayImage,out BmpGray8 region,byte min,byte max)
        {
            region = new BmpGray8(grayImage.Width,grayImage.Height);
            region.InitMemory();
            int loopCount = grayImage.Count;
            byte* gp = (byte*)grayImage.Scan0.ToPointer();
            byte* rp = (byte*)region.Scan0.ToPointer();
            while (--loopCount > -1)
            {     
                *rp++ = *gp>=min&&*gp<=max?255:0;
                gp++;
            }
        }
        
        public static void Threshold(ImageCore grayImage,out RLC_NodeList<byte> list,byte min,byte max)
        {
            int loopCount = grayImage.Count;
            byte* gp = (byte*)grayImage.Scan0.ToPointer();
            list = new RLC_NodeList<byte>();
            int idx = -1;
            while (++idx<grayImage.Count)
            {
                if (*gp>=min&&*gp<=max)
                {
                    list.PushBack(0xff,idx);
                }
                gp++;
            }
        }
    }
}