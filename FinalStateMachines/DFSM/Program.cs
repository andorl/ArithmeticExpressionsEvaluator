using System;
using System.Linq;

namespace FinalStateMachines
{
    public static class Program
    {
        private static string source =
@"0
0 a 0
0 b 1
1 a 1
1 b 0";

        public static void Main(string[] args)
        {
            var dfsm = new DFSM(source);
            new[] {
                "ab",
                "abb",
                "abbb",
                "baabaa",
                "baaaa",
                "aabbaa", }.ToList().ForEach(line => Console.WriteLine($"{line}: {dfsm.Solve(line)}"));
        }
    }
}