#define TEST
using System;
using System.Diagnostics;

namespace CompilerAttributeTest
{
    class Program
    {
        static void Main(string[] args)
        {
            RuntimeDebug.DegbuMsgData data = new RuntimeDebug.DegbuMsgData("asd");
            RuntimeDebug.DebugMsg.Msg("Hello World!");
        }
    }
}
