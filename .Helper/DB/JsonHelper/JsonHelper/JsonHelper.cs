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
    public static class IJsonSerializewhere<T> where T:IComparable<T>, 
        System.Collections.Generic.IEnumerable<T>, 
        System.Collections.Generic.IList<T>, 
        System.Collections.Generic.IReadOnlyCollection<T>, 
        System.Collections.Generic.IReadOnlyList<T>
    {
        //序列化
        public static string Serialize(List<T> jsList) 
            => JsonSerializer.Serialize(jsList, jsList.GetType());
        public static string Serialize(T jsList) 
            => JsonSerializer.Serialize<T>(jsList);
        //并行
        public static async Task SerializeAsync(T value, Stream stream)
            =>await JsonSerializer.SerializeAsync<T>(stream,value);
        //反序列化
        public static List<T> Deserialize(string str) 
            => JsonSerializer.Deserialize<List<T>>(str);
        public static async Task DeserializeAsync(Stream stream)
            =>await JsonSerializer.DeserializeAsync<T>(stream);
    }
    public interface IJsonWriter
    { 
        void WriteStart();
        void WriteProperty(string tag);
        void WriteBase64StringValue(byte[] arry);
        void WriteStringValue(string val);
        void WriteNumberValue(int val);
        void WriteBoolValue(bool val);
        void WriteNullValue();

        void WriteBase64String(string key,byte[] arry);
        void WriteString(string key,string val);
        void WriteNumber(string key,int val);
        void WriteBool(string key,bool val);
        void WriteNull(string key);
        void WriteEnd();

        void Flush();
        void Dispose();

        byte[] GetBytes();
        string GetString();
        void Write(string filePath);
    }

    public interface IJsonReader
    {
        
    }

    public class JsonWriter_UTF8:IJsonWriter
    {
        JsonWriterOptions options;
        ArrayBufferWriter<byte> buffer;
        Utf8JsonWriter writer;
        public JsonWriter_UTF8()
        {
            options = new JsonWriterOptions
            {
                Indented = true
            };
            buffer = new ArrayBufferWriter<byte>();
            writer = new Utf8JsonWriter(buffer, options);
        }


        void IJsonWriter.WriteStart()
        {
            writer.WriteStartObject();
        }

        #region Tag Write
        void IJsonWriter.WriteProperty(string tag){writer.WritePropertyName(tag);}
        void IJsonWriter.WriteBase64StringValue(byte[] arry){writer.WriteBase64StringValue(new ReadOnlySpan<byte>(arry));}
        void IJsonWriter.WriteStringValue(string val){writer.WriteStringValue(val);}
        void IJsonWriter.WriteNumberValue(int val){writer.WriteNumberValue(val);}
        void IJsonWriter.WriteBoolValue(bool val){writer.WriteBooleanValue(val);}
        void IJsonWriter.WriteNullValue(){writer.WriteNullValue();}
        #endregion

        #region Key-Val
        
        void IJsonWriter.WriteBase64String(string key,byte[] arry)
        {
            writer.WriteBase64String(key,new ReadOnlySpan<byte>(arry));
        }

        void IJsonWriter.WriteString(string key,string val)
        {
            writer.WriteString(key,val);   
        }

        void IJsonWriter.WriteNumber(string key,int val)
        {
            writer.WriteNumber(key,val);  
        }
        void IJsonWriter.WriteBool(string key,bool val)
        {
            writer.WriteBoolean(key,val); 
        }
        void IJsonWriter.WriteNull(string key)
        {
            writer.WriteNull(key);  
        }
        #endregion

        void IJsonWriter.WriteEnd()
        {
            writer.WriteEndObject();
        }

        void IJsonWriter.Flush()
        {
            writer.Flush();
        }

        void IJsonWriter.Dispose()
        {
            writer.Dispose();
            buffer.Clear();
        }

        byte[] IJsonWriter.GetBytes()
            =>buffer.WrittenSpan.ToArray();

        string IJsonWriter.GetString()
            =>Encoding.UTF8.GetString(buffer.WrittenSpan.ToArray());  
        
        void IJsonWriter.Write(string filePath)
        {
            File.WriteAllBytes(filePath,buffer.WrittenSpan.ToArray());
        }
    }

    public class JsonHelper       
    {
        public JsonHelper()
        {
            //JsonDocument document = JsonDocument.Parse(person);
        }
        static void CreateDoc()
        {

        }
        static void CreateProperty()
        {

        }
        static void CreateElement()
        {

        }
        #region read json
        #endregion
        #region write json
        #endregion
        #region Delete
        #endregion

        #region Find
                //按照指定路径查找节点
        public static JsonElement GetProperty(JsonDocument doc,params string[] searchTags)
        {
            JsonElement property = doc.RootElement;
            foreach (var item in searchTags)
            {
                property = property.GetProperty(item);
            }
            return property;
        }
        #endregion
      
        //字符串转json handle
        public static JsonDocument Parse(string str) => JsonDocument.Parse(str);  

        #region Json Opertor
        public static void Write()
        {
            
        }
        #endregion
        #region Json <-> String
        
        #endregion  
    }
}
