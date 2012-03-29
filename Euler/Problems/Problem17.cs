using System.Linq;
using Euler.Utilities;

namespace Euler.Problems
{
  public class Problem17
  {
    public int GetAnswer()
    {
      return Enumerable.Range(1, 1000).Sum(num => num.Say().Split(' ').Sum(word => word.Length));
    }
  }
}