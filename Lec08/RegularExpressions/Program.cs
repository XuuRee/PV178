using System.Text.RegularExpressions;

namespace RegularExpressions
{
    class Program
    {
        /// <summary>
        /// Vasim ukolem je implementace nize uvedenych metod
        /// </summary>
        static void Main(string[] args)
        {
            GetAllWordsContainingLo();
            GetAllSums();
            GetAllTagValues();
            GetAllEvenNumbers();
        }

        /// <summary>
        /// Napište regulární výraz, který z řetězce dataToParse01 získá všechny slova 
        /// obsahující řetězec "lo" (výraz vytvořte jako case-insensitive)    
        /// </summary>
        private static void GetAllWordsContainingLo()
        {
            const string dataToParse01 = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                                         "Ut lobortis dui in erat commodo, eu ornare orci semper. ";

            var wordsWithLoPattern = new Regex(@"(\s?\w*lo\w*\s?)", RegexOptions.IgnoreCase);
            var wordsContainingLo = wordsWithLoPattern.Matches(dataToParse01);
        }

        /// <summary>
        /// Napište regulární výraz, který z řetězce dataToParse02 získá všechny částky v $ 
        /// (formát částky zachovejte $X,XXX,XXX...) 
        /// Tip: jak již bylo uvedeno, znak $ je rezervovaný,
        /// proto je třeba před něj uvést zpětné lomítko: '\$'
        /// </summary>
        private static void GetAllSums()
        {
            const string dataToParse02 = "Throughout March 1973, the band negotiated a new contract " +
                                         "with Columbia Records, who gave them a reported advance of " +
                                         "$1,000,000 which is worth approximately $5,000,000 today since $1 ...";

            var extractSumsPattern = new Regex(@"(\$\d{1,3}[,\d{3}]*)");
            var wordsContainingLo = extractSumsPattern.Matches(dataToParse02);
        }

        /// <summary>
        /// Napište regulární výraz, který z řetězce dataToParse03 získá hodnotu tagů h1 a p, 
        /// tedy řetězce "The headline text" a "..."
        /// </summary>
        private static void GetAllTagValues()
        {
            const string dataToParse03 = "<h1>The headline text</h1> <p>...</p>";
            var tagContentPattern = new Regex(@"");
        }

        /// <summary>
        /// Napište regulární výraz, který z řetězce dataToParse04 získá pouze sudá čísla
        /// </summary>
        private static void GetAllEvenNumbers()
        {
            const string dataToParse04 = "178 255 12879 1 5429786 0 75893 43 88 116";
            
            // TODO...

            // var evenNumbersPattern = ...
        }
    }
}
