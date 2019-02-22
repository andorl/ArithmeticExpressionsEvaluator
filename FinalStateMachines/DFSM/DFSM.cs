using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FinalStateMachines
{
    using State = Int32;

    public class DFSM : IFinalStateMachine
    {
        private Dictionary<State, Dictionary<char, State>> transitions
            = new Dictionary<int, Dictionary<char, int>>();

        private readonly HashSet<State> finalStates;

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
                    transitions[qstart] = new Dictionary<char, int>();

                transitions[qstart][symbol] = qend;
            }

        }

        public State InitialState => 0;

        public bool Solve(string word) => IsFinalState(GetLastState(word));

        private State GetLastState(string word)
        {
            var currentState = InitialState;
            foreach (var currentSymbol in word)
            {
                currentState = transitions[currentState][currentSymbol];
            }

            return currentState;
        }

        private bool IsFinalState(State state) => finalStates.Contains(state);
    }
}
