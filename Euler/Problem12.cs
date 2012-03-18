using System.Linq;

namespace Euler
{
  public class Problem12
  {
    public int GetAnswer()
    {
      return Sequences.TriangleNumbers().First(number => number.Factors().Count() > 500);
    }
  }
}