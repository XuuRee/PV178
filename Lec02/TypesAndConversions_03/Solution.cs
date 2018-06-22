using System;

namespace TypesAndConversions_02
{
    public static class Solution
    {
        public enum Color { Red, Green, Blue }

        /// <summary>
        /// Prevod Enum na string a zpet:
        /// je zadan nasledujici vyctovy typ: public enum Color {Red, Green, Blue} 
        /// dale jsou zadany nasledujici hodnoty:
        /// var colorString = "Green";
        /// var color = Color.Green;
        /// ukolem je prevest nejprve promenou colorString na Enum a nasledne color na String
        /// Tip: lze vyuzit funkcionality staticke tridy Enum
        /// </summary>
        public static void Task01()
        {
            var color = Color.Green;

            var colorString = color.ToString();

            color = (Color)Enum.Parse(typeof(Color), colorString);          // case sensitive variant
            color = (Color)Enum.Parse(typeof(Color), colorString, true);    // case insensitive variant
        }

        /// <summary>
        /// prevod int (Int32) na Hexadecimalni hodnotu a zpet
        /// jsou zadany nasledujici hodnoty:
        /// int intValue = 1234;
        /// string hexValue = "4D2"; // hexadecimalni hodnota cisla 1234
        /// ukolem je prevest celociselnou hodnotu na hexadecimalni string a naopak,
        /// po prevodu provedte porovnani zda prevedena hodnota odpovida ocekavanemu vysledku
        /// a vysledek vypiste na konzoli
        /// Tip: pro prevod z hexadecimalni hodnoty na celociselnou lze pouzit vhodnou variantu metody tridy Convert,
        /// pro opacny prevod by se mohlo hodit: https://msdn.microsoft.com/en-us/library/dwhawy9k(v=vs.110).aspx
        /// </summary>
        public static void Task02()
        {
            var intValue = 1234;
            var hexValue = "4D2";
            
            // from int to HEX
            var convertedHex = intValue.ToString("X");
            // var resultIntToHex = convertedHex.Equals(hexValue) ? "Successfully" : "Not successfully"; // could also use string compare 
            var resultIntToHex = convertedHex == hexValue ? "Successfully" : "Not successfully"; 
            // Even though string is reference type it returns true, why ? See this SO post: http://stackoverflow.com/questions/636932/in-c-why-is-string-a-reference-type-that-behaves-like-a-value-type
            Console.WriteLine($"{resultIntToHex} converted integer value {intValue} to hexadecimal: {hexValue}");

            // from HEX to int
            var convertedInt = Convert.ToInt32(hexValue, 16);  
            var resultHexToInt = convertedInt == intValue ? "Successfully" : "Not successfully";  
            // == operator will resolve in Object.ReferenceEquals (for reference types), for value types it compares its values
            Console.WriteLine($"{resultHexToInt} converted hexadecimal value {hexValue} to integer: {intValue}");

            Console.ReadKey();
        }
    }
}
