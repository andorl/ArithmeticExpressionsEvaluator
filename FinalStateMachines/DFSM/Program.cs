using System;
using System.Collections.Generic;
using System.Linq;

namespace FinalStateMachines
{
    public static class Program
    {
        // a <--> 0, b <--> 1
        private static string dSource =
@"0
0 a 0
0 b 1
1 a 1
1 b 0";
        private static string nSource = 
@"1
0 a 0
0 b 0
0 b 1";
        public static void Main(string[] args)
        {
            var dfsm = new DFSM(dSource);

            //чётн. кол-во b
            new List<string>
            {
                "b",
                "ab",
                "abb",
                "abbb",
                "baabaa",
                "baaaa",
                "aabbaa",
            }.ForEach(text => Console.WriteLine($"{text}: {dfsm.Recognize(text)}"));

            var nfsm = new NFSM(nSource);

            Console.WriteLine("---");

            //заканч. на b
            new List<string>
            {
                "b",
                "ab",
                "aaba",
                "aabb",
                "baabaa",
                "baaab",
                "aabbba",
            }.ForEach(text => Console.WriteLine($"{text}: {nfsm.Recognize(text)}"));


        }
    }
}