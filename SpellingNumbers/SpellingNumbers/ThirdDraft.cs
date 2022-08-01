namespace SpellingNumbers
{
    internal class ThirdDraft
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

            if (hundreds == 10)
            {
                return "one thousand";
            }

            if (hundreds > 0 && hundreds < 10)
            {
                output = numbersInEnglish[hundreds] + " hundred and ";
            }

            int tens = number / 10;
            if (tens <= 1)
            {
                return output + numbersInEnglish[number];
            }

            number -= 10 * tens;
            output += numbersInEnglish[20 + tens] + " " + numbersInEnglish[number];

            return output;
        }
    }
}