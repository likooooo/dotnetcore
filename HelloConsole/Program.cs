using System;

namespace LinqTest
{
    class Program
    {
        public interface IMyInterface
        {
            int Val{get;}
        }
        public class MyClass:IMyInterface
        {
            public int Val{get;protected set;}
            public MyClass(){Val = 10;}
        }
        public class MyClass1:MyClass,IMyInterface
        {
            public new int Val{get;protected set;}
            public MyClass1(){Val = 100;}
        }
        static void Main(string[] args)
        {
            var c1 = new MyClass1();
            Console.WriteLine(c1.Val);
        }
    }
}
