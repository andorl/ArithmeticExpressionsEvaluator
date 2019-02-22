using System;

namespace ArithmeticExpressionsEvaluator
{
    public class ParserException : Exception
    {
        public ParserException(char charCaused, int position) 
            : base($"The first occurence of character '{charCaused}' is on illegal position {position}!")
        {
            CharCaused = charCaused;
            Position = position;
        }


        public ParserException(char charCaused, LexerException innerException)
            : base(innerException.Message, innerException)
        {
            CharCaused = charCaused;
            Position = innerException.Position;
        }

        public char CharCaused { get;  }
        public int Position { get; }
    }
}