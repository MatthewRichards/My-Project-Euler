using System;
using System.Collections.Generic;
using System.Linq;

namespace Euler.Utilities
{
  public static class EnumerableExtensions
  {
    public static T MinBy<T, TMin>(this IEnumerable<T> enumerable, Func<T, TMin> selector)
    {
      return enumerable.FindExtremeWithPostCondition(selector, val => true, true);
    }

    public static T MinByWithPostCondition<T, TMin>(this IEnumerable<T> enumerable, Func<T, TMin> selector, Func<T, bool> postCondition)
    {
      return enumerable.FindExtremeWithPostCondition(selector, postCondition, true);
    }

    public static T MaxBy<T, TMax>(this IEnumerable<T> enumerable, Func<T, TMax> selector)
    {
      return enumerable.FindExtremeWithPostCondition(selector, val => true, false);
    }

    public static T FindExtremeWithPostCondition<T, TExtreme>(
      this IEnumerable<T> enumerable, 
      Func<T, TExtreme> selector, 
      Func<T, bool> postCondition,
      bool findMinimum)
    {
      var comparer = Comparer<TExtreme>.Default;
      TExtreme minSelected = default(TExtreme);
      T minKey = default(T);
      bool firstTime = true;

      foreach (var value in enumerable)
      {
        var selected = selector(value);

        if ((firstTime || (findMinimum ^ (comparer.Compare(selected, minSelected)) > 0)) && postCondition(value))
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