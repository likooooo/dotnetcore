using System;
using IniHelper;
using System.IO;

namespace Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Test.ini");
            if(!File.Exists(filePath))
            {
                File.Create(filePath);
            }
             //写入节点1
            IniHelper_Win32.Write("s1", "1", "a", filePath);
            IniHelper_Win32.Write("s1", "2", "b", filePath);
            IniHelper_Win32.Write("s1", "3", "c", filePath);
            //写入节点2
            IniHelper_Win32.Write("s2", "4", "d", filePath);
            IniHelper_Win32.Write("s2", "5", "e", filePath);
            //改节点值（就是重写一遍）
            IniHelper_Win32.Write("s1", "3", "c3", filePath);
            //读取节点1中的key为1的值
            string value = IniHelper_Win32.Read("s1", "1", "789", filePath);
            IniHelper_Win32.DeleteKey("s1", "2", filePath);//删除节点s1中key为2的值
            IniHelper_Win32.DeleteSection("s1", filePath);//删除节点s2
            Console.WriteLine(value);
        }
    }
}
