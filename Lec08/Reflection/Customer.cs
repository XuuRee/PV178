using System;

namespace Reflection
{
    public class Customer
    {
        private int age;

        public Customer(string name)
        {
            Name = name;
        }

        public string Name { get; }
        public string Address { get; set; }
        public int SomeValue { get; private set; }

        public int ImportantCalculation()
        {
            return 1000;
        }

        public void ImportantVoidMethod(int param1, string param2)
        {
            Console.WriteLine($"Got invoked with {param1} and {param2}");
        }

        private void ImportantPrivateVoidMethod(int param1, string param2)
        {
            Console.WriteLine($"Got invoked with {param1} and {param2}");
        }

        private static void ImportantPrivateStaticVoidMethod(int param1, string param2)
        {
            Console.WriteLine($"Got invoked with {param1} and {param2}");
        }

        public enum SomeEnumeration
        {
            ValueOne = 1
            , ValueTwo = 2
        }

        public class SomeNestedClass
        {
            private string someString;
        }
    }
}
