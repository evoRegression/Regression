using System;
using System.Collections.Generic;
using System.Text;

namespace RandomShuffle
{
    internal static class ListExtension
    {
        private static readonly Random myRandom = new Random();

        internal static string PopRandom(this List<string> listOfString)
        {
            int randomIndex = myRandom.Next(listOfString.Count);
            string randomString = listOfString[randomIndex];

            listOfString.RemoveAt(randomIndex);

            return randomString;
        }
    }
}
