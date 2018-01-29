using System;
using System.Diagnostics;

namespace FileCreator
{
    class Program
    {
        static void Main(string[] args)
        {
            const int bytesInMegabytes = 1048576;

            Console.WriteLine("How many files would you like to create?");
            var inputFileCount = Console.ReadLine();
            var totalFilesToCreate = int.Parse(string.IsNullOrWhiteSpace(inputFileCount) ?  "1" : inputFileCount);

            Console.WriteLine("What size (MBs) do you want the files to be?");
            var inputFileSize = Console.ReadLine();

            var size = double.Parse(string.IsNullOrWhiteSpace(inputFileSize) ? "1" : inputFileSize) * bytesInMegabytes;

            Console.WriteLine("What File Name would you like these files to be called?");
            var fileName = Console.ReadLine();

            var cmd = new Process
            {
                StartInfo =
                {
                    FileName = "cmd.exe",
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    CreateNoWindow = false,
                    UseShellExecute = false
                }
            };
            cmd.Start();

            for (var i = 1; i <= totalFilesToCreate; i++)
            {
                cmd.StandardInput.WriteLine($"fsutil file createnew {fileName + i}.txt {size}");
                cmd.StandardInput.Flush();
            }

            cmd.StandardInput.Close();
            cmd.WaitForExit();

            Console.WriteLine(cmd.StandardOutput.ReadToEnd());
        }
    }
}
