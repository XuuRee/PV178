using System;
using System.Collections.Generic;
using System.Text;
using MyIntList = System.Collections.Generic.List<int>;

namespace TypesAndConversions_02
{
    /// <summary>
    /// Based on demo samples from Rudolf Wittner and David Kadlec 
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            // Reseni samostatne prace (zadani viz nize)
            //Solution.Task01();
            //Solution.Task02();

            //TypeChecking();
            //Aliases();
            //TypeSafety();
            //BoxingVsUnboxing();
            //ImplicitVsExplicit();
            //StaticVsDynamicTyping();
            //Nullables();
            ReferenceVsValueTypesPassing();
        }

        /// <summary>
        /// pekny clanek o tride Type: http://www.dotnetperls.com/type
        /// </summary>
        private static void TypeChecking()
        {
  
            // Operator nameof slouzi pro ziskani nazvu promenne, typu nebo napriklad clenu tridy
            Console.WriteLine(nameof(TypeChecking));

            Type type = typeof(StringBuilder);
           
            var tmp = type.IsClass;
            var tmpp = type.BaseType.Name;

            
            
            // ukazka pouziti klicoveho slova is / operatoru typeof / metody GetType():
            object builderObject = new StringBuilder();
            // kontrola typu objektu builder
            if (builderObject.GetType() == typeof(StringBuilder)) // GetType vraci typ objektu nad kterym je volan, operator typeof vraci typ tridy 
            {
                var builderr = (StringBuilder)builderObject;
                // blok kodu ktery chceme vykonat nad objektem s typem StringBuilder
                // podminka v if-u se da nahradit klicovym slovem: is
                Console.WriteLine("YES!");
            }

            // Nove lze v C# 7.0 vyuzit is expression pattern matching,
            if (builderObject is StringBuilder builder)
            {
                //var builder = (StringBuilder)builderObject;
                // builder je v tomto pripade promenna typove patternu, 
                // ktera je pri splneni podminky inicializovana
                builder.AppendLine("It's a builder :)");
                Console.WriteLine(builder.ToString());
            }
            // ukazka pouziti klicoveho slova as:
            var derived = new Derived();
            //var baseInstance = (Base)derived; 
            var baseInstance = derived as Base;         
            Console.WriteLine(baseInstance.ToString());

            Console.ReadKey();
        }

        /// <summary>
        /// aliasy pro jednotlive typy
        /// </summary>
        private static void Aliases()
        {
            // int vs Int32 - int je alias 
            int numberDeclaredWithTypeAlias;
            Int32 numberWithSpecificType;   // typ Int32 je poskytovany .NET frameworkem

            //vlastni aliasy
            MyIntList myList = new List<int>();
            MyIntList mySecondList = new MyIntList();
            myList.Add(5);
            mySecondList.Add(6);

            // podobne u ostatnich typu viz https://msdn.microsoft.com/en-us/library/ya5y69ds.aspx
        }

        /// <summary>
        /// typova kontrola
        /// </summary>
        private static void TypeSafety()
        {
            Console.WriteLine(Environment.NewLine + nameof(TypeSafety));
            int i = 10;
            float f = i;    // int muze byt implicitne pretypovan na float -> bezpecny bez vyjimek
            Console.WriteLine("i:{0} f:{1}", i, f);

            f = 15.8f;
            i = Convert.ToInt32(f); // nutne explicitne pretypovat, viz dale trida Convert
            //i = (int)f; 
            //i=f;
            Console.WriteLine(i);

            string s = "20";
            //i = Convert.ToInt32(s);
            //i = int.Parse(s); //pri neplatne hodnote napr: s = "abcd" vyhazuje vyjimku
            int.TryParse(s, out i); // pokud se konverze nepodari, bude mit i defaultni hodnotu

            Console.WriteLine(i);

            bool canBeParsed = int.TryParse(s, out _); // pokud nas nezajima zparsovana hodnota

            //is pattern matching lze dobre kombinovat napriklad s Try... metodami
            object message = "42";
            if (message is string msg && int.TryParse(msg, out int number))
            {
                Console.WriteLine($"Message contains number: {number}");
            }


            Console.ReadKey();
        }

        /// <summary>
        /// Boxing - "zabaleni" hodnotoveho typu (struktury) do objektu, zasobnik -> halda
        /// Unboxing - ziska hodnotovy typ z objektu, je provaden explicitne narozdil od unboxingu
        /// </summary>
        private static void BoxingVsUnboxing()
        {
            Console.WriteLine(Environment.NewLine + nameof(BoxingVsUnboxing));
            int i = 4;
            // boxing
            object obj = i;
            // unboxing
            int j = (int)obj;

            Console.WriteLine(i);
            Console.WriteLine(j);
            Console.WriteLine(obj);

            Console.ReadKey();
        }

        /// <summary>
        /// implicit vs. explicit typing (all static)
        /// </summary>
        private static void ImplicitVsExplicit()
        {
            Console.WriteLine(Environment.NewLine + nameof(ImplicitVsExplicit));
            int x = 10; //explicitne typovana premenna
            var y = 15; //implicitne typovana premenna
            Console.WriteLine(x.GetType() == y.GetType());
    
            Console.ReadKey();
        }

        /// <summary>
        /// dynamic - type is known at runtime (!IntelliSense), predpoklada se ze umi jakoukoliv metodu -> mozny vznik chyb za behu
        /// dynamic - zajimavost - "The type is a static type, but an object of type dynamic bypasses static type checking..." viz http://stackoverflow.com/questions/25124664/why-is-c-sharp-dynamic-type-static
        /// vs.
        /// static - type is known at compile-time, pouzivat co nejvic, vykonostne daleko lepsi nez dynamicky datovy typ
        /// </summary>
        private static void StaticVsDynamicTyping()
        {
            Console.WriteLine(Environment.NewLine + nameof(StaticVsDynamicTyping));
            var vr = "hello world!";
            //vr = 25;
            Console.WriteLine(vr.Length);

            dynamic dyn = "hello world!";
            dyn = 25;
           // dyn.Hello(); // Intelli Sense
            Console.WriteLine(dyn.Length);
            //Console.WriteLine(dyn);


            Console.ReadKey();
        }

        /// <summary>
        /// ukazka nulovatelnych datovych typu, pristup pres HasValue a Value viz https://msdn.microsoft.com/en-us/library/b3h38hb0.aspx
        /// </summary>
        private static void Nullables()
        {
            Console.WriteLine(Environment.NewLine + nameof(Nullables));
            var number = 10;
            int? number2 = null; // zde nefunguje implicitni typovani pomoci var
            // null coalescing operator priradi hodnotu 10
            var notNullableNumber = number2 ?? 5;
  
            // 5 bude defaultni hodnota  
            notNullableNumber = number2.GetValueOrDefault(5);

            number2 = notNullableNumber;
            // dalsi moznost zjisteni hodnoty (maji vsechny nullable typy, viz odkaz)
            if (number2.HasValue)
            {
                // nelze prevest int? na int
                // notNullableNumber = number2 
                // konverzi lze provest naprimo skrze property value (viz dale)
                notNullableNumber = number2.Value;
            }
        }

        /*
        Tasks:

        I. prevod Enum na string a zpet:
            je zadan nasledujici vyctovy typ: public enum Color {Red, Green, Blue} 
            dale jsou zadany nasledujici hodnoty:
            var colorString = "Green";
            var color = Color.Green;
            ukolem je prevest nejprve promenou colorString na Enum a nasledne color na String
            Tip: lze vyuzit funkcionality staticke tridy Enum

        II. prevod int (Int32) na Hexadecimalni hodnotu a zpet
            jsou zadany nasledujici hodnoty:
            int intValue = 1234;
            string hexValue = "4D2"; // hexadecimalni hodnota cisla 1234
            ukolem je prevest celociselnou hodnotu na hexadecimalni string a naopak,
            po prevodu provedte porovnani zda prevedena hodnota odpovida ocekavanemu vysledku
            a vysledek vypiste na konzoli
            Tip: pro prevod z hexadecimalni hodnoty na celociselnou lze pouzit vhodnou variantu metody tridy Convert,
            pro opacny prevod by se mohlo hodit: https://msdn.microsoft.com/en-us/library/dwhawy9k(v=vs.110).aspx
    */

        #region ReferenceVsValueTypesPassing

        /// <summary>
        /// Rozdily mezi predavanim odkazem a hodnotou, pouziti klicovych slov ref a out
        /// </summary>
        private static void ReferenceVsValueTypesPassing()
        {
            Console.WriteLine(Environment.NewLine + nameof(ReferenceVsValueTypesPassing));
            // predani parametru referenci
            int[] array = { 1, 2, 3, 4, 5 };
            Console.WriteLine($"hodnota array[0] po inicializaci: {array[0]}");
            ModifyArray(array);         //predani pole referenci
            Console.WriteLine($"hodnota array[0] po zavolani ModifyArray(...): {array[0]}");

            // predani parametru hodnotou
            string test = "Test";
            Console.WriteLine($"hodnota po inicializaci: {test}");
            ModifyString(test);      
            Console.WriteLine($"hodnota po zavolani ModifyString(...): {test}");

            // pouziti klicoveho slova ref  
            RefDemo();

            // pouziti klicoveho slova out
            OutDemo();
            
            Console.ReadKey();
        }

        /// <summary>
        /// Je predavana reference, zmena hodnoty prvku v poli se tedy propaguje (= je trvala)
        /// </summary>
        /// <param name="array"></param>
        static void ModifyArray(int[] array)
        {
            array = new int[]{ 5,6};
            //array[0] = 42;
        }

        /// <summary>
        /// String se chova stejne jako hodnotove typy, je predana hodnota, zmeny se tedy nepropaguji
        /// </summary>
        /// <param name="test"></param>
        static void ModifyString(string test)
        {
            test = "Moo";
        }


        #region Ref

        private static void RefDemo()
        {
            Console.WriteLine(Environment.NewLine + "'ref' demo:");
            var array = new int[5] {1, 2, 3, 4, 5};
            Console.WriteLine($"hodnota array[0] po inicializaci: {array[0]}");
            ModifyArrayPassedWithRef(ref array); //predani pole referenci
            Console.WriteLine($"hodnota array[0] po zavolani ModifyArrayPassedWithRef(...): {array[0]}");

            // Nove lze v C# 7.0 vytvorit "ref" lokalni promennou
            int x = 3;
            Console.WriteLine($"hodnota promenne x: {x}");
            ref int y = ref x;
            y = 2;
            Console.WriteLine($"hodnota promenne x po zmene hodnoty y: {x}");

            // Nove lze jako ref oznacit take navratovy typ 
            Console.WriteLine($"hodnota array[0] pred zavolanim GetValueByRef: {array[0]}");
            ref int secondArrayValue = ref GetValueByRef(array, 0);
            secondArrayValue = 8;
            Console.WriteLine($"hodnota array[0] po zavolani GetValueByRef a zmene navracene hodnoty(...): {array[0]}");
        }

        /// <summary>
        /// Rozdil mezi standardnim predanim a predanim pomoci parametru ref 
        /// (prirazeni uplne jine instance do odkazu vs modifikace dat instance odkazu)
        /// </summary>
        static void ModifyArrayPassedWithRef(ref int[] array)
        {
            array = new[] { 100, 101 };
        }

        static ref int GetValueByRef(int[] array, int index) {
            return ref array[index];
        }

        #endregion

        #region Out

        private static void OutDemo()
        {
            Console.WriteLine(Environment.NewLine + "'out' demo:");
            int[] array;
            // Nove lze v C# 7.0 inlinovat deklaraci promenne predavane pomoci 'out'
            ModifyArrayPassedWithOut(out array);
            Console.WriteLine("hodnota po zavolani ModifyArrayPassedWithOut(...): " + array[0]);
            // Pri inline deklaraci lze take pouzit implicitni typovani
            ModifyArrayPassedWithOut(out var array2);


            Console.ReadKey();
        }

        // podobne jako ref, nemusi se predem inicializovat, ref ovsem inicializaci vyzaduje
        static void ModifyArrayPassedWithOut(out int[] array)
        {
            // zde musime do array priradit nejakou hodnotu, jinak zpusobi compile-time error
            array = new [] { 100, 101 };
        }

        #endregion

        #endregion
    }

    class Base
    {
        public override string ToString()
        {
            return "Base";
        }
    }
    class Derived : Base
    { }


}
