using System;
using System.Collections.Generic;

namespace Extension
{
    /*
    Extension metody nám dovolují "rozšířit" již definovanou třídu (typicky knihovní), bez nutnosti jejího překompilování.
    Je důležité si uvědomit že rozšíření probíhá pomocí statických metod, ale výsledná metoda statická není.
    Také musí být public.
    */

    public static class ExtensionMethods
    {
        public static string FirstLetterUpper(this string input)
        {
            if (!string.IsNullOrWhiteSpace(input))
            {
                var charArray = input.ToCharArray();
                charArray[0] = char.ToUpper(charArray[0]);
                input = new string(charArray);
            }
            return input;
        }

        public static bool IsDividibleBy(this int dividend, int divisor)
        {
            if (divisor == 0)
                throw new DivideByZeroException();

            return (dividend % divisor) == 0;
        }


        /// <summary>
        /// Rozsirujuca funkcia pre slovniky
        /// </summary>
        /// <typeparam name="TKey">Typ kluca</typeparam>
        /// <typeparam name="TValue">Typ hodnoty</typeparam>
        /// <param name="dictionary">Slovnik ktory moze obsahovat zadany kluc</param>
        /// <param name="key">Kluc v slovniku ktoreho hodnotu chceme ziskat.</param>
        /// <returns>Hodnotu ktora v slovniku zodpoveda danemu klucu alebo defaultnu hodnotu ak sa kluc v slovniku nenachadza</returns>
        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            TValue value;
            if (!dictionary.TryGetValue(key, out value))
            {
                // operator default vrati defaultni hodnotu pro dany typ
                return default(TValue);
            }
            return value;
        }
    }
}
