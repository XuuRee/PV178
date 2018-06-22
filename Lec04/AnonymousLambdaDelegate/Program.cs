using System;
using System.Collections.Generic;

namespace AnonymousLambdaDelegate
{
    /// <summary>
    /// All credit for this demo sample goes to Lukas Daubner, Filip Opaleny and Milan Mikus
    /// </summary>
    class Program
    {
        // Definice vlastniho delegata
        internal delegate void MyStringDelegate(string s);

        // & jeho vyvolani v metode Call
        private static void Call(MyStringDelegate del, string s) {
            del(s);
        }

        //Action bere pouze typove parametry
        private static void GenericCall(Action del) {
            del();
        }

        private static void GenericCall(Action<string> del, string s) {
            del(s);
        } 

        //Func vraci naposledy uvedeny typ, kterym je parametrizovan
        private static void GenericCall(Func<string> del) {
            Console.WriteLine(del());
        }

        private static void GenericCall(Func<string, string> del, string s) {
            Console.WriteLine(del(s));
        }

        static void Main(string[] args)
        {
            // Otestovani samostatne prace na cviceni (zadani viz Lab04_Tasks)
            //Solution.TestAll(); 

            //CustomDelegates();

            AnonymousMethods();

            //LambdaExpressions();
        }

        private static void CustomDelegates()
        {
            Call(Console.WriteLine, "And now for something completely different.");
            Call(Console.Write, "I am different!" + Environment.NewLine);

            MyStringDelegate del = Console.WriteLine;
            del("I am original!");

            del = Console.WriteLine;
            del("I am also original!");

            MyStringDelegate del2 = Console.WriteLine;
            del2 += Console.WriteLine;
            del2("Multiple! Yey!");
        }

        private static void AnonymousMethods()
        {

            Call(delegate(string s) { Console.WriteLine($"{s}, she wrote."); }, "Murder");
             
            MyStringDelegate del3 = delegate(string s) { Console.WriteLine($"{s}, she wrote."); };
            Call(del3, "Murder");

            //Anonymní metody jako predikáty
            var list = new List<int> {1, 2, 3, 4, 5, 6, 7, 8, 9, 10};

            var filteredList = list.FindAll(delegate(int item) { return (item % 2) == 0; });

            foreach (var item in filteredList)
            {
                Console.Write(item + " ");
            }
            Console.Write(Environment.NewLine);
        }
        
        /// <summary>
        /// Lambda vyraz je ve tvaru: (parametry) => {tělo funkce}
        /// </summary>
        private static void LambdaExpressions()
        {
            var list = new List<int> {1, 2, 3, 4, 5, 6, 7, 8, 9, 10};

            var filteredList2 = list.FindAll(item => (item % 2) == 0);

            foreach (var item in filteredList2)
            {
                Console.Write(item + " ");
            }
            Console.Write(Environment.NewLine);

            GenericCall(() => Console.Beep());
            GenericCall(s => Console.WriteLine((s.Length > 5).ToString()), "Long");
            GenericCall(s => Console.WriteLine((s.Length > 5).ToString()), "Too Long");

            GenericCall(() => "Only this");
            GenericCall(s => s + " and that.", "This");

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        public void Hack() {
            
        }

        public void Send() {
            
        }

        public void Find() {
            
        }

        public void Hello() {
            string command = "";

            switch (command) {
                case "hack": Hack();
                    break;
            }


        }
    }
}
