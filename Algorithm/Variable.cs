namespace Algorithm
{
    /// <summary>
    ///   A model for the individual letters of a minterm.
    /// </summary>
    public class Variable
    {
        public char Letter { get; }
        public bool NotFlagSet { get; }

        public Variable(char letter, bool notFlagSet)
        {
            Letter = letter;
            NotFlagSet = notFlagSet;
        }

        public Variable(char letter, char bit)
            : this(letter, bit != '1')
        { }

        public override string ToString()
            => (NotFlagSet ? "!" : "") + Letter;
    }
}
