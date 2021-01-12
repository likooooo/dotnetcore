#define DEBUG
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

#if DEBUG
#warning TEST IS DEFINED
#endif

namespace RuntimeDebug
{
    //https://docs.microsoft.com/zh-cn/dotnet/csharp/language-reference/attributes/general
    public sealed class DebugMsg
    {
        [Conditional("DEBUG")]
        public static void WriteDebugLine(string msg)
        {
            Console.WriteLine($"[DEBUG] {msg}");
        }

        [Obsolete("Obsolete")]
        [Conditional("DEBUG")]
        public static void Error(string msg)
        {
            Console.WriteLine($"[ERROR] {msg}");
        }
    }

    class DegbuMsgData
    {
        public string Message{get;protected set;}

        public string MemberName{get;protected set;}

        public string SourceFilePath{get;protected set;}

        public int SourceLineNumber{get;protected set;}
        public DateTime CurrentTime{get;protected set;}
        public DegbuMsgData(string message,[CallerMemberName] string memberName = "",[CallerFilePath] string sourceFilePath = "",[CallerLineNumber] int sourceLineNumber = 0)
        {
            CurrentTime = DateTime.Now.ToLocalTime();
            Message = message;
            MemberName = memberName;
            SourceFilePath = sourceFilePath;
            SourceLineNumber = sourceLineNumber;
            Console.WriteLine("Msg Time: " + _currentTime.ToString());
            Console.WriteLine("message: " + message);
            Console.WriteLine("member name: " + memberName);
            Console.WriteLine("source file path: " + sourceFilePath);
            Console.WriteLine("source line number: " + sourceLineNumber);
        }
    }
}
