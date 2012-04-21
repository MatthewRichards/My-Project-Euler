using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Euler.Problems
{
  public class Problem40
  {
    private Queue<int> targetDigits;
    private readonly List<int> answers = new List<int>();
 
    public int GetAnswer()
    {
      targetDigits = new Queue<int>(new[] {1, 10, 100, 1000, 10000, 100000, 1000000});
      Digits(1, 10, 1, 0);
      return answers.Aggregate(1, (acc, num) => acc*num);
    }

    public void Digits(int start, int limit, int lengthPerDigit, int digitsSoFar)
    {
      int newNumber = limit - start;
      int newDigits = newNumber * lengthPerDigit;

      int target = targetDigits.Peek();

      while (target < digitsSoFar + newDigits)
      {
        targetDigits.Dequeue();

        int offset = (target - digitsSoFar) - 1;
        int num = (int)Math.Floor((double)offset/lengthPerDigit);
        int digit = offset%lengthPerDigit;
        answers.Add((start + num).ToString(CultureInfo.InvariantCulture)[digit] - '0');

        if (targetDigits.Count == 0)
        {
          return;
        }

        target = targetDigits.Peek();
      }

      Digits(limit, limit*10, lengthPerDigit + 1, digitsSoFar + newDigits);
    }
  }
}