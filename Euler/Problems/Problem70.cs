using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Euler.Utilities;

namespace Euler.Problems
{
  public class Problem70
  {
    public long GetAnswer()
    {
      var candidates = Sequences.Range(1, 3000).Where(number => IsPermutation(number, Phi(number)));

      foreach (var candidate in candidates)
      {
        Console.WriteLine(candidate);
      }

      return 42;
    }

    private static long Phi(long number)
    {
      var isCoprime = GetCoprimeTester(number);

      // Is there a better way of getting coprimes than just testing them all? Must be.
      return Sequences.Range(1, number).Count(isCoprime);
    }

    private static Func<long, bool> GetCoprimeTester(long firstNumber)
    {
      IEnumerable<long> primeFactors = firstNumber.PrimeFactors().ToList();
      return secondNumber => primeFactors.All(prime => secondNumber%prime != 0);
    }

    private static bool IsPermutation(long firstNumber, long secondNumber)
    {
      var firstNormalised = firstNumber.ToString(CultureInfo.InvariantCulture);
      var secondNormalised = secondNumber.ToString(CultureInfo.InvariantCulture);

      return secondNormalised.Length == firstNormalised.Count() &&
             secondNormalised.OrderBy(c => c).SequenceEqual(firstNormalised.OrderBy(c => c));
    }
  }
}