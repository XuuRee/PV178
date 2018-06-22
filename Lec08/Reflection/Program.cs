using System;

namespace Reflection
{
    /// <summary>
    /// Ukol: implementace nize uvedenych metod
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            DynamicallySetCustomerPropertyViaPrivateSetter();
            DynamicallyInvokeStaticCustomerMethod();
            GetAllNonAbstractClassesImplementingIAnimal();
        }

        /// <summary>
        /// Nastavte property "SomeValue" instanci tridy Customer na libovolnou hodnotu 
        /// </summary>
        private static void DynamicallySetCustomerPropertyViaPrivateSetter()
        {
            var customer = new Customer("Elvis");
            var propInfo = typeof(Customer).GetProperty(nameof(Customer.SomeValue));
            propInfo?.SetValue(customer, 20);
            Console.WriteLine(customer.SomeValue);
        }

        /// <summary>
        /// Vyvolejte statickou metodu "ImportantPrivateStaticVoidMethod" tridy Customer
        /// </summary>
        private static void DynamicallyInvokeStaticCustomerMethod()
        {
            //var customer = new Customer("Method");
            //var method = typeof(Customer).GetMethod("ImportantPrivateStaticVoidMethod", BindingFlags.Static | BindingFlags.NonPublic);
            //method?.GetMethod(nameof(Customer.));
        }

        /// <summary>
        /// Ziskejte vsechny neabstraktni tridy z tohoto projektu, ktere implementuji rozhrani IAnimal
        /// Hint: vyuzijte funkcionalitu tridy Assembly: Assembly.GetExecutingAssembly()...
        /// </summary>
        private static void GetAllNonAbstractClassesImplementingIAnimal()
        {
            //TODO
        }
    }
}
