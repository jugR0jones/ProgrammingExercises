// using System;using System.Linq;class P{static void Main(){for(int i=0;i<51;i++)if(Convert.ToString(i,2).Sum(f=>f-'0')%2<1)Console.WriteLine(i);}}
//using a=System;using a.Linq;class P{static void Main(){for(int i=0;i<51;i++)if(a.Convert.ToString(i,2).Count(f=>f>'0')%2<1)a.Console.WriteLine(i);}}
//using System;using System.Linq;class A{static void Main(){for(int i=0;i<51;i++)if(Convert.ToString(i,2).Sum(x=>x-'0')%2<1)Console.WriteLine(i);}}

using System;class P{static void Main(){for(int i=0;i<51;i++){int c=0,j=i;while(j>0){j&=(j-1);c++;}if(c%2<1)Console.WriteLine(i);}}}
