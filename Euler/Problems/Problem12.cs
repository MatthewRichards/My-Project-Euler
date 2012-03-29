using System.Linq;
using Euler.Utilities;

namespace Euler.Problems
{
  public class Problem12
  {
    public int GetAnswer()
    {
      return Sequences.TriangleNumbers().First(number => number.Factors().Count() > 500);
    }
  }
}