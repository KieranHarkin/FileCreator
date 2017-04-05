using System;
using System.Diagnostics;

namespace FileCreator
{
    class Program
    {
        static void Main(string[] args)
        {
            const string defaultDirectory = @"cd:\Generated Files";
            const int bytesInMegabytes = 1048576;

            Console.WriteLine("How many files would you like to create?");
            var totalFilesToCreate = int.Parse(Console.ReadLine());

            Console.WriteLine("What size (MBs) do you want the files to be?");
            var size = double.Parse(Console.ReadLine()) * bytesInMegabytes;

            Console.WriteLine("What File Name would you like these files to be called?");
            var fileName = Console.ReadLine();

            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = false;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();

            cmd.StandardInput.WriteLine(defaultDirectory);

            for (var i = 0; i <= totalFilesToCreate; i++)
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
