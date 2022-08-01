namespace SpellingNumbers
{
    internal class SecondDraft
    {
        public static string Convert(int number)
        {
            string[] numbersInEnglish =
            {
                "",
                "one",
                "two",
                "three",
                "four",
                "five",
                "six",
                "seven",
                "eight",
                "nine",
                "ten",
                "eleven",
                "twelve",
                "thirteen",
                "fourteen",
                "fifteen",
                "sixteen",
                "seventeen",
                "eighteen",
                "nineteen",
            };

            string[] tensInEnglish =
            {
                "",
                "",
                "twenty",
                "thirty",
                "forty",
                "fifty",
                "sixty",
                "seventy",
                "eighty",
                "ninety",
            };

            int hundreds = number / 100;
            number -= 100 * hundreds;

            string output = "";

            if(hundreds > 0 && hundreds < 10)
            {
                output = numbersInEnglish[hundreds] + " hundred and ";
            }
            else if (hundreds == 10)
            {
                return "one thousand";
            }

            int tens = number / 10;
            if (tens > 1)
            {
                output += tensInEnglish[tens] + " ";
            }
            else if (tens == 1)
            {
                output += numbersInEnglish[number];

                return output;
            }

            number -= 10 * tens;
            output += numbersInEnglish[number];

            return output;
        }
    }
}