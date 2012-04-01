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
      // It's easier to work out how many coprimes there aren't, than how many there are
      // So count all the non-coprimes of each number
      var inversePhi = new long[10000001];
      var allPrimes = Sequences.Primes().TakeWhile(prime => prime < inversePhi.Length).ToList();

      AddMultiplesOfPrimes(allPrimes, 1, 1, true, inversePhi);

      // Work out how many many non-coprimes there are
      // The above double-counts any multiples of two primes, so correct for them
      // But then that under-counts multiples of three primes, by subtracting them twice, so add these back on
      // But then that double-counts multiples of four primes... etc
      // This can't go on to more than multiples of 8 primes as 2x3x...x19x23 (first 9 primes) is more than 10^7

      Func<long, long> phi = n => n - inversePhi[n];
      return Sequences.Range(2, 10000000).Where(n => IsPermutation(n, phi(n))).MinBy(n => (double)n/phi(n));

      // Attempt to be cunning by iterating through all coprime pairs
      /*
      var phi = new long[100001];

      try
      {
        foreach (var coprimePair in Coprimes(5000))
        {
          phi[coprimePair.Item1]++;
        }
      }
      catch
      {
      }

      return phi[87109];
      */

      // Assume it's the highest prime such that p-1 is a pemutation of p. It's not.
      /*
      foreach (var prime in Sequences.Primes().TakeWhile(prime => prime < 10000000).Reverse())
      {
        if (IsPermutation(prime, prime - 1))
        {
          return prime;
        }
      }

      return -1;
      */
      /*
      // Brute force method
      var candidates = Sequences.Range(1, 100000).Where(number => IsPermutation(number, Phi(number)));

      foreach (var candidate in candidates)
      {
        Console.WriteLine(candidate);
      }

      return 42;
      */
    }

    private static void AddMultiplesOfPrimes(IList<long> allPrimes, long multipleOfPrimesSoFar, long highestPrimeSoFar, bool addPrimes, long[] inversePhi)
    {
      if (multipleOfPrimesSoFar*highestPrimeSoFar >= inversePhi.Length) return;

      var maxAdditionalPrime = inversePhi.Length/multipleOfPrimesSoFar;

      foreach (var additionalPrime in allPrimes.SkipWhile(prime => prime <= highestPrimeSoFar).TakeWhile(prime => prime < maxAdditionalPrime))
      {
        var newMultipleOfPrimes = multipleOfPrimesSoFar*additionalPrime;
        long multiple = newMultipleOfPrimes;

        for (long multiplier = 1; multiple < inversePhi.Length; multiplier++)
        {
          if (addPrimes)
          {
            inversePhi[multiple] += multiplier;
          }
          else
          {
            inversePhi[multiple] -= multiplier;
          }
          multiple += newMultipleOfPrimes;
        }

        AddMultiplesOfPrimes(allPrimes, newMultipleOfPrimes, additionalPrime, !addPrimes, inversePhi);
      }
    }

    private static long Gcd(long firstNumber, long secondNumber)
    {
      while (secondNumber > 0)
      {
        if (firstNumber > secondNumber)
        {
          firstNumber -= secondNumber;
        }
        else
        {
          secondNumber -= firstNumber;
        }
      }

      return firstNumber;
    }

    private static readonly Func<long, long, long> GcdRecursiveMemo = ((Func<long, long, long>)GcdRecursive).Memoize();

    private static long GcdRecursive(long firstNumber, long secondNumber)
    {
      if (secondNumber == 0) return firstNumber;
      return GcdRecursiveMemo(secondNumber, firstNumber%secondNumber);
    }

    private static IEnumerable<Tuple<long, long>> Coprimes(long limit)
    {
      var pairs = new Queue<Tuple<long, long>>(10000000);
      pairs.Enqueue(new Tuple<long, long>(2, 1));
      pairs.Enqueue(new Tuple<long, long>(3, 1));
      
      while (true)
      {
        var pair = pairs.Dequeue();
        if (pair.Item1 > limit) continue;

        yield return pair;

        var next1 = new Tuple<long, long>(2*pair.Item1 - pair.Item2, pair.Item1);
        var next2 = new Tuple<long, long>(2 * pair.Item1 + pair.Item2, pair.Item1);
        var next3 = new Tuple<long, long>(pair.Item1 + 2 * pair.Item2, pair.Item2);
        pairs.Enqueue(next1);
        pairs.Enqueue(next2);
        pairs.Enqueue(next3);
      }
    }

    private static long Phi(long number)
    {
      // Is there a better way of getting coprimes than just testing them all? Must be.
      return Sequences.Range(1, number).Count(candidate => GcdRecursiveMemo(number, candidate) == 1);
    }

    private static Func<long, bool> GetCoprimeTester(long firstNumber)
    {
      IEnumerable<long> primeFactors = firstNumber.PrimeFactors().ToList();
      return secondNumber => primeFactors.All(prime => secondNumber%prime != 0);
    }

    private static bool IsPermutation(long firstNumber, long secondNumber)
    {
      if (secondNumber < firstNumber / 10) return false;

      var firstNormalised = firstNumber.ToString(CultureInfo.InvariantCulture);
      var secondNormalised = secondNumber.ToString(CultureInfo.InvariantCulture);

      return secondNormalised.Length == firstNormalised.Count() &&
             secondNormalised.OrderBy(c => c).SequenceEqual(firstNormalised.OrderBy(c => c));
    }
  }
}