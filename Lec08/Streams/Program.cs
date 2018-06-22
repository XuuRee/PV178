using System.IO;
using System.IO.Compression;
using Streams.SampleData;

namespace Streams
{
    class Program
    {
        /// <summary>
        /// Pomocí třídy GZipStream zkomprimujte soubor text1.txt do 
        /// archivu text.gz, následně proveďte jeho dekomprimaci. 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            var file = new FileInfo(Paths.Text1);
            var compress = file.FullName.Remove(file.FullName.LastIndexOf('.')) + ".gz";

            using (var origFileStream = file.OpenRead())
            {
                using (var compressedFileStream = File.Create(compress))
                {
                    using (GZipStream compressionStream = new GZipStream(compressedFileStream, CompressionMode.Compress))
                    {
                        origFileStream.CopyTo(compressionStream);
                    }
                }
            }

        }
    }
}
