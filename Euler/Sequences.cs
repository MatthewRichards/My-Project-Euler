using System.Collections.Generic;

namespace Euler
{
  public class Sequences
  {
    public static IEnumerable<int> TriangleNumbers()
    {
      int sum = 0;

      for (int number = 1;; number++)
      {
        sum += number;
        yield return sum;

        if (sum > int.MaxValue - number)
        {
          yield break;
        }
      }
    }
  }
}