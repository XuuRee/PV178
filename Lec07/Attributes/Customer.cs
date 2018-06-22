using System;
using System.Diagnostics;
using Attributes.CustomAttributes;

namespace Attributes
{
    [Serializable]
    [DebuggerDisplay("Customer name: {FullName}, age: {Age}")]
    public class Customer
    {       
        public string FullName { get; }

        public int Age { get; }

        [NonSerialized]
        [DebuggerDisplay("Pointer value is: {ordersPtr}")]
        private IntPtr ordersPtr;

        public Customer(string fullName, int age)
        {
            FullName = fullName;
            Age = age;
            ordersPtr = new IntPtr(1234);
        }
    }
}
