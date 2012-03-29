using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Euler.Problems
{
  public class Problem81To83
  {
    public int GetAnswer()
    {
      int[][] input = File.ReadAllLines(@"E:\Test\array.txt").Select(line => line.Split(',').Select(int.Parse).ToArray()).ToArray();
      int[][] minsum = input.Select(row => row.Select(cell => cell).ToArray()).ToArray();
      bool changed = true;

      while (changed)
      {
        changed = false;

        for (int x = 0; x < input.Length; x++)
        {
          for (int y = 0; y < input.Length; y++)
          {
            var newMin = Neighbours(minsum, x, y).Min() + input[x][y];

            if (newMin != minsum[x][y])
            {
              minsum[x][y] = newMin;
              changed = true;
            }
          }
        }
      }

      return minsum[0][0];
    }
    
    private IEnumerable<int> Neighbours(int[][] values, int x, int y)
    {
      if (x == values.Length - 1 && y == values.Length -1)
      {
        yield return 0;
        yield break;
      }
      
      if (x < values.Length - 1) yield return values[x + 1][y];
      if (y < values.Length - 1) yield return values[x][y + 1];
      if (x > 0) yield return values[x - 1][y];
      if (y > 0) yield return values[x][y - 1];
    }
  }
}