using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Algorithm.Extensions;
using Algorithm.Minterms;
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
                WriteLine("Enter n (max number of bits for each sequence element):");
                ForegroundColor = ConsoleColor.DarkCyan;
                try
                {
                    var upperBoundInput = int.Parse(ReadLine());
                    if (upperBoundInput < 1)
                        throw new InvalidDataException("Upper bound must be at least 1.");

                    // n is the inclusive upper bound WRT value for the sequence of minterms
                    var n = Convert.ToString((int)Math.Pow(2, upperBoundInput) - 1, 2);

                    ForegroundColor = ConsoleColor.Gray;
                    WriteLine("Enter line-seperated base 10 integers smaller than 2^n.");
                    ForegroundColor = ConsoleColor.DarkCyan;

                    var minterms = new List<MintermBase>();

                    string input;
                    while (!string.IsNullOrWhiteSpace(input = ReadLine()))
                    {
                        // Convert input to binary
                        var binaryInput = Convert.ToString(int.Parse(input), 2)
                            .PadLeft(n.Length, '0'); // same length as n

                        if (binaryInput.Length > n.Length)
                            throw new InvalidDataException("Numbers must be less than 2^n");

                        // Convert each bit to its alphabetical form
                        var variables = binaryInput
                            .Select((c, i) => new Variable(i.AsAlphabetLetter(), c))
                            .ToList();

                        // Construct a minterm object with the parsed data
                        minterms.Add(new Minterm(variables, binaryInput));
                    }

                    if (!minterms.Any())
                        throw new InvalidDataException("Sequence cannot be empty.");

                    var expression = new Expression(minterms);

                    ForegroundColor = ConsoleColor.DarkGreen;
                    WriteLine($"Simplified expression: {await expression.Simplify()}\n\n");
                }
                catch (Exception ex)
                {
                    ForegroundColor = ConsoleColor.DarkRed;
                    WriteLine($"An error occurred: {ex.Message}");
                }
            }
        }
    }
}
