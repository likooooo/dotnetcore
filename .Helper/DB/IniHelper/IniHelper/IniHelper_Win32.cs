using System;
using System.Text;
using System.Runtime.InteropServices;

namespace IniHelper
{
    //reference https://www.cnblogs.com/xiesong/p/10320893.html
    public class IniHelper_Win32
    {
        //read
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, int nSize, string lpFileName);

        //write
        [DllImport("kernel32")]
        private static extern int WritePrivateProfileString(string lpApplicationName, string lpKeyName, string lpString, string lpFileName);
  
        const int MaxStrLength = 1024; 
        //读取指定节点指定key对应的value
        public static string Read(string section, string key, string def, string filePath)
        {
            StringBuilder sb = new StringBuilder(MaxStrLength);
            GetPrivateProfileString(section, key, def, sb, MaxStrLength, filePath);
            return sb.ToString();
        }

        //写入指定节点指定key对应的value
        public static int Write(string section, string key, string value, string filePath)
        {
            return WritePrivateProfileString(section, key, value, filePath);
        }
        
        //删除指定ini文件的节点
        public static int DeleteSection(string section, string filePath)
        {
            return Write(section, null, null, filePath);
        }

        //删除指定ini文件节点内的key对应的value
        public static int DeleteKey(string section, string key, string filePath)
        {
            return Write(section, key, null, filePath);
        }
    }
}
