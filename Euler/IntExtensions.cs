using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Euler
{
  static class IntExtensions
  {
    private static readonly Dictionary<int, string> Sayings =
      new Dictionary<int, string>()
        {
          {1, "one"},
          {2, "two"},
          {3, "three"},
          {4, "four"},
          {5, "five"},
          {6, "six"},
          {7, "seven"},
          {8, "eight"},
          {9, "nine"},
          {10, "ten"},
          {11, "eleven"},
          {12, "twelve"},
          {13, "thirteen"},
          {14, "fourteen"},
          {15, "fifteen"},
          {16, "sixteen"},
          {17, "seventeen"},
          {18, "eighteen"},
          {19, "nineteen"},
          {20, "twenty"},
          {30, "thirty"},
          {40, "forty"},
          {50, "fifty"},
          {60, "sixty"},
          {70, "seventy"},
          {80, "eighty"},
          {90, "ninety"}
        };

    public static string Say(this int number)
    {
      if (number >= 1000)
      {
        int thousands = number/1000;
        int remainder = number%1000;

        if (remainder > 0)
        {
          return Sayings[thousands] + " thousand " + remainder.Say();
        }

        return Sayings[thousands] + " thousand";
      }

      if (number >= 100)
      {
        int hundreds = number/100;
        int remainder = number%100;

        if (remainder > 0)
        {
          return Sayings[hundreds] + " hundred and " + remainder.Say();
        }

        return Sayings[hundreds] + " hundred";
      }

      if (number <= 20)
      {
        return Sayings[number];
      }

      int tens = number/10;
      int digits = number%10;

      if (digits > 0)
      {
        return Sayings[tens*10] + " " + Sayings[digits];
      }

      return Sayings[tens*10];
    }

    public static IEnumerable<int> Factors(this int number)
    {
      yield return 1;

      if (number == 1) yield break;

      yield return number;

      double sqrt = Math.Sqrt(number);

      for (int test = 2; test < sqrt; test++)
      {
        if (number % test == 0)
        {
          yield return test;
          yield return (number/test);
        }
      }
    }
  }
}
