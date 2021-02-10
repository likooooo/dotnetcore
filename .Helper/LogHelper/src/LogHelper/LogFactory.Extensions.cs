using System;
using System.Linq;
using System.Collections.Generic;

namespace LogHelper.Extension
{
    public static class LogPiplineOperatExtensions
    {
        //增加
        public static bool TryAddLogPipline(this LogFactory fac,ILogPipline pipline) 
        {
            if( fac._piplines.Count(s=>s.PiplineNum == pipline.PiplineNum) > 0)
            {
                return false;
            } 
            fac._piplines.Add(pipline);
            return true;
        }
        public static ILogPipline CreateLogPipline(this LogFactory fac,string pipName) 
        {
            ILogPipline pip = new LogPipline(fac._piplines.Count,pipName);
            fac._piplines.Add(pip);
            return pip;
        }

        //删除
        public static bool Remove(this LogFactory fac,ILogPipline pip) => fac._piplines.Remove(pip);
        public static void RemoveAll(this LogFactory fac) { fac._piplines.Clear(); }
        //查找
        public static ILogPipline GetLogPipline(this LogFactory fac,int idx) => idx < fac._piplines.Count ?fac._piplines[idx] : null;
        public static ILogPipline GetLogPipline(this LogFactory fac,string piplineName) => fac._piplines.Find(s => s.PiplineName == piplineName);
    }

    public static class LogMessageExtension
    {
        public static void AddError(this ILogPipline pipline , string str) { pipline.Add(new LogMessage(str,LogLevel.Error)); }
        public static void AddDebug(this ILogPipline pipline , string str) { pipline.Add(new LogMessage(str,LogLevel.Debug)); }   
        public static void AddInfo(this ILogPipline pipline , string str) { pipline.Add(new LogMessage(str,LogLevel.Info)); }
        public static void AddError(this ILogPipline pipline , params string[] str) { pipline.Add(new LogMessage(string.Join("", str),LogLevel.Error));}
        public static void AddDebug(this ILogPipline pipline ,params string[] str) { pipline.Add(new LogMessage(string.Join("", str),LogLevel.Debug)); }   
        public static void AddInfo(this ILogPipline pipline ,params string[] str) { pipline.Add(new LogMessage(string.Join("", str),LogLevel.Info)); }
        public static void AddError(this ILogPipline pipline , string seprator , params string[] str) { pipline.Add(new LogMessage(string.Join(seprator, str),LogLevel.Error));}
        public static void AddDebug(this ILogPipline pipline , string seprator ,params string[] str) { pipline.Add(new LogMessage(string.Join(seprator, str),LogLevel.Debug)); }   
        public static void AddInfo(this ILogPipline pipline , string seprator ,params string[] str) { pipline.Add(new LogMessage(string.Join(seprator, str),LogLevel.Info)); }
    }
}
