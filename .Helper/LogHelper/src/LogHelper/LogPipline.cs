using System;
using System.Collections;
using System.Collections.Generic;

namespace LogHelper
{
    public class LogMessageEnumerator:IEnumerator<ILogMessage>
    {
        private int currentIdx;
        private ILogMessage currentLog;
        private List<ILogMessage> _msgList;

        internal LogMessageEnumerator()
        {
            currentLog = null;
            currentIdx = -1;
        }

        public LogMessageEnumerator(List<ILogMessage> msgList):this(){_msgList = msgList;}

        object IEnumerator.Current { get => (object)currentLog;}
        ILogMessage IEnumerator<ILogMessage>.Current { get => currentLog;}

        bool IEnumerator.MoveNext() => ((currentLog = ++currentIdx <_msgList.Count ?_msgList[currentIdx]:null) != null);
        void IEnumerator.Reset() {currentIdx = -1;}

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            _msgList.Clear();
            _msgList = null;
            currentLog = null;
            currentIdx = -1;
        }
    }

    //Log管道属性
    public interface ILogPiplineAttributes:IEnumerable<ILogMessage>
    {
        int PiplineNum{get;}
        string PiplineName{get;}
        string LogFilePath{get;set;}
    }
    public interface ILogPipline:ILogPiplineAttributes
    {     
        void Add(ILogMessage msg);
        int MsgCount();
    }
    
    public class LogPipline:ILogPipline
    {
        internal List<ILogMessage> _msgList;
        public ILogMessage this[int idx] => _msgList[idx];
        public int PiplineNum{get;private set;}
        public string PiplineName{get;private set;}
        public string LogFilePath{get;set;}

        public LogPipline(){ _msgList = new List<ILogMessage>();}
        internal LogPipline(int piplineIdx,string piplineName):this()
        {
            PiplineNum = piplineIdx;
            PiplineName = piplineName;
            LogFilePath = piplineName + ".log";
        }

        public void Add(ILogMessage msg){_msgList.Add(msg);}
        public int MsgCount()=>_msgList.Count;

        IEnumerator<ILogMessage> IEnumerable<ILogMessage>.GetEnumerator()
        {
            return new LogMessageEnumerator(_msgList);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new LogMessageEnumerator(_msgList);
        }
    }

}
