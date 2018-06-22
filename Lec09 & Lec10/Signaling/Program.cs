namespace Signaling
{
    class Program
    {
        static void Main(string[] args)
        {
            new AutoResetEventDemo().RunAutoResetEventDemo();
            new ManualResetEventDemo().RunManualResetEventDemo();

            // Samostatna prace - signalizace pomoci CountdownEvent:
            // 
            // V predchystanem zadani probiha v separatnim vlakne vypocet 
            // faktorialu pro tri dane intervaly. Z kazdeho z techto tri intervalu
            // nas zajima pouze hodnota faktorialu pro jedno cislo, konkretne
            // chceme vedet vypoctenou hodnotu pro cislo 7 z prvniho intervalu,
            // cislo 18 ze druheho a cislo 21 ze tretiho.
            // 
            // Z techto 3 vypoctenych hodnot pak chceme co nejdrive vypsat uzivateli 
            // na vystup jejich soucet (nechceme tedy cekat az dobehne vypocet faktorialu 
            // pro vsechna cisla z daneho rozsahu). Bude tedy potreba signalizovat, ze
            // mame k dispozici vypoctenou hodnotu pro kazde z dilcich cisel.
            // Vasim ukolem je tedy pouzit CountdownEvent, tak aby se uzivateli mohl 
            // zobrazit pozadovany soucet ve chvili, kdy jsou vsechny vysledky k dispozici.
            // 
            // Tipy: 
            // 
            // -> Vhodne pouzijte metodu Wait() tridy CountDownEvent, ktera ceka, dokud
            //    nebude zavolan pozadovany pocet .Signal() metod teto tridy (tento pocet
            //    bere konstruktor tridy CountDownEvent jako parametr)
            // 
            // -> vyuzijte parametru action metody ComputeFactorialFromRange k pripadne 
            //    signalizaci (pokud jiz v danem intervalu doslo k vypoctu pozadovane hodnoty)

            //new Solution().TestSolution();
        }
    }
}
