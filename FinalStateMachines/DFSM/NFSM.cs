using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FinalStateMachines
{
    using State = Int32;

    public class NFSM : FinalStateMachine
    {
        private readonly Dictionary<State, Dictionary<char, List<State>>> transitions
            = new Dictionary<State, Dictionary<char, List<State>>>();

        public NFSM(string source) : this(new StringReader(source))
        {
        }

        public NFSM(TextReader source)
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
                    transitions[qstart] = new Dictionary<char, List<State>>();

                if (!transitions[qstart].ContainsKey(symbol))
                    transitions[qstart][symbol] = new List<State>();

                transitions[qstart][symbol].Add(qend);
            }
        }

        public override bool Recognize(string word)
        {
            var configurations = new Stack<Configuration>();
            var visitedConfigs = new HashSet<Configuration>();

            var initialConfig = new Configuration(string.Empty, InitialState);

            RegisterNewConfig(initialConfig);

            while (configurations.Any())
            {
                var currentConfig = configurations.Peek();
                var currentState = currentConfig.State;
                var currentTape = currentConfig.Tape;
                if (currentTape == word)
                {
                    if (IsFinalState(currentState))
                    {
                        return true;
                    }
                    else
                    {
                        configurations.Pop();
                        continue;
                    }
                }
                var nextSymbol = word[currentTape.Length];

                List<int> statesAvaliableForMove = new List<int>();
                try
                {
                    statesAvaliableForMove.AddRange(transitions[currentState][nextSymbol]);
                }
                catch
                {
                    // список останется пустым
                }

                var tapeOnNextMove = currentTape + nextSymbol;

                var newUnvisitedConfig = new List<int>().Select(stateToMove => new Configuration(tapeOnNextMove, stateToMove))
                    .FirstOrDefault(config => !visitedConfigs.Contains(config));

                if (newUnvisitedConfig != null)
                {
                    RegisterNewConfig(newUnvisitedConfig);
                }

                else
                {
                    configurations.Pop();
                }
            }

            return false;

            void RegisterNewConfig(Configuration configuration)
            {
                configurations.Push(configuration);
                visitedConfigs.Add(configuration);
            }
        }
    }
}