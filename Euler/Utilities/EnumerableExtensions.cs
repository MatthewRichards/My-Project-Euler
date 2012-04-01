using System;
using System.Collections.Generic;
using System.Linq;

namespace Euler.Utilities
{
  public static class EnumerableExtensions
  {
    public static T MinBy<T, TMin>(this IEnumerable<T> enumerable, Func<T, TMin> selector)
    {
      var comparer = Comparer<TMin>.Default;
      TMin minSelected = default(TMin);
      T minKey = default(T);
      bool firstTime = true;
      
      foreach (var value in enumerable)
      {
        var selected = selector(value);

        if (firstTime || comparer.Compare(selected, minSelected) < 0)
        {
          firstTime = false;
          minSelected = selected;
          minKey = value;
        }
      }

      return minKey;
    }
  }
}