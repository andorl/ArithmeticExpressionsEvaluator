using System;
using System.Collections.Generic;

namespace FinalStateMachines
{
    using State = Int32;

    public abstract class FinalStateMachine
    {
        protected HashSet<State> finalStates;

        public State InitialState => 0;

        public abstract bool Recognize(string tape);

        protected bool IsFinalState(State state) => finalStates.Contains(state);
    }
}