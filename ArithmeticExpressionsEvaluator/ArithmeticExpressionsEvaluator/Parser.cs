using System;
using System.IO;

namespace ArithmeticExpressionsEvaluator
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
            UpdateCurrent();
        }


        public Parser(string expression)
            : this(new Lexer(new StringReader(expression)))
        {

        }

        public int ParseExpression()
        {
            int temp = ParseTerm();
            while (current.Type == LexemeType.Plus || current.Type == LexemeType.Minus)
            {
                var operationType = current.Type;
                UpdateCurrent();

                if (operationType == LexemeType.Plus)
                {
                    temp += ParseTerm();
                }

                else if (operationType == LexemeType.Minus)
                {
                    temp -= ParseTerm();
                }
            }

            return temp;
        }

        private int ParseTerm()
        {
            int temp = ParseFactor();

            while (current.Type == LexemeType.Multiply || current.Type == LexemeType.Divide)
            {
                var operationType = current.Type;
                UpdateCurrent();

                if (operationType == LexemeType.Multiply)
                {
                    temp *= ParseFactor();
                }

                else if (operationType == LexemeType.Divide)
                {
                    temp /= ParseFactor();
                }

            }

            return temp;
        }

        private int ParseFactor()
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

        private int ParsePower()
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

        private int ParseAtom()
        {
            if (current.Type == LexemeType.OpeningBracket)
            {
                UpdateCurrent();
                int temp = ParseExpression();
                if (current.Type != LexemeType.ClosingBracket)
                {
                    throw new ParserException(current.Text[0], lexer.CurrentIndex - 1);
                }
                UpdateCurrent();
                return temp;
            }
            else if (current.Type == LexemeType.Number)
            {
                int number = int.Parse(current.Text);

                UpdateCurrent();
                return number;
            }

            else
            {
                throw new ParserException(current.Text[0], lexer.CurrentIndex - 1);
            }
        }

        private void UpdateCurrent()
        {
            try
            {
                current = lexer.GetNextLexeme();
            }

            catch (LexerException le)
            {
                throw new ParserException(lexer.CurrentChar, le);
            }
        }
    }
}