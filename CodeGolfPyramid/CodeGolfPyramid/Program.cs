using static System.Console;
class P {
    static void Main() {
        void d(int n)
        {
            string s = " ";
            for (int k = 0; k < 9 - n; k++)
                s += " ";
            for (int k = 1; k <= n; k++)
                s += k;
            //for (int k = n - 1; k > 0; k--)
            //    s += k;
            WriteLine(s);
        }

        for (int i = 1; i < 10; i++)
        {
            for (int j = 1; j < i + 1; j++)
                d(j); 
            for (int j = i - 1; j > 0; j--) 
                d(j);
            WriteLine(); 
        }
    }
}