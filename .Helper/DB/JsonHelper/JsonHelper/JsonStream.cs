using System;
using System.Linq;
using System.IO;
using System.Buffers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace DB.JsonHelper
{
    public interface IJsonReader:IReadAttribute
    {
        bool ReadString(string jsonFile,string key,ref string val,ref Utf8JsonReader reader);
        bool ReadString(string jsonFile,string key,ref List<string> valArry,ref Utf8JsonReader reader);
        bool ReadString(string jsonFile,List<string> key,ref List<string> valArry,ref Utf8JsonReader reader);
    }
    
    public interface IJsonWriter:IWriteAttribute
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

        void Write(string filePath);
    } 
  
    public class JsonReader:ReadAttribute,IJsonReader
    {
        string jsonFile;
        public JsonReader()
        {
            jsonFile = "";
        }

        public bool ReadString(string jsonFile,string key,ref string val,ref Utf8JsonReader reader)
        {
            if(this.jsonFile !=jsonFile)
            {
                this.jsonFile = jsonFile;
                reader = new Utf8JsonReader(JsonConvert.FilePath2Bytes(jsonFile), options);
            }
            while (reader.Read())
            {
                if(reader.TokenType == JsonTokenType.PropertyName && reader.ValueTextEquals(key))
                {
                    if(reader.Read())
                    {
                        val = reader.GetString();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return false;
        }
      
        public bool ReadString(string jsonFile,string key,ref List<string> valArry,ref Utf8JsonReader reader)
        {           
            if(this.jsonFile !=jsonFile)
            {
                this.jsonFile = jsonFile;
                reader = new Utf8JsonReader(JsonConvert.FilePath2Bytes(jsonFile), options);
            }            while (reader.Read())
            {
                if(reader.TokenType == JsonTokenType.PropertyName && reader.ValueTextEquals(key))
                {
                    if(reader.Read())
                    {
                        valArry.Add(reader.GetString());
                        continue;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return false;
        }
        public bool ReadString(string jsonFile,List<string> key,ref List<string> valArry,ref Utf8JsonReader reader)
        {      
            if(this.jsonFile !=jsonFile)
            {
                this.jsonFile = jsonFile;
                reader = new Utf8JsonReader(JsonConvert.FilePath2Bytes(jsonFile), options);
            }
            valArry = new List<string>(key.Count);
            while (reader.Read())
            {
                if(reader.TokenType == JsonTokenType.PropertyName && key.Contains(reader.GetString()))
                {
                    if(reader.Read())
                    {
                        valArry.Add(reader.GetString());
                        continue;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return false;
        }
    }

    public class JsonWriter:WriteAttribute,IJsonWriter
    {
        ArrayBufferWriter<byte> buffer;
        Utf8JsonWriter writer;
        public JsonWriter()
        {
            buffer = new ArrayBufferWriter<byte>();
            writer = new Utf8JsonWriter(buffer, options);
        }
        public JsonWriter(byte[] originData)
        {
            buffer = new ArrayBufferWriter<byte>();
            BuffersExtensions.Write(buffer,new ReadOnlySpan<byte>(originData));
            writer = new Utf8JsonWriter(buffer, options);
        }

        public void WriteStart()
        {
            writer.WriteStartObject();
        }

        #region Tag Write
        public void WriteProperty(string tag){writer.WritePropertyName(tag);}
        public void WriteBase64StringValue(byte[] arry){writer.WriteBase64StringValue(new ReadOnlySpan<byte>(arry));}
        public void WriteStringValue(string val){writer.WriteStringValue(val);}
        public void WriteNumberValue(int val){writer.WriteNumberValue(val);}
        public void WriteBoolValue(bool val){writer.WriteBooleanValue(val);}
        public void WriteNullValue(){writer.WriteNullValue();}
        #endregion

        #region Key-Val       
        public void WriteBase64String(string key,byte[] arry)
        {
            writer.WriteBase64String(key,new ReadOnlySpan<byte>(arry));
        }

        public void WriteString(string key,string val)
        {
            writer.WriteString(key,val);   
        }

        public void WriteNumber(string key,int val)
        {
            writer.WriteNumber(key,val);  
        }
        public void WriteBool(string key,bool val)
        {
            writer.WriteBoolean(key,val); 
        }
        public void WriteNull(string key)
        {
            writer.WriteNull(key);  
        }
        #endregion

        public void WriteEnd()
        {
            writer.WriteEndObject();
        }

        public void Flush()
        {
            writer.Flush();
        }

        public void Dispose()
        {
            writer.Dispose();
            buffer.Clear();
        }
        
        public void Write(string filePath)
        {
            File.WriteAllBytes(filePath,buffer.WrittenSpan.ToArray());
        }
    }
}