using System.Collections.Generic;

namespace SpellingNumbers
{
    internal class FirstDraft
    {
        public static string Convert(int number)
        {
            Dictionary<int, string> hundredsInEnglish = new Dictionary<int, string>
            {
                { 0, "" },
                { 1, "one hundred" },
                { 2, "two hundred" },
                { 3, "three hundred" },
                { 4, "four hundred" },
                { 5, "five hundred" },
                { 6, "six hundred" },
                { 7, "seven hundred" },
                { 8, "eight hundred" },
                { 9, "nine hundred" },
                { 10, "one thousand" },
            };

            Dictionary<int, string> tensInEnglish = new Dictionary<int, string>
            {
                { 0, "" },
                { 1, "" },
                { 2, "twenty" },
                { 3, "thirty" },
                { 4, "forty" },
                { 5, "fifty" },
                { 6, "sixty" },
                { 7, "seventy" },
                { 8, "eighty" },
                { 9, "ninety" },
            };

            Dictionary<int, string> teensInEnglish = new Dictionary<int, string>
            {
                {10, "ten" },
                {11, "eleven" },
                {12, "twelve" },
                {13, "thirteen" },
                {14, "fourteen" },
                {15, "fifteen" },
                {16, "sixteen" },
                {17, "seventeen" },
                {18, "eighteen" },
                {19, "nineteen" },
            };

            Dictionary<int, string> unitsInEnglish = new Dictionary<int, string>
            {
                {0, "" },
                {1, "one" },
                {2, "two" },
                {3, "three" },
                {4, "four" },
                {5, "five" },
                {6, "six" },
                {7, "seven" },
                {8, "eight" },
                {9, "nine" },
            };

            int hundreds = number / 100;
            number = number - 100 * hundreds;

            string output = "";

            output += hundredsInEnglish[hundreds];
            if (hundreds > 0 && hundreds < 10)
            {
                output += " and ";
            }

            int tens = number / 10;
            if (tens > 1)
            {
                output += tensInEnglish[tens] + " ";
            }
            else if (tens == 1)
            {
                output += teensInEnglish[number];

                return output;
            }

            number = number - 10 * tens;
            output += unitsInEnglish[number];

            return output;
        }
    }
}
