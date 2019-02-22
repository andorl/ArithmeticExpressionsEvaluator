using System;

namespace ArithmeticExpressionsEvaluator
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

        public override bool Equals(object another)
        {
            return another is Lexeme lexeme &&
                   Type == lexeme.Type &&
                   Text == lexeme.Text;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Type, Text);
        }
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