using System.Collections.Generic;
using System.Linq;

namespace Algorithm.Minterms
{
    /// <summary>
    ///   A model for a minterm composed of other minterms.
    /// </summary>
    public class CompositeMinterm : MintermBase
    {
        private List<MintermBase> Minterms { get; }

        /// <summary>
        ///   Constructs a composite minterm.
        /// </summary>
        /// <param name="variables">The logical variables at this step.</param>
        /// <param name="inBinary">The binary representation of the variables.</param>
        /// <param name="minterms">The minterms used during the combination.</param>
        public CompositeMinterm
        (
            List<Variable> variables, 
            string inBinary,
            params MintermBase[] minterms
        )
        {
            Variables = variables;
            InBinary = inBinary;
            Minterms = minterms.ToList();
        }

        public override string ToString()
            => $"({string.Join(", ", Minterms)})";
    }
}
