using System;
using System.Linq;

namespace Euler
{
  public class Problem23
  {
    public long GetAnswer()
    {
      int perms = 0;
      long number = 12345677;

      while (perms < 1000000)
      {
        number++;
        if (number.ToString("000000000").Distinct().Count() == 9)
        {
          perms++;

          if (perms % 1000 == 0)
          {
            Console.Out.WriteLine(perms + ": " + number);
          }
        }
      }

      return number;
    }
  }
}