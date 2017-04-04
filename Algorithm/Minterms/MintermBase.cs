using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Algorithm.Extensions;

namespace Algorithm.Minterms
{
    /// <summary>
    ///   The base class containing aspects shared by both 
    ///   <see cref="Minterm"/> and <see cref="CompositeMinterm"/> instances.
    /// </summary>
    [DebuggerDisplay("{ToString(), nq} {InBinary, nq} [{Raw(), nq}]")]
    [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
    public abstract class MintermBase : IEquatable<MintermBase>
    {
        protected List<Variable> Variables { get; set; } = new List<Variable>();
        public string InBinary { get; protected set; }
        public bool IsPrime { get; set; } = true;

        public int NumberOfOnes
            => InBinary.Count(c => c == '1');

        public string Raw()
            => string.Join(" * ", Variables);

        public override int GetHashCode()
            => Convert.ToInt32(InBinary.Replace('-', '2'));

        public override bool Equals(object obj)
            => Equals(obj as MintermBase);

        public bool Equals(MintermBase other)
            => InBinary == other.InBinary;

        public bool MatchesDomain(MintermBase other)
            => InBinary.AllIndicesOf("-")
                .SequenceEqual(other.InBinary.AllIndicesOf("-"));

        /// <summary>
        ///   Checks if two minterms differ by one and
        ///   only one bit and combines them if so.
        /// </summary>
        /// <param name="other">The minterm to attempt combination with.</param>
        /// <param name="combination">The combined minterm.</param>
        /// <returns>
        ///   An initialized <paramref name="combination"/> if the
        ///   minterms can be combined, <see langword="null"/> otherwise.
        /// </returns>
        public Task<bool> TryCombineWith(MintermBase other, out MintermBase combination)
        {
            combination = null; // Must set by default if prematurely returning false

            if (InBinary.Length != other.InBinary.Length)
                return Task.FromResult(false); // Unequal length, cannot be compared

            var differentIndex = -1;
            for (var i = 0; i < InBinary.Length; i++)
            {
                if (InBinary[i] != other.InBinary[i])
                {
                    if (differentIndex != -1)
                        return Task.FromResult(false); // More than one match
                    differentIndex = i;
                }
            }

            if (differentIndex == -1)
                return Task.FromResult(false); // No matches

            if (Variables.Count != 1)
            {
                var newBinary = InBinary.ReplaceAt(differentIndex, '-');
                var newVariables = Variables
                    .Where((value, index) => index != differentIndex)
                    .ToList();

                combination = new CompositeMinterm(newVariables, newBinary, this, other);
            }
            else
            {
                // Minterms only have one matching variable (e.g. A or !A).
                // In this case, the combination will be the true one.
                var finalMinterm = Variables[0].NotFlagSet ? other : this;
                var droppedMinterm = Variables[0].NotFlagSet ? this : other;

                combination = new CompositeMinterm(finalMinterm.Variables,
                    finalMinterm.InBinary, finalMinterm, droppedMinterm);
            }
            
            return Task.FromResult(true);
        }

        public abstract override string ToString();
    }
}
