using System;
using DB.JsonHelper;

namespace Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            IJsonWriter writer = new JsonWriter_UTF8();
            writer.WriteStart();
            //writer.WriteProperty("title");
            //writer.WriteStringValue("test data");
            writer.WriteBool("isRunning",true);
            writer.WriteProperty("title");
            writer.WriteStringValue("test data");

            writer.WriteEnd();
            writer.Flush();
            writer.Write("test.json");
            Console.WriteLine(writer.GetString());
        }
    }
}
