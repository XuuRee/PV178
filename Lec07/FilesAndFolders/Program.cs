using System;
using System.IO;
using Paths = LinqToXml.SampleData.Paths;

namespace FilesAndFolders
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Reseni ukolu, zadani viz Lab07_Tasks
            // Solution.Tasks();

            DirectoriesIntro();

            PathsIntro();

            FilesIntro();
        }

        private static void DirectoriesIntro()
        {
            var executingDirectory = AppDomain.CurrentDomain.BaseDirectory;

            // List all file paths within executing directory
            var filePaths = Directory.GetFiles(executingDirectory);

            // List only file paths with specific extension
            var exeFilePaths = Directory.GetFiles(executingDirectory, "*.exe");

            // Move to the parent directory (twice) 
            // FilesAndFolders\Bin\Debug -> FilesAndFolders

            var projectRootDirectory = executingDirectory + @"..\..\";
            var parentFilePaths = Directory.GetFiles(projectRootDirectory, "*");

            // All file paths within current project directory
            var allFilePaths = Directory.GetFiles(projectRootDirectory, "*", SearchOption.AllDirectories);

            // Get all directories within root project folder
            var directoryPaths = Directory.GetDirectories(projectRootDirectory);

            var tmpDirectoryPath = projectRootDirectory + @"tmp\";

            Directory.CreateDirectory(tmpDirectoryPath);

            // Get all related info for tmpDirectory
            var tmpDirectoryInfo = new DirectoryInfo(tmpDirectoryPath);

            // Get projects parent directory info
            var solutionDirectoryInfo = tmpDirectoryInfo.Parent;

            Directory.Delete(tmpDirectoryPath);

            // Verify directory exists
            var tmpDirectoryExists = tmpDirectoryInfo.Exists;

            // Check for various directory attributes
            if (tmpDirectoryInfo.Attributes.HasFlag(FileAttributes.ReadOnly))
            {
                //Do stuff
            }
        }

        private static void PathsIntro()
        {
            var fileName = Path.GetFileName(Paths.Movies);
            var fileExtension = Path.GetExtension(Paths.Movies);

            // Get drive root path (i.e. C:\)
            var drivePath = Path.GetPathRoot(Paths.Movies);

            // Combine Paths
            var sampleDataFilePath = Path.Combine(drivePath + @"Users\Public");
        }

        private static void FilesIntro()
        {
            var newFilePath = Path.Combine(SampleData.Paths.SampleDataFolder, "text4.txt");
            // Creates new file (return value: FileStream is disposed since streams will be discussed later )
            File.Create(newFilePath).Dispose();
            
            File.AppendAllText(newFilePath, "Lets put some text into text4.txt.");
            var text4Content = File.ReadAllText(newFilePath);

            var backupFilePath = Path.Combine(SampleData.Paths.SampleDataFolder, "text4_backup.txt");

            // Replace new file by text1 and perform its backup to file: "text4_backup.txt"
            File.Replace(newFilePath, SampleData.Paths.Text2, backupFilePath);
            File.Delete(backupFilePath);

            // Verify "text4.txt" does no longer exists
            var text4Exists = File.Exists(newFilePath);
        }
    }
}
