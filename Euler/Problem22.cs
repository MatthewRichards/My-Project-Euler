using System;
using System.IO;
using System.Linq;

namespace Euler
{
  public class Problem22
  {
    public long GetAnswer()
    {
      var names = File.ReadAllText(@"E:\test\names.txt").Split(',').Select(name => name.Trim('"'));
      var sortedNames = names.OrderBy(name => name);
      var nameScores = sortedNames.Select(name => name.Sum(chr => chr - 'A' + 1));
      return nameScores.Aggregate(new Tuple<long, int>(0, 1),
                                  (progress, score) => new Tuple<long, int>(progress.Item1 + (score * progress.Item2), progress.Item2 + 1)).Item1;

    }
  }
}