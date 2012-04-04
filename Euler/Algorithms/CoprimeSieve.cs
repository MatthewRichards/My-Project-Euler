using System;
using System.Collections.Generic;
using System.Linq;
using Euler.Utilities;

namespace Euler.Algorithms
{
  public class CoprimeSieve
  {
    private readonly long[] inversePhi;
    private readonly long[] ceilings;
    private readonly long[] largestCoprimes;

    public CoprimeSieve(long limit) : this(limit, new long[limit + 1])
    {
    }

    public CoprimeSieve(long limit, long[] ceilings)
    {
      // It's easier to work out how many coprimes there aren't, than how many there are
      // So count all the non-coprimes of each number instead
      inversePhi = new long[limit + 1];
      largestCoprimes = new long[limit + 1];
      this.ceilings = ceilings;

      var allPrimes = Sequences.Primes().TakeWhile(prime => prime <= limit).ToList();
      AddMultiplesOfPrimes(allPrimes, 1, 1, true, inversePhi);
    }

    public long Phi(long number)
    {
      return number - inversePhi[number];
    }

    public long LargestCoprime(long number)
    {
      return largestCoprimes[number];
    }

    private static void AddMultiplesOfPrimes(IList<long> allPrimes, long multipleOfPrimesSoFar, long highestPrimeSoFar, bool addPrimes, long[] inversePhi)
    {
      // We count non-coprimes by considering in turn each prime number, and all its multiples
      // So the multiples of 2 are 2,4,6,8 etc. We can see that from this list,
      // 2 has 1 non-coprime, 4 has 2 non-coprimes, 6 has 3 non-coprimes, etc.
      //
      // We repeat for all prime numbers to find all the non-coprimes of any number.

      if (multipleOfPrimesSoFar * highestPrimeSoFar >= inversePhi.Length) return;

      var maxAdditionalPrime = inversePhi.Length / multipleOfPrimesSoFar;

      foreach (var additionalPrime in allPrimes.SkipWhile(prime => prime <= highestPrimeSoFar).TakeWhile(prime => prime <= maxAdditionalPrime))
      {
        var newMultipleOfPrimes = multipleOfPrimesSoFar * additionalPrime;
        long multiple = newMultipleOfPrimes;

        for (int multiplier = 1; multiple < inversePhi.Length; multiplier++)
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

        // Of course, we're not actually "finding" non-coprimes, but counting them. And the
        // method above double-counts numbers that are multiples of several primes.
        //
        // E.g. consider 6 = 2*3. We will mark 6 as a multiple of 2 (3 non-coprimes - 2,4,6) and 
        // of 3 (2 non-coprimes - 3,6). 6 is marked as a non-coprime twice. So if we've just added all
        // the multiples of 2, we now need to go through and subtract off all the multiples of 2*p for
        // primes p>2.
        //
        // However this will double-count (i.e. subtract twice) any multiple that is itself a prime,
        // e.g. consider 30 = 2*3*5. Without the line of code below we would record the following
        // non-coprimes of 30:
        //   2*... : 2,4,6,...,30 = 15 non-coprimes
        //   3*... : 3,6,9,...,30 = 10 non-coprimes
        //   5*... : 5,10,....,30 = 6 non-coprimes
        //
        // We realise we've counted 6,10,12,etc multiple times, so as per the second paragraph of this
        // comment we subtract off:
        //   2*3...: 6,12,...,30  = 5 non-coprimes
        //   2*5...: 10,20,30     = 3 non-coprimes
        //   3*5...: 15,30        = 2 non-coprimes
        //
        // But now we've subtracted off 30 three times, having only added it three times in the first place!
        // This is because 30 is has three prime factors, not just two, so we've "found" it as many times in
        // the subtraction as we did in the original addition. Hence we now need to go through all the 
        // combinations of /three/ primes, adding them back on to the coprimes list again:
        //   2*3*5...: 30 = 1 non-coprime
        //
        // Which leaves us with inversePhi(30)=22 => phi(30)=8, which is correct.
        //
        // And of course this keeps going, potentially, until we've alternated add/subtract as many
        // times as there can be distinct prime factors of any number. Which for 10^7 happens to be 8.

        AddMultiplesOfPrimes(allPrimes, newMultipleOfPrimes, additionalPrime, !addPrimes, inversePhi);
      }
    }
    
  }
}