using System;
using System.CommandLine;
using System.Threading.Tasks;

namespace LinearRegressionBackend.OOPExercise
{
    class Program
    {

        static async Task<int> Main(string[] args)
        {
            var rootCommand =
                new RootCommand("Machine Learning CLI Tool");

            var nameOption = new Option<string>(
                name: "--name",
                description: "The name of the user.")
                { 
                    IsRequired = true,
                };

            rootCommand.AddOption(nameOption);

            rootCommand.SetHandler(
                (name) => PrintUserName(name),
                nameOption);

            return await rootCommand.InvokeAsync(args);
        }

        static void PrintUserName(string userName)
        {
            Console.WriteLine($"Hello, {userName}!");
        }

    }
}
