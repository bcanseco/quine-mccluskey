using System;
using System.Collections.Generic;

namespace Algorithm.Extensions
{
    /// <summary>
    ///   Holding class for <see cref="string"/> helper functions.
    /// </summary>
    public static class StringExtensions
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
    }
}
