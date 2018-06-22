namespace ConcurrentCollections
{
    class Program
    {
        /// <summary>
        /// Concurrent collections are fully thread-safe and suitable for highly concurrent scenarios.
        /// There are 5 generic concurrent collections (the last two has no ordinary equivalent):
        /// ConcurrentStack, ConcurrentQueue, ConcurrentDictionary, ConcurrentBag and BlockingCollection.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            ConcurrentDictionaryDemo.RunConcurrentDictionaryDemo();

            ConcurrentBagDemo.RunConcurrentBagDemo();

            BlockingCollectionDemo.RunBlockingCollectionDemo();

            // Samostatna prace - konzumace vice blokujicich kolekci:
            //
            // I.   Vytvorte pole o dvou blokujicich kolekci, jedna z nich bude interne pouzivat 
            //      ConcurrentQueue, druha pak ConcurrentStack. 
            // 
            // II.  Vytvorte dva Tasky, prvni z nich bude pridavat do prvni blokujici kolekce prvky
            //      s casovym intervalem 250ms, druhy pak 500ms (kazdy z Tasku prida prave 10 prvku).
            // 
            // III. Zajistete aby po pridani vsech prvku v obou Tascich jiz nebyla ani jedna z 
            //      kolekci nadale blokovana (tedy aby pri volani TryTake neprobihalo cekani).
            // 
            // IV.  Pomoci staticke metody BlockingCollection<int>.TryTakeFromAny(...) odebirejte 
            //      prvky z obou kolekci zaroven (cekejte vsak pouze v pripade pokud jsou obe prazdne
            //      a maji se do nich jeste pridavat prvky). Odebrane prvky prubezne vypisujte na 
            //      konzoly (jako typ prvku muzete napriklad zvolit cela cisla).

            // Solution.TestSolution();
        }
    }
}
