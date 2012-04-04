using System;
using Euler.Problems;

namespace Euler
{
  class Program
  {
    static void Main(string[] args)
    {
      DateTime startTime = DateTime.Now;
      var answer = new Problem69To72().GetAnswer();
      DateTime endTime = DateTime.Now;

      Console.Out.WriteLine("Solved in " + (endTime - startTime).TotalMilliseconds + "ms");
      Console.Out.WriteLine(answer);
      Console.In.ReadLine();
    }

    
  }
}
