using System.IO;
using System.Linq;
using ArithmeticExpressionsRecognizer;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class LexerTests
    {
        private static object[] sequences =
        {
            new object[] { "(2+2)*(-383+(2^7))", "(", "2", "+", "2", ")"},
        };

        [SetUp]
        public void Setup()
        {

        }

        [Test, TestCaseSource(nameof(sequences))]
        public void ReadExpression_CheckTexts(string expression, params Lexeme[] lexemes)
        {
            StringReader sr = new StringReader(expression);
            Lexer lexer = new Lexer(sr);

            CollectionAssert.AreEqual(lexemes,
                lexer.EnumerateLexemes().Select(lexeme => lexeme.Text));
        }

    }
}