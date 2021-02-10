using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace LogHelper
{
    public sealed class LogFactory
    {
        internal List<ILogPipline> _piplines;
        internal Dictionary<int,Task> _pipTasks;
        internal LogFactory()
        {
            _piplines = new List<ILogPipline>();
            _pipTasks = new Dictionary<int,Task>();
        }
    }

}
