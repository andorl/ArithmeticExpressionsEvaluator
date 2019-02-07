using System;

namespace ArithmeticExpressionsRecognizer
{
    public class LexerException : Exception
    {
        public LexerException(string message) : base(message) { }
        public LexerException(string message, Exception inner) : base(message, inner) { }
    }
}