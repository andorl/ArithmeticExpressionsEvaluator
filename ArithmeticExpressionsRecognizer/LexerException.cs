using System;

namespace ArithmeticExpressionsRecognizer
{
    class LexerException : Exception
    {
        public LexerException(string message) : base(message) { }
        public LexerException(string message, Exception inner) : base(message, inner) { }
    }
}