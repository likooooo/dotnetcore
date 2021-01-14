using System;

namespace LinqTest
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] d = new int[]{1,3,5,7,9};
            int[] copy = new int[d.Length];
            int idx = 0;
            foreach (var dd in d)
            {
                copy[idx++] = dd;
            }
            foreach (var dd in copy)
            {
                Console.WriteLine(dd);
            }
        }
    }
}
