using System;

namespace FinalStateMachines
{
    public class Configuration
    {
        public Configuration(string tape, int state)
        {
            Tape = tape;
            State = state;
        }

        public string Tape { get; }
        public Int32 State { get; }

        public override bool Equals(object obj)
        {
            return obj is Configuration configuration 
                   && Tape == configuration.Tape 
                   && State == configuration.State;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Tape, State);
        }
    }
}