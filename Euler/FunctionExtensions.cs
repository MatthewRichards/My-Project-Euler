using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Euler
{
  static class FunctionExtensions
  {
    public static Func<T1, T2, TResult> Memoize<T1, T2, TResult>(this Func<T1, T2, TResult> function)
    {
      var memory = new Dictionary<Tuple<T1, T2>, TResult>();

      return (arg1, arg2) =>
               {
                 TResult cachedValue;
                 var key = new Tuple<T1, T2>(arg1, arg2);

                 if (memory.TryGetValue(key, out cachedValue))
                 {
                   return cachedValue;
                 }

                 cachedValue = function(arg1, arg2);
                 memory[key] = cachedValue;
                 return cachedValue;
               };
    }
  }
}
