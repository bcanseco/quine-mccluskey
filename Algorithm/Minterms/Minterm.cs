using System;
using System.Linq;
using System.Text;

namespace Algorithm.Minterms
{
    public class Minterm : MintermBase
    {
        /// <summary>
        ///   Constructs a minterm given a boolean expression
        ///   containing only variables and NOT operators (if any).
        /// </summary>
        /// <param name="subExpression">The expression to use.</param>
        /// <exception cref="FormatException">
        ///   Thrown when an error occurs parsing <paramref name="subExpression"/>.
        /// </exception>
        public Minterm(string subExpression)
        {
            subExpression = subExpression
                .Replace(" ", string.Empty)
                .Replace("*", string.Empty);

            var bits = new StringBuilder();
            var notFlagSet = false;
            foreach (var c in subExpression)
            {
                if (c == '!')
                {
                    notFlagSet = !notFlagSet; // Allow arbitrary subsequent NOTs
                }
                else if (char.IsLetter(c))
                {
                    if (Variables.Any(v => v.Letter == c))
                        throw new FormatException($"Duplicate letter {c} in minterm.");

                    Variables.Add(new Variable(c, notFlagSet));
                    bits.Append(notFlagSet ? "0" : "1");
                    notFlagSet = false;
                }
                else
                {
                    throw new FormatException($"Unrecognized symbol {c} in minterm.");
                }
            }
            
            InBinary = bits.ToString();
        }

        public override string ToString()
            => $"m{Convert.ToInt32(InBinary, 2)}";
    }
}
