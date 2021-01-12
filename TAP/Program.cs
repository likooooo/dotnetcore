using System;
using System.Threading.Tasks;

namespace TAP
{
    class Program
    {
        static void Main(string[] args)
        {
            int length = 0;
            Task myTsk = Task.Run
            (
                async delegate
                {
                    for(int i = Int16.MinValue ;i<Int16.MaxValue;i++)
                    {
                        await Task.Yield();
                        length++;
                        //...
                    }
                }
            );
            Console.WriteLine("Hello World!");
            Task.WaitAny(myTsk);
            Console.WriteLine(length);
        }
    }
}
