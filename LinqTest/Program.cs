using System;
using System.Linq;

namespace HelloConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("#Start Linq Test");
            int[] arry = new int[]{1,2,3,4,5,6};
            Console.WriteLine
            (
                string.Format
                (
                    "    Sum:{0}\n    Min:{1}\n    Max:{2}\n    Text:{3}",
                    arry.Sum(),
                    arry.Min(),
                    arry.Max(),
                    string.Concat(arry.Select(s=>s.ToString()))
                )
            );
            Console.WriteLine("#End Linq Test");
        }
    }
}