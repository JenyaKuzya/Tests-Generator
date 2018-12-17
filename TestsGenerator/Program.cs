using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TestsGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            string sourceDirectory;
            string targetDirectory;
            int countOfReadThreads;
            int countOfWriteThreads;
            int countOfProcessThreads;
            List<string> inputFiles = new List<string>();

            //Entering source folder path
            Console.WriteLine("Enter the source folder path:");
            sourceDirectory = Console.ReadLine();

            //Entering destination folder path
            Console.WriteLine("Enter the target folder path:");
            targetDirectory = Console.ReadLine();

            //Entering conveyor parameteres  
            Console.WriteLine("Enter the count of read threads:");
            countOfReadThreads = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter the count of process threads:");
            countOfProcessThreads = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter the count of write threads:");
            countOfWriteThreads = Convert.ToInt32(Console.ReadLine());

            try
            {
                string[] fileEntries = Directory.GetFiles(targetDirectory);
                foreach (string fileName in fileEntries)
                {
                    inputFiles.Add(fileName);
                }
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine("Directory not found");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }


            var config = new Config(countOfReadThreads, countOfProcessThreads, countOfWriteThreads);

            var generator = new TestsGenerator(config);
            generator.Generate(sourceFiles, outputPath).Wait();

            Console.WriteLine("Generation ended");
            Console.ReadKey();
        }
    }
}
