using System;
using System.Linq;
using System.Reflection;

namespace ReflectionTest
{
    public class CoderDefAttribute : System.Xml.Serialization.XmlIgnoreAttribute 
    {
        public bool a;
        public string b;
        public CoderDefAttribute(bool a,string b) 
        {
            this.a = a;
            this.b = b;
        }

    }
    class TestClass
    {
        [CoderDefAttribute(true,"asd")]
        public int member;
        public int Property{get=>member;}
        public TestClass(){}
        public void PrintWhoCallThisMethord()
        {
            MethodBase method = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            Console.WriteLine(method.ReflectedType.FullName);
        }
    }
    class Program
    {
        //https://docs.microsoft.com/zh-cn/dotnet/api/system.reflection.assembly?view=net-5.0
        static void Main(string[] args)
        {
            TestClass tc = new TestClass();
            tc.PrintWhoCallThisMethord();
            Type t = typeof(TestClass);
            //获取所有方法 
            System.Reflection.MethodInfo[] methods = t.GetMethods(); 
            Console.WriteLine("方法:");
            foreach (var i in methods)
            {
                Console.WriteLine("  " + i);
            }
            //获取所有成员 
            System.Reflection.MemberInfo[] members = t.GetMembers(); 
            Console.WriteLine("成员对象:");
            foreach (var i in members)
            {
                Console.WriteLine("  " + i);
            }
            //获取所有属性 
            System.Reflection.PropertyInfo[] properties = t.GetProperties();
            Console.WriteLine("索引器:");
            foreach (var i in properties)
            {
                Console.WriteLine("  " + i);
            }
            //获取Attribute
            var attributes = t.GetCustomAttributes(typeof(CoderDefAttribute), false);
            Console.WriteLine("Attribute:");
            foreach(var mInfo in members) 
            {
                // Iterate through all the Attributes for each method.
                foreach (Attribute attr in Attribute.GetCustomAttributes(mInfo)) 
                {
                    // Check for the AnimalType attribute.
                    if (attr.GetType() == typeof(CoderDefAttribute))
                    {
                        Console.WriteLine("m_Info {0} has [Attribute] : bool {1} ,string {2} .",mInfo.Name, ((CoderDefAttribute)attr).a,((CoderDefAttribute)attr).b);
                    }
            }
        }

            // Console.WriteLine("Assembly Full Name:");
            // Console.WriteLine(t.FullName);
            // Console.WriteLine("\nAssembly CodeBase:");
            // Console.WriteLine(t.CodeBase);
        }
    }
}
