using System;
using System.Linq;
using System.Runtime;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections.Generic;
using System.Runtime.InteropServices;
namespace ImageProcess
{
    public class StreamHelper
    {
        /// <summary>
        /// 结构体转byte[]
        /// </summary>
        public static byte[] StructToBytes<TStruct>(TStruct data) where TStruct : struct
        {
            int structSize = Marshal.SizeOf(typeof(TStruct));
            byte[] buffer = new byte[structSize];
            GCHandle handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
            Marshal.StructureToPtr(data, handle.AddrOfPinnedObject(), false);
            handle.Free();
            return buffer;
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

        public static TStruct PtrToStruct<TStruct>(IntPtr p) where TStruct : struct
        {
            int structSize = Marshal.SizeOf(typeof(TStruct));
            TStruct t = default(TStruct);
            Marshal.PtrToStructure<TStruct>(p, t);
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
    }
}