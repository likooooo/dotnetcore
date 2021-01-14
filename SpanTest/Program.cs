using System;
using System.Text;
using System.Linq;
using System.Runtime;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace SpanTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var p = Marshal.AllocHGlobal(10);
            Span<byte> span ;
            unsafe
            {
                span = new Span<byte>(p.ToPointer(),10);
            }
            span.Fill(10);
            byte[] copy = new byte[10];
            unsafe
            {       
                Marshal.Copy(new IntPtr((void*)(span[0])),copy,0,10);
            }
            foreach(var s in copy)
            {
                Console.Write(s);
            }
            Marshal.FreeHGlobal(p);
            Console.WriteLine("Hello World!");
        }
    }
}
