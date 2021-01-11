using System;
using DB.JsonHelper;
using System.Collections.Generic;
using System.Text.Json;

namespace Sample
{
    class Program
    {
        struct Test
        {
            public string IsChecked{get;set;}
            public string IsRuning{get;set;}
        }
        static string filePath = "./bin/Debug/net5.0/test.json";
        static void Main(string[] args)
        {
            WriteTest();
            Console.WriteLine(JsonConvert.FilePath2String(filePath));
            ReadTest();
        }

        static void WriteTest()
        {
            IJsonWriter writer = new JsonWriter();
            writer.WriteStart();
            //writer.WriteBool("isRunning",true);
            writer.WriteProperty("dec");
            writer.WriteStringValue("abcde");
            writer.WriteString("language","c#");
            writer.WriteEnd();
            writer.Flush();
            writer.Write(filePath);
        }

        public static void ReadTest()
        {
            IJsonReader reader = new JsonReader();
            List<string> keys = new List<string>();
            List<string> value = new List<string>();
            keys.Add("dec");
            keys.Add("language");
            Utf8JsonReader utf8 = new Utf8JsonReader();
            reader.ReadString(filePath,keys,ref value,ref utf8);
            foreach (var item in value)
            {
                Console.WriteLine(item );
            }
            // foreach (var item in keys)
            // {
            //     if(reader.ReadString(filePath,item,ref value,ref utf8)) 
            //     {
            //         Console.WriteLine(item + " :" + value);

            //     }
            //     else
            //     {
            //         Console.WriteLine(item + ": not found val");
            //     }
                
            // }
        }
    }
}
