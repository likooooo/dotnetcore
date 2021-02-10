using System;
using Xunit;
using LogHelper.Extension;
using System.Threading;
using System.Threading.Tasks;

namespace LogHelper.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var logFactory = LogFactoryEntry.GetLogFactory();
            LogPipline pip = new LogPipline();
            Assert.True(logFactory.TryAddLogPipline(pip));
            CancellationToken  token = new CancellationToken (false);
            Task task = pip.ExecuteAsyncWriteLog(token);
            tacdsk.Start();
            Assert.False(task.IsCanceled);
            Assert.False(task.IsCompleted);
        }
    }
}
