using System;

namespace ArithmeticExpressionsRecognizer
{
    public class LexerException : Exception
    {
        public LexerException(char charCaused) 
            : base($"Arithmetic expressions can't contain this character: {charCaused}")
        {
            CharCaused = charCaused;
        }

        public char CharCaused { get; }
    }
}