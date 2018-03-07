using System;
using System.IO;
using Microsoft.Extensions.CommandLineUtils;

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
            var app = new CommandLineApplication(throwOnUnexpectedArg: true);

            app.Name = nameof(AllClean);
            app.Description = "Clean 'bin' and 'obj' directory";
            app.HelpOption("-h|--help");

            var targetDirectoryArgument = app.Argument("PROJ_DIR", "project root directory that contains {bin, obj} directory. default: .");
            var interactiveOption = app.Option("-i", "Interactive mode", CommandOptionType.NoValue);

            app.OnExecute(() =>
            {
                var targetDir = targetDirectoryArgument.Value ?? ".";
                var interactive = interactiveOption.HasValue();

                foreach (var p in Target)
                {
                    DeleteDir(targetDir, p, interactive);
                }

                return 0;
            });

            app.Execute(args);
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
