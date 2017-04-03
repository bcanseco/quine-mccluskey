using System;
using System.Threading.Tasks;
using static System.Console;

namespace Algorithm.CLI
{
    /// <summary>
    ///   A command-line interface for the Q-M algorithm library.
    /// </summary>
    public class Program
    {
        /// <summary>
        ///   Entry point of the console app.
        /// </summary>
        private static void Main()
            => new Program().Start().GetAwaiter().GetResult();

        /// <summary>
        ///   Runs the Quine-McCluskey algorithm on user input in a loop.
        /// </summary>
        private async Task Start()
        {
            Title = "Press CTRL + C to quit.";
            
            while (true)
            {
                ForegroundColor = ConsoleColor.Gray;
                WriteLine("Enter a boolean expression (use * for AND, + for OR, ! for NOT):");
                try
                {
                    ForegroundColor = ConsoleColor.DarkCyan;
                    var userInput = ReadLine();
                    ForegroundColor = ConsoleColor.DarkGreen;

                    var expression = new Expression(userInput);

                    WriteLine($"Simplified expression: {await expression.Simplify()}");
                }
                catch (FormatException ex)
                {
                    ForegroundColor = ConsoleColor.DarkRed;
                    WriteLine($"An error occurred while constructing minterms: {ex.Message}");
                }
            }
        } 
    }
}
