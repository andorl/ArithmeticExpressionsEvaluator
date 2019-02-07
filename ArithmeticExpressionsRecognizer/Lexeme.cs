namespace ArithmeticExpressionsRecognizer
{
    class Lexeme
    {
        public Lexeme(LexemeType type, string text)
        {
            Type = type;
            Text = text;
        }

        public LexemeType Type { get; }
        public string Text { get; }
    }

    enum LexemeType
    {
        Plus,
        Minus,
        Multiply,
        Divide,
        Power,
        OpeningBracket,
        ClosingBracket,
        Number,
        Eof
    }
}