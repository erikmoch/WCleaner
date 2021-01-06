using System;
using System.IO;
using System.Threading.Tasks;

namespace WCleaner
{
    class Program
    {
        static void Main(string[] args) => new Program().Run();

        void Run()
        {
            ClearTemp();
            Console.ReadKey();
        }

        string[] Paths => new string[]
        {
            "C:\\Windows\\Temp",
            string.Format("C:\\Users\\{0}\\AppData\\Local\\Temp", Environment.UserName)
        };

        void ClearTemp()
        {
            foreach (string path in Paths)
            {
                DirectoryInfo directory = new DirectoryInfo(path);
                Task.Run(() => ClearFiles(directory.GetFiles()));
                Task.Run(() => ClearDirectories(directory.GetDirectories()));
            }
        }

        void ClearFiles(FileInfo[] files)
        {
            for (int i = 0; i < files.Length; i++)
                DeleteFile(files[i]);
            Console.WriteLine("All files deleted.");
        }
        void ClearDirectories(DirectoryInfo[] directories)
        {
            for (int i = 0; i < directories.Length; i++)
                DeleteDirectory(directories[i]);
            Console.WriteLine("All directories deleted.");
        }

        void DeleteFile(FileInfo file)
        {
            try
            {
                Console.WriteLine($"Deleting {file.Name} file.");
                file.Delete();
            }
            catch { }
        }
        void DeleteDirectory(DirectoryInfo directory)
        {
            try
            {
                Console.WriteLine($"Deleting {directory.Name} directory.");
                directory.Delete();
            }
            catch { }
        }
    }
}