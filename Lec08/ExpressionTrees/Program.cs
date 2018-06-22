using System.Collections.Generic;

namespace ExpressionTrees
{
    class Program
    {
        /// <summary>
        /// Zadani:
        /// Za pomoci sestaveneho expression tree vyberte z nasledujiciho 
        /// seznamu jmena vsech osob, pro ktere plati nasledujici:
        /// osoba je single, nebo jeji vek spada do rozmezi 19 - 25 let.
        /// Tip: pri implementaci se muze hodit Expression.PropertyOrField(...)
        /// </summary>
        static void Main(string[] args)
        {
            var people = new List<Person>
            {
                new Person("Thomas", 26, false),
                new Person("Daisy", 23, true),
                new Person("John", 32, false),
                new Person("Kate", 18, false)
            };

            // TODO...


        }
    }
}
