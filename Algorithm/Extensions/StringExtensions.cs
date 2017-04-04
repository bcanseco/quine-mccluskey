using System;
using System.Collections.Generic;

namespace Algorithm.Extensions
{
    /// <summary>
    ///   Holding class for primitive helper functions.
    /// </summary>
    public static class SystemExtensions
    {
        /// <summary>
        ///   Gets all the indices of a substring inside a string.
        ///   <a href="http://stackoverflow.com/a/24016130/6609109">By fubo.</a>
        /// </summary>
        /// <param name="str">The string to find indices in.</param>
        /// <param name="searchStr">The substring to search for.</param>
        public static IEnumerable<int> AllIndicesOf(this string str, string searchStr)
        {
            var minIndex = str.IndexOf(searchStr, StringComparison.Ordinal);
            while (minIndex != -1)
            {
                yield return minIndex;
                minIndex = str
                    .IndexOf(searchStr, minIndex + searchStr.Length, StringComparison.Ordinal);
            }
        }

        /// <summary>
        ///   Makes a deep copy of a string and returns it with a replaced character.  
        ///   <a href="http://stackoverflow.com/a/9367156/6609109">By Jon Skeet.</a>
        /// </summary>
        /// <param name="input">The string to use (will not be changed).</param>
        /// <param name="index">The index to replace at.</param>
        /// <param name="newChar">The character to use during replacement.</param>
        /// <returns>
        ///   <paramref name="input"/> with <paramref name="newChar"/> at index <paramref name="index"/>.
        /// </returns>
        public static string ReplaceAt(this string input, int index, char newChar)
        {
            var chars = input.ToCharArray();
            chars[index] = newChar;
            return new string(chars);
        }

        /// <summary>
        ///   Converts a zero-indexed integer to its alphabetic form. 
        /// </summary>
        /// <example>0 => A, 1 => B, 2 => C, etc.</example>
        /// <param name="number">The integer to use.</param>
        /// <returns>A capital letter.</returns>
        public static char AsAlphabetLetter(this int number)
            => (char)(65 + number);
    }
}
