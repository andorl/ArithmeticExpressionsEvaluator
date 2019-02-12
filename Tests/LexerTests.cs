using System.IO;
using System.Linq;
using ArithmeticExpressionsEvaluator;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class LexerTests
    {
        private static readonly object[][] CorrectSequences =
        {
            new object[]
            {
                "(2+2)^(-30/5)",
                new Lexeme[]
                {
                    new Lexeme(LexemeType.OpeningBracket, "("),
                    new Lexeme(LexemeType.Number, "2"),
                    new Lexeme(LexemeType.Plus, "+"),
                    new Lexeme(LexemeType.Number, "2"),
                    new Lexeme(LexemeType.ClosingBracket, ")"),
                    new Lexeme(LexemeType.Power, "^"),
                    new Lexeme(LexemeType.OpeningBracket, "("),
                    new Lexeme(LexemeType.Minus, "-"),
                    new Lexeme(LexemeType.Number, "30"),
                    new Lexeme(LexemeType.Divide, "/"),
                    new Lexeme(LexemeType.Number, "5"),
                    new Lexeme(LexemeType.ClosingBracket, ")"),
                }
            },

            new object[]
            {
                "4+(43/20)*(-383+(2^7))",
                new Lexeme[]
                {
                    new Lexeme(LexemeType.Number, "4"),
                    new Lexeme(LexemeType.Plus, "+"),
                    new Lexeme(LexemeType.OpeningBracket, "("),
                    new Lexeme(LexemeType.Number, "43"),
                    new Lexeme(LexemeType.Divide, "/"),
                    new Lexeme(LexemeType.Number, "20"),
                    new Lexeme(LexemeType.ClosingBracket, ")"),
                    new Lexeme(LexemeType.Multiply, "*"),
                    new Lexeme(LexemeType.OpeningBracket, "("),
                    new Lexeme(LexemeType.Minus, "-"),
                    new Lexeme(LexemeType.Number, "383"),
                    new Lexeme(LexemeType.Plus, "+"),
                    new Lexeme(LexemeType.OpeningBracket, "("),
                    new Lexeme(LexemeType.Number, "2"),
                    new Lexeme(LexemeType.Power, "^"),
                    new Lexeme(LexemeType.Number, "7"),
                    new Lexeme(LexemeType.ClosingBracket, ")"),
                    new Lexeme(LexemeType.ClosingBracket, ")"),
                }
            },
        };

        private static readonly object[][] WrongSequences_AllCharsAppropriate =
        {
            new object[]
            {
                "(2++2)/)())//1^+))111",
                new Lexeme[]
                {
                    new Lexeme(LexemeType.OpeningBracket, "("),
                    new Lexeme(LexemeType.Number, "2"),
                    new Lexeme(LexemeType.Plus, "+"),
                    new Lexeme(LexemeType.Plus, "+"),
                    new Lexeme(LexemeType.Number, "2"),
                    new Lexeme(LexemeType.ClosingBracket, ")"),
                    new Lexeme(LexemeType.Divide, "/"),
                    new Lexeme(LexemeType.ClosingBracket, ")"),
                    new Lexeme(LexemeType.OpeningBracket, "("),
                    new Lexeme(LexemeType.ClosingBracket, ")"),
                    new Lexeme(LexemeType.ClosingBracket, ")"),
                    new Lexeme(LexemeType.Divide, "/"),
                    new Lexeme(LexemeType.Divide, "/"),
                    new Lexeme(LexemeType.Number, "1"),
                    new Lexeme(LexemeType.Power, "^"),
                    new Lexeme(LexemeType.Plus, "+"),
                    new Lexeme(LexemeType.ClosingBracket, ")"),
                    new Lexeme(LexemeType.ClosingBracket, ")"),
                    new Lexeme(LexemeType.Number, "111")
                }
            }
        };

        private static readonly object[][] SequencesWithIllegalCharacters =
        {
            new object[]
            {
                "(2+2)>(-30/5)",
                '>'
            },

            new object[]
            {
                "4+(43/20)*(-383!+(2^7))",
                '!'
            },
        };

        [Test]
        [TestCaseSource(nameof(CorrectSequences))]
        public void ReadExpression_SuccessOnCorrect(string expression, Lexeme[] expected)
        {
            var reader = new StringReader(expression);
            var lexer = new Lexer(reader);

            var actual = lexer.EnumerateLexemes().ToArray();
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        [TestCaseSource(nameof(WrongSequences_AllCharsAppropriate))]
        public void ReadExpression_SuccessOnWrongWithAppropriateChars(string expression, Lexeme[] expected)
        {
            var reader = new StringReader(expression);
            var lexer = new Lexer(reader);

            var actual = lexer.EnumerateLexemes().ToArray();
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        [TestCaseSource(nameof(SequencesWithIllegalCharacters))]
        public void ReadExpression_ExceptionOnWrongCharacters(string expression, char expectedCharCaused)
        {
            var reader = new StringReader(expression);
            var lexer = new Lexer(reader);

            var exception = Assert.Throws<LexerException>(() => lexer.EnumerateLexemes().ToArray());

            Assert.AreEqual(exception.CharCaused, expectedCharCaused);
        }
    }
}