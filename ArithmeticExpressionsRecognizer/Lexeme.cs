namespace ArithmeticExpressionsRecognizer
{
    public class Lexeme
    {
        public Lexeme(LexemeType type, string text)
        {
            Type = type;
            Text = text;
        }

        public LexemeType Type { get; }
        public string Text { get; }
    }

    public enum LexemeType
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