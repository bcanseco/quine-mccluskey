namespace Algorithm
{
    /// <summary>
    ///   A model for the individual letters of a minterm.
    /// </summary>
    public class Variable
    {
        public char Letter { get; }
        private bool NotFlagSet { get; }

        public Variable(char letter, bool notFlagSet)
        {
            Letter = letter;
            NotFlagSet = notFlagSet;
        }

        public override string ToString()
            => (NotFlagSet ? "!" : "") + Letter;
    }
}
