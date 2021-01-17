using System;

namespace ExtentionTest
{
    class Program
    {
        static void Main(string[] args)
        {
            // int max =3;
            // int idx = 0;
            // while (idx++<max)
            // {
            //     Console.WriteLine(idx);
            // }
            int[] arry = new int[]{0,1,2,3,4,5};
            int idx = 0;
            fixed(int* pArray = &arry[0])
            {
                int* p = pArray;
                while (idx<arry.Length)
                {
                    p++ =  idx;
                    idx++;
                }
                idx = 0;
                while (idx<arry.Length)
                {
                    Console.WriteLine(arry[idx]);
                    idx++;
                }
            }
            
        }
    }
    interface ITest
    {
        void A();
    }
    class Test:ITest
    {
        public int TestFlg;
        public void A()
        {
            this.Extention();
            Console.WriteLine("I'm A");
        }
    }
    static class GG
    {
        public static int Extention(this Test t) 
        {
            Console.WriteLine("I'm Extention");
            return t.TestFlg;
        }
    }
}
