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
    // public class JsonReader_UTF8:IJsonReader
    // {
    //     public byte[] Bytes{get;private set;}
    //     public JsonReaderOptions Options{get;private set;}

    //     public JsonReader_UTF8(string filePath)
    //     {
    //         Bytes = Encoding.UTF8.GetBytes(File.ReadAllText(filePath));
    //         Options = new JsonReaderOptions
    //         {
    //             AllowTrailingCommas = true,
    //             CommentHandling = JsonCommentHandling.Skip
    //         };
    //     }
    //     public void CreateReader(ref Utf8JsonReader reader)
    //     {           
    //         reader = new Utf8JsonReader(Bytes, Options);
    //     }  

    //     //读取指定key的Value值，注意只能读取Value为String类型，否则会报错
    //     public List<string> ReadStringValues(List<string> keys)
    //     {
    //         Utf8JsonReader reader = new Utf8JsonReader(Bytes, Options);
    //         List<string> res = new List<string>(keys.Count);
    //         while (reader.Read())
    //         {
    //             switch (reader.TokenType)
    //             {
    //                 case JsonTokenType.PropertyName:
    //                 {
    //                     if(keys.Contains(reader.GetString()))
    //                     {
    //                         reader.Read();
    //                         switch (reader.TokenType)
    //                         {
    //                             case JsonTokenType.Number:             
    //                             case JsonTokenType.False:
    //                             case JsonTokenType.True:
    //                             case JsonTokenType.String:
    //                                 res.Add(reader.GetString());
    //                                 break;
    //                             default:
    //                                 break;
    //                         }
    //                     }
    //                     break;
    //                 }
    //             }
    //         }
    //         return res;
    //     } 
    // }

    // public class JsonWriter_UTF8:IJsonWriter
    // {
    //     JsonWriterOptions options;
    //     ArrayBufferWriter<byte> buffer;
    //     Utf8JsonWriter writer;
    //     public JsonWriter_UTF8()
    //     {
    //         options = new JsonWriterOptions
    //         {
    //             Indented = true
    //         };
    //         buffer = new ArrayBufferWriter<byte>();
    //         writer = new Utf8JsonWriter(buffer, options);
    //     }

    //     void IJsonWriter.WriteStart()
    //     {
    //         writer.WriteStartObject();
    //     }

    //     #region Tag Write
    //     void IJsonWriter.WriteProperty(string tag){writer.WritePropertyName(tag);}
    //     void IJsonWriter.WriteBase64StringValue(byte[] arry){writer.WriteBase64StringValue(new ReadOnlySpan<byte>(arry));}
    //     void IJsonWriter.WriteStringValue(string val){writer.WriteStringValue(val);}
    //     void IJsonWriter.WriteNumberValue(int val){writer.WriteNumberValue(val);}
    //     void IJsonWriter.WriteBoolValue(bool val){writer.WriteBooleanValue(val);}
    //     void IJsonWriter.WriteNullValue(){writer.WriteNullValue();}
    //     #endregion

    //     #region Key-Val
        
    //     void IJsonWriter.WriteBase64String(string key,byte[] arry)
    //     {
    //         writer.WriteBase64String(key,new ReadOnlySpan<byte>(arry));
    //     }

    //     void IJsonWriter.WriteString(string key,string val)
    //     {
    //         writer.WriteString(key,val);   
    //     }

    //     void IJsonWriter.WriteNumber(string key,int val)
    //     {
    //         writer.WriteNumber(key,val);  
    //     }
    //     void IJsonWriter.WriteBool(string key,bool val)
    //     {
    //         writer.WriteBoolean(key,val); 
    //     }
    //     void IJsonWriter.WriteNull(string key)
    //     {
    //         writer.WriteNull(key);  
    //     }
    //     #endregion

    //     void IJsonWriter.WriteEnd()
    //     {
    //         writer.WriteEndObject();
    //     }

    //     void IJsonWriter.Flush()
    //     {
    //         writer.Flush();
    //     }

    //     void IJsonWriter.Dispose()
    //     {
    //         writer.Dispose();
    //         buffer.Clear();
    //     }

    //     byte[] IJsonWriter.GetBytes()
    //         =>buffer.WrittenSpan.ToArray();

    //     string IJsonWriter.GetString()
    //         =>Encoding.UTF8.GetString(buffer.WrittenSpan.ToArray());  
        
    //     void IJsonWriter.Write(string filePath)
    //     {
    //         File.WriteAllBytes(filePath,buffer.WrittenSpan.ToArray());
    //     }
    // }



    // public class JsonHelper       
    // {       
    //     public JsonOperateEnum JsonOperate{get;private set;}
    //     public string FilePath{get;private set;}
    //     public JsonWriterOptions WriteOptions{get;set;}
    //     public ArrayBufferWriter<byte> buffer{get;set;}
    //     // public IJsonRead{get;private set;}
    //     // public IJsonWrite{get;private set;}
    //     public JsonHelper(JsonOperateEnum operate,string filePath = "")
    //     {
    //         JsonOperate = operate;
    //         FilePath = filePath;
    //         switch (operate)
    //         {
    //             case JsonOperateEnum.Read:
    //                 //IJsonRead
    //                 break;
    //             case JsonOperateEnum.Write:
    //                 //IJsonWrite
    //                 break;
    //             case JsonOperateEnum.ReadWrite:
    //                 //IJsonRead
    //                 //IJsonWrite
    //                 break;
    //             default:
    //                 break;
    //         }
    //         if(File.Exists(filePath))
    //         {

    //         }
    //         else
    //         {
                
    //         }
    //     }


    //     #region Find
    //     //按照指定路径查找节点
    //     public static JsonElement GetProperty(JsonDocument doc,params string[] searchTags)
    //     {
    //         JsonElement property = doc.RootElement;
    //         foreach (var item in searchTags)
    //         {
    //             property = property.GetProperty(item);
    //         }
    //         return property;
    //     }
    //     #endregion
      
    //     //字符串转json handle
    //     public static JsonDocument Parse(string str) => JsonDocument.Parse(str);  

    //     #region Json Opertor
    //     public static void Write()
    //     {
            
    //     }
    //     #endregion
    //     #region Json <-> String
        
    //     #endregion  
    // }
}
