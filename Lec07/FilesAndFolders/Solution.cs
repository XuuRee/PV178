using System;
using System.IO;
using System.Linq;
using FilesAndFolders.SampleData;

namespace FilesAndFolders
{
    public static class Solution
    {
        public static void Tasks()
        {
            FileEqualityComparer();

            FileSystemWatcher();
        }

        /* TODO 
           Pomocí třídy FileSystemWatcher sledujte složku SampleData v projektu FilesAndFolders.
           V okamžiku když dojde ve složce k vytvoření nového souboru s příponou .txt, 
           vypište do konzole vhodné hlášení (ostatní typy souborů ignorujte).
           V rámci testování v dané složce nejprve vytvořte soubor "text5.txt", dále pak "data.xml" 
           (nemělo by dojít k vyvolání obsluhy události Created třídy FileSystemWatcher)*/


        private static void FileSystemWatcher()
        {
            var fileWatcher = new FileSystemWatcher
            {
                Path = Paths.SampleDataFolder,
                NotifyFilter = NotifyFilters.FileName,
                Filter = "*.txt"
            };
            fileWatcher.Created += FileWatcher_Created;
            fileWatcher.EnableRaisingEvents = true;

            var text5FilePath = Path.Combine(Paths.SampleDataFolder, "text5.txt");
            File.Create(text5FilePath).Dispose();
            var xmlFilePath = Path.Combine(Paths.SampleDataFolder, "data.xml");
            File.Create(xmlFilePath).Dispose();
            File.Delete(text5FilePath);
            File.Delete(xmlFilePath);

            fileWatcher.Created -= FileWatcher_Created;
            fileWatcher.Dispose();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }


        /* TODO 
          Vytvořte metodu, která bude brát jako parametry cesty ke dvěma souborům, 
          které pak porovná (zjistí, zda jsou soubory identické). 
          V případě že jsou totožné, vrátí true, jinak false.
          Testování proveďte nejdříve na souborech text1.txt a text2.txt (false)
          následně pak na text1.txt a text3.txt (metoda by měla vrátit true)*/

        private static void FileEqualityComparer()
        {
          
            var areDifferentFilesEqual = FileEquals(Paths.Text1, Paths.Text2);
            var areSameFilesEqual = FileEquals(Paths.Text1, Paths.Text3);
        }


        private static bool FileEquals(string path1, string path2)
        {
            var file1 = File.ReadAllBytes(path1);
            var file2 = File.ReadAllBytes(path2);
            return file1.Length == file2.Length && !file1.Where((t, i) => t != file2[i]).Any();       
        }

        private static void FileWatcher_Created(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine($"Creation of new txt file: {e.Name} detected.");
        }
    }
}
