using System;
using System.Linq;
using System.IO;
using System.Buffers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

//https://docs.microsoft.com/zh-cn/dotnet/api/system.text.json?view=netcore-3.0
namespace DB.JsonHelper
{
    //Json的IO操作
   public enum JsonOperateEnum
    {
        Read,
        Write,
        ReadWrite
    }

    //序列化
    public static class JsonSerialize<T>
    {
        //序列化
        public static string Serialize(T jsList) 
            => JsonSerializer.Serialize<T>(jsList);

        //反序列化
        public static T Deserialize(string str) 
            => JsonSerializer.Deserialize<T>(str);
    }

    public static class JsonConvert
    {
        //当前环境的字符编码
        public static Encoding EnvironmentEncoding = Encoding.UTF8;
     
        /// <summary>
        /// 字符串转byte[]
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] String2Bytes(string data) =>Encoding.UTF8.GetBytes(data);
        
        /// <summary>
        /// byte[]转字符串
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string Bytes2String(byte[]  data) =>Encoding.UTF8.GetString(data);


        /// <summary>
        /// 读取指定路径文件的所有字节
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static byte[] FilePath2Bytes(string filePath)=>File.ReadAllBytes(filePath);

        /// <summary>
        /// 指定数据保存至指定路基
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="data"></param>
        public static void Bytes2FilePath(string filePath,byte[] data)=>File.WriteAllBytes(filePath,data);

        /// <summary>
        /// 读取指定路基文件的所有字符串
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string FilePath2String(string filePath)=>File.ReadAllText(filePath,EnvironmentEncoding);

        /// <summary>
        /// 将指定字符串存入目录
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="data"></param>
        public static void String2FilePath(string filePath,string data)=>File.WriteAllText(filePath,data,EnvironmentEncoding);
    }
}