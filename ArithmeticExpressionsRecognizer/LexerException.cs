using System;

namespace ArithmeticExpressionsRecognizer
{
    public class LexerException : Exception
    {
        public LexerException(char charCaused, int position) 
            : base($"Found illegal character '{charCaused}'  on  position {position}!")
        {
            CharCaused = charCaused;
            Position = position;
        }

        public char CharCaused { get; }
        public int Position { get;  }
    }
}