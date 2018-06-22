using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tuples
{
    class Program
    {
        static void Main(string[] args)
        {
            // Tuple, vyzaduje alokaci Tuple objektu, neintuitivni pouziti...
            Tuple<int, int> oldTuple = Tuple.Create(30, 40);

            // ValueTuple, narozdil od Tuple se jedna o strukturu (momentalne pridany jako NuGet)
            ValueTuple<int, char, string, StringBuilder> valueTuple1 = (1, 'a', "text", new StringBuilder());

            // Zapis typu ValueTuple lze zjednodusit nasledovne, pripadne lze pouzit implicitni typovani
            (int, char, string, StringBuilder) valueTuple2 = (1, 'a', "text", new StringBuilder());

            // K hodnotam tuplu lze pristupovat skrze fieldy, ktere maji defaultni identifikatory ItemX
            valueTuple2.Item4.AppendLine(valueTuple2.Item3);

            // Coz neni prilis intuitivni, doporucuje se tedy u typu uvadet identifikatory
            (int id, char level, string desc) namedValueTuple = (1, 'a', "text");

            // Dekonstrukce tuplu - umoznuje jej snadno rozdelit na jednotlive hodnoty
            (int id, char level, string desc)  = namedValueTuple;
            Console.WriteLine($"id: {id}, level: {level}, desc: {desc}");

            // Ukazka pouziti tuples (viz konstruktor tridy Person)
            var person = new Person(21, true, "John Snow");

            // Dekonstrukce instance tridy Person, lze jej provest diky tomu, ze dana trida obsahuje metodu Deconstruct
            var (likesHockey, age, name) = person;
            Console.WriteLine($"{name}, aged {age}, " + (likesHockey ? "likes": "does not like") + " ice hockey.");

            // Opet se pouzije pretizena varianta metody Deconstruct, znak '_' udava, ze nas hodnota veku nezajima
            var (_, name2) = person;

            // GetFullName vraci vice hodnot skrze tuple
            var (firstName, lastName) = person.GetFullName();

            // Tuples lze take vyuzit napriklad pro kolekce...
            var minMaxList = new List<(int min, int max)> {(1, 100)};
            // ...
        }
    }
}
