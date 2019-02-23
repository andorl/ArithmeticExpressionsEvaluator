using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FinalStateMachines
{
    using State = Int32;

    public class DFSM : FinalStateMachine
    {
        private readonly Dictionary<State, Dictionary<char, State>> transitions
            = new Dictionary<int, Dictionary<char, State>>();

        public DFSM(string source) : this(new StringReader(source))
        {

        }

        public DFSM(TextReader source)
        {
            var lines = source.ReadToEnd().Split('\n');
            finalStates = new HashSet<int>(lines[0].Split(' ').Select(int.Parse));

            foreach (var transition in lines.Skip(1))
            {
                var split = transition.Split(' ');
                var qstart = int.Parse(split[0]);
                var symbol = Convert.ToChar(split[1]);
                var qend = int.Parse(split[2]);

                if (!transitions.ContainsKey(qstart))
                    transitions[qstart] = new Dictionary<char, State>();

                transitions[qstart][symbol] = qend;
            }

        }

        public override bool Recognize(string word) => IsFinalState(GetLastState(word));

        private State GetLastState(string word)
        {
            var currentState = InitialState;
            foreach (var currentSymbol in word)
            {
                currentState = transitions[currentState][currentSymbol];
            }

            return currentState;
        }
    }
}
