using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace LogHelper.Extension
{
    public static class LogAsyncExtensions
    {
        public static void RunLogPipline(this LogFactory fac,LogPipline pipline , CancellationToken stoppingToken) { fac._pipTasks.Add(pipline.PiplineNum , pipline.ExecuteAsyncWriteLog(stoppingToken));}
        public static async Task ExecuteAsyncWriteLog(this LogPipline pipline, CancellationToken stoppingToken)
        {
            StreamWriter sw = File.Exists(pipline.LogFilePath) ? File.AppendText(pipline.LogFilePath) : File.CreateText(pipline.LogFilePath);

            int idx = -1;
            while (!stoppingToken.IsCancellationRequested)
            {
                while(++idx < pipline.MsgCount())
                {
                    sw.WriteLine(pipline[idx].LogString());
                }
                await Task.Delay(1000, stoppingToken);
            }
            sw.Dispose();
        }
    }

}
