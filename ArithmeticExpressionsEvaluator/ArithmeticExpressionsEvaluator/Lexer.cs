﻿using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ArithmeticExpressionsEvaluator
{
    public class Lexer
    {
        public int CurrentIndex => currentIndex;
        public char CurrentChar => (char)current;

        private static readonly Dictionary<char, LexemeType> charToTypeMappings = new Dictionary<char, LexemeType>
        {
            ['+'] = LexemeType.Plus,
            ['-'] = LexemeType.Minus,
            ['*'] = LexemeType.Multiply,
            ['/'] = LexemeType.Divide,
            ['^'] = LexemeType.Power,
            ['('] = LexemeType.OpeningBracket,
            [')'] = LexemeType.ClosingBracket,
        };

        private readonly TextReader reader; 
        private int current;
        private int currentIndex = -1;


        public Lexer(TextReader reader)
        {
            this.reader = reader;
            UpdateCurrent();
        }


        public Lexeme GetNextLexeme()
        {
            if (current == -1)
            {
                return new Lexeme(LexemeType.Eof, char.MinValue.ToString());
            }


            var currentChar = (char) current;

            if (char.IsWhiteSpace(currentChar))
            {
                UpdateCurrent();
                return GetNextLexeme();
            }

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
                var number = numberBuilder.ToString();
                numberBuilder.Clear();
                return new Lexeme(LexemeType.Number, number);
            }

            //ни один из вариантов не подошёл -> ошибка
            throw new LexerException(currentChar, currentIndex);
        }

        public IEnumerable<Lexeme> EnumerateLexemes()
        {
            for (var lexeme = GetNextLexeme(); lexeme.Type != LexemeType.Eof; lexeme = GetNextLexeme())
            {
                yield return lexeme;
            }
        }

        private int UpdateCurrent()
        {
            current = reader.Read();
            currentIndex++;
            return current;
        }

    }
}
