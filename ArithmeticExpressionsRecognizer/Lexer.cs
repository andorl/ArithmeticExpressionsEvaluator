using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ArithmeticExpressionsRecognizer
{
    class Lexer
    {
        private static Dictionary<char, LexemeType> charToTypeMappings = new Dictionary<char, LexemeType>
        {
            ['+'] = LexemeType.Plus,
            ['-'] = LexemeType.Minus,
            ['*'] = LexemeType.Multiply,
            ['/'] = LexemeType.Divide,
            ['^'] = LexemeType.Power,
            ['('] = LexemeType.OpeningBracket,
            [')'] = LexemeType.ClosingBracket,
        };

        private StreamReader reader;
        private int current;

        public Lexer(StreamReader reader)
        {
            this.reader = reader;
            UpdateCurrent();
        }

        private int UpdateCurrent()
        {
            current = reader.Read();
            return current;
        }

        public Lexeme GetNextLexeme()
        {
            if (current == -1)
            {
                return new Lexeme(LexemeType.Eof, "");
            }


            var currentChar = (char) current;

            if (charToTypeMappings.ContainsKey(currentChar))
            {
                UpdateCurrent();
                return new Lexeme(charToTypeMappings[currentChar], currentChar.ToString());
            }

            var numberBuilder = new StringBuilder();

            while (char.IsDigit(currentChar))
            {
                numberBuilder.Append(currentChar);
                currentChar = (char) UpdateCurrent();
            }

            if (numberBuilder.Length > 0)
            {
                numberBuilder.Clear();
                return new Lexeme(LexemeType.Number, numberBuilder.ToString());
            }

            //ни один из вариантов не подошёл -> ошибка
            throw new LexerException("Arithmetic expressions can't contain " +
                                     $"this character: {currentChar}");
        }
    }
}
}