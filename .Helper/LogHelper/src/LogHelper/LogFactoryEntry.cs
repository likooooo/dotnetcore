using System;
using System.Collections.Generic;

namespace LogHelper
{
    //Log管道属性
    public static class LogFactoryEntry
    {
        private static LogFactory _logFactory;
        private static readonly object _lock = new object();
        
        public static LogFactory GetLogFactory()
        {
            lock(_lock)
            {
                if(_logFactory == null)
                {
                    _logFactory = new LogFactory();
                }
            }
            return _logFactory;
        }
    }
}
