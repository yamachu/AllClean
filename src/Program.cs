using System;
using System.IO;

namespace AllClean
{
    class Program
    {
        static string[] Target = {
            "bin",
            "obj",
        };

        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                System.Console.WriteLine(@"Usage: dotnet allclean <PROJ_DIR> [options]");
                System.Console.WriteLine();
                System.Console.WriteLine("Arguments:");
                System.Console.WriteLine("  <PROJ_DIR>: project root directory that contains {bin,obj} directory.");
                System.Console.WriteLine();
                System.Console.WriteLine("Options:");
                System.Console.WriteLine("  -i  Interactive mode");
                return;
            }

            var dir = args[0];
            var interactive = args.Length == 2;

            foreach (var p in Target)
            {
                DeleteDir(dir, p, interactive);
            }
        }

        static void DeleteDir(string dirPath, string subDir, bool interactive = false)
        {
            if (!Directory.Exists(Path.Combine(dirPath, subDir)))
            {
                return;
            }

            var rmPath = Path.GetFullPath(Path.Combine(dirPath, subDir));

            if (interactive)
            {
                System.Console.WriteLine($"{rmPath} will be deleted");
                System.Console.WriteLine("Continue? [Y/n]");
                var key = Console.ReadKey().KeyChar;

                if (key == 'n' || key == 'N')
                {
                    return;
                }
            }

            try
            {
                Directory.Delete(rmPath, true);
            }
            catch(Exception e)
            {
                Console.Error.Write(e.Message);
                return;
            }

            System.Console.WriteLine($"{rmPath} : Deleted");
        }
    }
}
