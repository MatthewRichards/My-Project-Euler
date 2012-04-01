using System;
using System.Collections.Generic;
using System.Linq;

namespace Euler.Utilities
{
  public class Sequences
  {
    private static readonly SequenceStore PrimesSoFar = new SequenceStore("Primes", new long[]{ 2, 3 });

    public static IEnumerable<long> Range(long start, long end)
    {
      return Range(start, end, 1);
    }

    public static IEnumerable<long> Range(long start, long end, int step)
    {
      int direction = start > end ? -step : step;

      for (long number = start; number != end; number += direction)
      {
        yield return number;
      }
    }

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

    public static IEnumerable<long> Primes()
    {
      long currentPrime = 3;

      foreach (long prime in PrimesSoFar)
      {
        currentPrime = prime;
        yield return prime;
      }

      while (currentPrime > 0)
      {
        double sqrt;
        do
        {
          currentPrime += 2;
          sqrt = Math.Sqrt(currentPrime);
        } while (PrimesSoFar.TakeWhile(prime => prime <= sqrt).Any(prime => currentPrime % prime == 0));

        PrimesSoFar.Add(currentPrime);
        yield return currentPrime;
      }
    }
  }
}