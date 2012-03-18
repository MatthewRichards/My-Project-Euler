using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Euler
{
  class Problem16
  {
    public long GetAnswer()
    {
      var current = new List<int> {4};
      var power = 2;

      while (power < 10000000)
      {
        current = current.Select(digit => digit*2).ToList();

        if (current.Any(digit => digit > 100000))
        {
          Distribute(current);
        }

        power++;
      }

      Distribute(current);
      return current.Aggregate(0L, (acc, digit) => acc + digit);
    }

    private static void Distribute(List<int> digits)
    {
      int carry = 0;

      for (int index = digits.Count - 1; index >= 0; index--)
      {
        digits[index] += carry;

        if (digits[index] >= 10)
        {
          carry = digits[index] / 10;
          digits[index] = digits[index] % 10;
        }
        else
        {
          carry = 0;
        }
      }

      while (carry > 0)
      {
        digits.Insert(0, carry%10);
        carry = carry/10;
      }
    }
  }
}
