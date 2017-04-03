using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Algorithm.Minterms;

namespace Algorithm
{
    public class Expression
    {
        private List<MintermBase> Minterms { get; set; }
            = new List<MintermBase>();

        /// <summary>
        ///   Parses a string into <see cref="Minterm"/> fragments.
        /// </summary>
        /// <param name="input">A string containing an expression.</param>
        /// <exception cref="FormatException">
        ///   Thrown in the Minterm constructor if a subexpression is invalid; uncaught.
        /// </exception>
        public Expression(string input)
        {
            var rawMinterms = input.Split('+');

            foreach (var raw in rawMinterms)
                Minterms.Add(new Minterm(raw));
        }

        /// <summary>
        ///   Simplifies the <see cref="Minterms"/> contents
        ///   using the Q-M algorithm and returns the expression.
        /// </summary>
        public async Task<Expression> Simplify()
        {
            var currentBatch = Minterms;
            var simplification = new List<MintermBase>();
            List<MintermBase> foundMatches = null;

            do
            {
                if (foundMatches != null) // if not first iteration
                    currentBatch = foundMatches.Distinct().ToList();

                foundMatches = new List<MintermBase>();
                foreach (var minterm in currentBatch.OrderBy(m => m.NumberOfOnes))
                {
                    var potentialMatches = currentBatch
                        .Where(m => m.NumberOfOnes == minterm.NumberOfOnes + 1)
                        .Where(m => m.MatchesDomain(minterm));

                    foreach (var otherMinterm in potentialMatches)
                    {
                        if (await minterm.TryCombineWith(otherMinterm, out MintermBase combination))
                        {
                            foundMatches.Add(combination);
                            minterm.IsPrime = false;
                            otherMinterm.IsPrime = false;
                        }
                    }
                }
                // Minterms that couldn't be combined are included in final result
                simplification.AddRange(currentBatch.Where(m => m.IsPrime).Distinct());

            } while (foundMatches.Any());

            Minterms = simplification;
            return this;
        }

        public override string ToString()
            => Minterms
                .Select(m => m.Raw())
                .Aggregate((current, next) => current + " + " + next);
    }
}
