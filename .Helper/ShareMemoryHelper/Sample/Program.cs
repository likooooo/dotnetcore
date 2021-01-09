using System;
using ShareMemoryHelper;
using System.Linq;

namespace ShareMemoryHelper_Client
{
    class Program
    {
        static void Main(string[] args)
        {
            ShareMemory_Win32 sw = new ShareMemory_Win32("test",10);
            sw.CreateShareMemoryMap("test",10);
            byte[] write =  System.Text.Encoding.UTF8.GetBytes("abcd");
            int res = sw.Write(write,0,4);
            System.Threading.Thread.Sleep(300);
            if(res == 0)
            {
                ShareMemory_Win32 sr = new ShareMemory_Win32("test",10);
                Console.WriteLine("write sucess");
                byte[] read = new byte[4];
                sr.Read(ref read,0,4);
                Console.WriteLine("write data: abcd\n"+"read data:"+System.Text.Encoding.UTF8.GetString(read));
            }
            else
            {
                Console.WriteLine("write fail");
            }
        }
    }
}
