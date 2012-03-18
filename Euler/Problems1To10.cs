using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace Euler
{
  class Problems1To10
  {
    private static readonly List<int> PrimesSoFar = new List<int>() { 2, 3 };

    static void Main1(string[] args)
    {
      Console.Out.WriteLine(Problem10());

      Console.In.ReadLine();
    }

    static int Problem1()
    {
      return Enumerable.Range(1, 999).Where(number => number % 3 == 0 || number % 5 == 0).Sum();
    }

    static int Problem2()
    {
      int current = 2;
      int previous = 1;
      int sum = current;
      int sumWaiter = 0;

      while (current <= 4000000)
      {
        int oldCurrent = current;
        current = current + previous;
        previous = oldCurrent;

        if (++sumWaiter == 3)
        {
          sum += current;
          sumWaiter = 0;
        }
      }

      return sum;
    }

    static int Problem3()
    {
      const long target = 600851475143;

      var maxFactor = (int)Math.Sqrt(target);
      int largestPrimeFactor = 1;

      foreach (int prime in Enumerable.Range(2, maxFactor)) // Primes().TakeWhile(p => p < maxFactor))
      {
        if (target % prime == 0 && IsPrime(prime))
        {
          largestPrimeFactor = prime;
        }
      }

      return largestPrimeFactor;
    }

    static bool IsPrime(int number)
    {
      if (number % 2 == 0) return false;

      int possibleDivisor = 3;

      while (possibleDivisor < Math.Sqrt(number))
      {
        if (number % possibleDivisor == 0)
        {
          return false;
        }

        possibleDivisor += 2;
      }

      return true;
    }

    static IEnumerable<int> Primes()
    {
      foreach (int prime in PrimesSoFar)
      {
        yield return prime;
      }

      int currentPrime = 3;

      while (true)
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

    static int Problem4()
    {
      int largestPalindrome = 0;

      foreach (int firstNumber in Enumerable.Range(100, 999 - 100))
      {
        foreach (int secondNumber in Enumerable.Range(100, 999 - 100))
        {
          int multiple = firstNumber * secondNumber;

          if (IsPalindrome(multiple) && multiple > largestPalindrome)
          {
            largestPalindrome = multiple;
            Console.Out.WriteLine("  Found " + multiple);
          }
        }
        Console.Out.WriteLine(firstNumber);
      }

      return largestPalindrome;
    }

    static bool IsPalindrome(int number)
    {
      string stringValue = number.ToString(CultureInfo.InvariantCulture);

      for (int index = 0; index <= stringValue.Length / 2; index++)
      {
        if (stringValue[index] != stringValue[stringValue.Length - index - 1])
        {
          return false;
        }
      }

      return true;
    }

    static int Problem5()
    {
      var combinedFactors = new List<int>();

      for (int number = 20; number > 1; number--)
      {
        AddMissingFactors(combinedFactors, number);
      }

      return combinedFactors.Aggregate(1, (factor, acc) => factor * acc);
    }

    static IEnumerable<int> PrimeFactors(int number)
    {
      int smallestPrimeFactor = Primes().First(p => number % p == 0);
      var justThisFactor = new[] { smallestPrimeFactor };

      if (smallestPrimeFactor == number)
      {
        return justThisFactor;
      }
      return justThisFactor.Concat(PrimeFactors(number / smallestPrimeFactor));
    }

    static void AddMissingFactors(List<int> factors, int number)
    {
      var targetFactors = new List<int>(PrimeFactors(number));

      Console.Out.WriteLine("Factors of " + number + " : " + string.Join(",", targetFactors.Select(f => f.ToString()).ToArray()));

      foreach (int existingFactor in factors)
      {
        if (targetFactors.Contains(existingFactor))
        {
          targetFactors.Remove(existingFactor);
        }
      }
      factors.AddRange(targetFactors);
    }

    static long Problem6()
    {
      long sumOfSquares = Sequence(1, 100).Select(num => num * num).Sum();
      long squareOfSums = Sequence(1, 100).Sum() * Sequence(1, 100).Sum();

      return squareOfSums - sumOfSquares;
    }

    static IEnumerable<long> Sequence(long start, long count)
    {
      for (long number = start; number < (start + count); number++)
      {
        yield return number;
      }
    }

    static int Problem7()
    {
      return Primes().Skip(10000).First();
    }

    static int Problem8()
    {
      const string number = "7316717653133062491922511967442657474235534919493496983520312774506326239578318016984801869478851843858615607891129494954595017379583319528532088055111254069874715852386305071569329096329522744304355766896648950445244523161731856403098711121722383113622298934233803081353362766142828064444866452387493035890729629049156044077239071381051585930796086670172427121883998797908792274921901699720888093776657273330010533678812202354218097512545405947522435258490771167055601360483958644670632441572215539753697817977846174064955149290862569321978468622482839722413756570560574902614079729686524145351004748216637048440319989000889524345065854122758866688116427171479924442928230863465674813919123162824586178664583591245665294765456828489128831426076900422421902267105562632111110937054421750694165896040807198403850962455444362981230987879927244284909188845801561660979191338754992005240636899125607176060588611646710940507754100225698315520005593572972571636269561882670428252483600823257530420752963450";
      int[] digits = number.Select(c => int.Parse(c.ToString())).ToArray();
      int max = 0;

      for (int index = 0; index < digits.Length - 4; index++)
      {
        int trial = digits[index] * digits[index + 1] * digits[index + 2] * digits[index + 3] * digits[index + 4];

        if (trial > max) max = trial;
      }

      return max;
    }

    static int Problem9()
    {
      const int limit = 1000;

      for (int a = 1; a < limit; a++)
      {
        int aSquared = a * a;
        int bSquared = aSquared;

        for (int b = a + 1; a + b + b < limit; b++)
        {
          bSquared += (2 * b) - 1;
          int cSquared = aSquared + bSquared;
          int c = limit - a - b;

          if (cSquared == c * c)
          {
            Console.Out.WriteLine(a);
            Console.Out.WriteLine(b);
            Console.Out.WriteLine(c);
            return a * b * c;
          }
        }
      }

      return -1;
    }

    static long Problem10()
    {
      return Primes().Select(prime => (long)prime).TakeWhile(prime => prime < 2000000L).Sum();
    }
  }

}
