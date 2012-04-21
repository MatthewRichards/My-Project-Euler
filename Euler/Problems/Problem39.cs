using System.Linq;
using Euler.Utilities;

namespace Euler.Problems
{
  public class Problem39
  {
     public int GetAnswer()
     {
       int[] squares = Sequences.Range(0, 1000).Select(n => n*n).ToArray();
       int[] combos = new int[squares.Length];

       for (int a = 1; a <=333; a++)
       {
         int aSquared = squares[a];
         int c = a + a;
         int aSquaredPlusbSquared = aSquared + aSquared;
         int bLimit = 1000 - a;

         for (int b = a; b < bLimit; b++)
         {
           while (squares[c] < aSquaredPlusbSquared)
           {
             c++;
           }

           if (c > 1000 - a - b)
           {
             break;
           }

           if (squares[c] == aSquaredPlusbSquared)
           {
             combos[a + b + c]++;
           }

           aSquaredPlusbSquared += b + b + 1;
         }
       }

       return Sequences.Range(0, 1000).MaxBy(n => combos[n]);
     }
  }
}