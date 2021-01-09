using System;
using System.Runtime.InteropServices;

namespace ShareMemoryHelper
{
    public class ShareMemory_Win32
    {
        #region Win32 api
        [DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr OpenFileMapping(int dwDesiredAccess, [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle, string lpName);
        [DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr MapViewOfFile(IntPtr hFileMapping, uint dwDesiredAccess, uint dwFileOffsetHigh, uint dwFileOffsetLow, uint dwNumberOfBytesToMap);
        [DllImport("kernel32", EntryPoint = "GetLastError")]
        public static extern int GetLastError();
        [DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr CreateFileMapping(int hFile, IntPtr lpAttributes, uint flProtect, uint dwMaxSizeHi, uint dwMaxSizeLow, string lpName);
        [DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
        public static extern bool UnmapViewOfFile(IntPtr pvBaseAddress);
        [DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
        public static extern bool CloseHandle(IntPtr handle);
        #endregion

        //opertor handle
        public IntPtr mapView = IntPtr.Zero;
        #region params
        protected IntPtr fileMapping = IntPtr.Zero;
        protected bool bValid;
        protected bool m_bAlreadyExist = false;
        protected bool m_bInit = false;
        protected long m_MemSize = 0;

        #endregion
        #region const status
        const int FILE_MAP_COPY = 0x0001;
        const int FILE_MAP_WRITE = 0x0002;
        const int FILE_MAP_READ = 0x0004;
        const int FILE_MAP_ALL_ACCESS = 0x0002 | 0x0004;
        const int PAGE_READWRITE = 0x04;
        const int INVALID_HANDLE_VALUE = -1;
        const int ERROR_ALREADY_EXISTS = 183;
        #endregion

        public ShareMemory_Win32(string name, uint length)
        {
            bValid = GetShareMemoryMap(name, length);
        }
        
        ~ShareMemory_Win32()
        {
            Close();
        }
        public bool GetShareMemoryMap(string name, uint length)
        {
            m_MemSize = length;
            fileMapping = OpenFileMapping(PAGE_READWRITE, false, name);
            if (fileMapping == IntPtr.Zero)
            {
                return false;
            }
            mapView = MapViewOfFile(fileMapping, (uint)FILE_MAP_READ, 0, 0, length);
            if (mapView == IntPtr.Zero)
            {
                int a = GetLastError();
                return false;
            }
            m_bInit = true;
            return true;
        }
 
        public int CreateShareMemoryMap(string strName, long lngSize)
        {
            if (lngSize <= 0 || lngSize > 0x00800000) lngSize = 0x00800000;
            m_MemSize = lngSize;
            if (string.IsNullOrEmpty(strName))
            {
               return 1;
            }
            fileMapping = CreateFileMapping(INVALID_HANDLE_VALUE, IntPtr.Zero, (uint)PAGE_READWRITE, 0, (uint)lngSize, strName);
            if (fileMapping == IntPtr.Zero)
            {
                return 2; 
            }
            m_bAlreadyExist = GetLastError() == ERROR_ALREADY_EXISTS;
            mapView = MapViewOfFile(fileMapping, FILE_MAP_WRITE, 0, 0, (uint)lngSize);
            if (mapView == IntPtr.Zero)
            {
                m_bInit = false;
                CloseHandle(fileMapping);
                return 3; 
            }
            else
            {
                m_bInit = true;
                if (m_bAlreadyExist == false)
                {
                }
            }
        return 0;
    }

        public void Close()
        {
            if (m_bInit)
            {
                UnmapViewOfFile(mapView);
                CloseHandle(fileMapping);
            }
        }

        public int Read(ref byte[] bytData, int lngAddr, int lngSize)
        {
            if (lngAddr + lngSize > m_MemSize) return 2;
            if (m_bInit)
            {
                Marshal.Copy(mapView, bytData, lngAddr, lngSize);
            }
            else
            {
                return 1;
            }
            return 0;
        }

        public int Write(byte[] bytData, int lngAddr, int lngSize)
        {
            if (lngAddr + lngSize > m_MemSize) return 2;
            if (m_bInit)
            {
                Marshal.Copy(bytData, lngAddr, mapView, lngSize);
            }
            else
            {
                return 1;
            }
            return 0;
        }
 
    }
}
