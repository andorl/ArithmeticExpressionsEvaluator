using System;
using System.IO;

namespace ArithmeticExpressionsRecognizer
{
    /*  
        expr -> term +|- term +|- term +|- ...
        term -> factor *|/ factor *|/ factor *|/ ...
        factor -> power^factor | power
        power -> atom | -atom
        atom -> number | (expr)

        whitespace is ignored
    */
    public class Parser
    {
        private readonly Lexer lexer;
        private Lexeme current;

        public Parser(Lexer lexer)
        {
            this.lexer = lexer;
        }


        public Parser(string expression) 
            : this(new Lexer(new StringReader(expression))) { }

        public int ParseExpression()
        {
            int temp = ParseTerm();
            while (current.Type == LexemeType.Plus || current.Type == LexemeType.Minus)
            {
                if (current.Type == LexemeType.Plus)
                {
                    temp += ParseTerm();
                }

                else if (current.Type == LexemeType.Minus)
                {
                    temp -= ParseTerm();
                }
            }

            UpdateCurrent();
            return temp;
        }

        public int ParseTerm()
        {
            int temp = ParseFactor();
            while (current.Type == LexemeType.Multiply || current.Type == LexemeType.Divide)
            {
                if (current.Type == LexemeType.Multiply)
                {
                    temp *= ParseFactor();
                }

                else if (current.Type == LexemeType.Divide)
                {
                    temp /= ParseFactor();
                }
            }

            UpdateCurrent();
            return temp;
        }

        public int ParseFactor()
        {
            int temp = ParsePower();
            if (current.Type == LexemeType.Power)
            {
                UpdateCurrent();
                return (int) Math.Pow(temp, ParseFactor());
            }
            else
            {
                return temp;
            }
        }

        public int ParsePower()
        {
            if (current.Type == LexemeType.Minus)
            {
                UpdateCurrent();
                return -ParseAtom();
            }

            else
            {
                return ParseAtom();
            }

        }

        public int ParseAtom()
        {
            if (current.Type == LexemeType.OpeningBracket)
            {
                UpdateCurrent();
                int temp = ParseExpression();
                if (current.Type != LexemeType.ClosingBracket)
                {
                    throw new ParserException();
                }

                UpdateCurrent();
                return temp;
            }
            else if (current.Type == LexemeType.Number)
            {
                return int.Parse(current.Text);
            }

            else
            {
                throw new ParserException();
            }
        }

        private Lexeme UpdateCurrent()
        {
            current = lexer.GetNextLexeme();
            return current;
        }
    }
}