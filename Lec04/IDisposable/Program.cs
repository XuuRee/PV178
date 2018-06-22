namespace IDisposable
{
    class Program
    {
        static void Main(string[] args)
        {
            // Proper use of IDisposable 
            var testClassInstance1 = new TestClass();

            // Use testClassInstance...

            testClassInstance1.Dispose();


            // Using block example (.Dispose() does not have to be called)
            using (var testClassInstance2 = new TestClass())
            {
                // Use testClassInstance...
            }
        }
    }
}
