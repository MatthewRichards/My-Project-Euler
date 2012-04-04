using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Euler.Algorithms;
using Euler.Utilities;

namespace Euler.Problems
{
  public class Problem69To72
  {
    public long GetAnswer()
    {
      return GetAnswerTo71();
    }

    public long GetAnswerTo69()
    {
      const int limit = 1000000;
      var sieve = new CoprimeSieve(limit);
      return Sequences.Range(2, limit).AsParallel().MaxBy(n => (double)n / sieve.Phi(n));
    }

    public long GetAnswerTo70()
    {
      const int limit = 10000000;
      var sieve = new CoprimeSieve(limit);
      return Sequences.Range(2, limit).MinByWithPostCondition(
        n => (double)n / sieve.Phi(n),
        n => IsPermutation(n, sieve.Phi(n)));
    }

    public long GetAnswerTo71()
    {
      const int limit = 1000000;
      const double targetFraction = 3.0/7.0;
      double maxFraction = 0;
      int maxN = 0;

      foreach (int d in Sequences.Range(limit, 2))
      {
        var startN = (int)Math.Floor(d*targetFraction);
        var endN = (int)Math.Ceiling(d*maxFraction);
        int n = startN;
        bool found = false;

        while (n >= endN)
        {
          if (GcdRecursive(d, n) == 1)
          {
            found = true;
            break;
          }
          n--;
        }

        if (!found)
        {
          continue;
        }

        var fraction = (double)n/d;

        if (fraction > maxFraction && fraction < targetFraction)
        {
          maxFraction = fraction;
          maxN = n;
        }
      }

      Console.WriteLine(maxN / maxFraction);
      return maxN;
    }

    public long GetAnswerTo72()
    {
      const int limit = 1000000;
      var sieve = new CoprimeSieve(limit);
      return Sequences.Range(2, limit).Sum(n => sieve.Phi(n));
    }

    // This method is rather slow
    private static bool IsPermutation(long firstNumber, long secondNumber)
    {
      if (secondNumber < firstNumber / 10) return false;

      var firstNormalised = firstNumber.ToString(CultureInfo.InvariantCulture);
      var secondNormalised = secondNumber.ToString(CultureInfo.InvariantCulture);

      return secondNormalised.Length == firstNormalised.Count() &&
             secondNormalised.OrderBy(c => c).SequenceEqual(firstNormalised.OrderBy(c => c));
    }

    // This method validates Phi, but doesn't perform well enough to run on large volumes of data
    private static long Phi(long number)
    {
      return Sequences.Range(1, number).Count(candidate => GcdRecursiveMemo(number, candidate) == 1);
    }

    private static long GcdRecursive(long firstNumber, long secondNumber)
    {
      if (secondNumber == 0) return firstNumber;
      return GcdRecursiveMemo(secondNumber, firstNumber%secondNumber);
    }

    private static readonly Func<long, long, long> GcdRecursiveMemo = ((Func<long, long, long>)GcdRecursive).Memoize();
  }
}