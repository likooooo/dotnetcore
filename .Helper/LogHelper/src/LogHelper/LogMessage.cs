using System;

namespace LogHelper
{
    public interface ILogMessage
    {
        DateTime TimeStamp{get;}
        LogLevel Level{get;}
        string Context{get;}
        public string LogString();
    }
    //Log基类数据结构
    public class LogMessage:ILogMessage
    {
        public static string TimeFormat = "yyyy-MM-dd HH:mm:ss:ffff";
        public DateTime TimeStamp{get;private set;}
        public string Context{get;private set;}
        public LogLevel Level{get;private set;}
        public LogMessage(){}
        internal LogMessage(string context,LogLevel level)
        {
            Context = context;
            Level = level;
            TimeStamp = DateTime.Now;
        }
        public virtual string LogString() =>$"[{TimeStamp.ToString(TimeFormat)}] [{Level.ToString()}] {Context}";
    }
}
