using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DocumentsManagementSystem.Utilities
{
    public static class Descriptor
    {
        //This function recieves from the user a "non-clean" string, returns as an out parameter an array with the words, ready for sorting.
        public static bool CleanContent(string content, out string[] separatedWords)
        {
            separatedWords = null;
            if (content == null)
            {
                return false;
            }

            separatedWords =
                Regex.Matches(content, @"([a-zA-Z])+")
                .Cast<Match>()
                .Select(m => m.Value)
                .ToArray();

            if (separatedWords.Length < 5 || separatedWords.Length > 10)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool SortCleanStrings(string stringToClean, out string cleanPermutation, out string[] separatedWordsToSort) 
        {
            bool isOk = CleanContent(stringToClean, out separatedWordsToSort);
            cleanPermutation = null;

            if (!isOk)
            {
                return false;
            }

            StringBuilder SB = new StringBuilder("");
            for (int i = 0; i < separatedWordsToSort.Length; i++)
            {
                SB.Append(separatedWordsToSort[i]);
                if (!(i == separatedWordsToSort.Length - 1))
                {
                    SB.Append((" "));
                }
            }
            cleanPermutation = SB.ToString();
            Array.Sort(separatedWordsToSort);

            return true;
        }

        // Receives the sorted array of words and "glue" all the words to one string. return that string. 
        public static string GetKeyFromArray(string[] sortedArray)
        {
            StringBuilder SB = new StringBuilder();

            for (int i = 0; i < sortedArray.Length; i++)
            {
                SB.Append(sortedArray[i] + " ");
            }
            SB.Remove(SB.Length - 1, 1);

            return SB.ToString().ToUpper(); // returns key
        }


        public static bool GetKey(string stringToClean, out string cleanPermutation, out string key)
        {
            string[] separatedWordsToSort;
            key = null;
            bool isOk = SortCleanStrings(stringToClean, out cleanPermutation, out separatedWordsToSort);

            if (!isOk)
            {
                return false;
            }

            key = GetKeyFromArray(separatedWordsToSort);

            return true;
        }
    }
}
