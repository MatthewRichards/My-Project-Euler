using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Euler
{
  class Program
  {
    static void Main(string[] args)
    {
      DateTime startTime = DateTime.Now;
      var answer = new Problem12().GetAnswer();
      DateTime endTime = DateTime.Now;

      Console.Out.WriteLine("Solved in " + (endTime - startTime).Milliseconds + "ms");
      Console.Out.WriteLine(answer);
      Console.In.ReadLine();
    }

    
  }
}
