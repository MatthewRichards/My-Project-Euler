using System;
using Euler.Utilities;

namespace Euler.Problems
{
  public class Problem15
  {
    public long GetAnswer()
    {
      return GridRoutesMemo(20, 20);
    }

    private static readonly Func<int, int, long> GridRoutesMemo = ((Func<int, int, long>)GridRoutes).Memoize();

    static long GridRoutes(int width, int height)
    {
      if (width == 0 || height == 0)
      {
        return 1;
      }

      if (width > height)
      {
        return GridRoutesMemo(height, width);
      }

      return GridRoutesMemo(width - 1, height) + GridRoutesMemo(width, height - 1);
    }

  }
}