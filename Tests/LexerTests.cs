using System.IO;
using System.Linq;
using ArithmeticExpressionsRecognizer;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class LexerTests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void Test1()
        {
            StringReader sr = new StringReader("(2+2)");
            Lexer lexer = new Lexer(sr);

            CollectionAssert.AreEquivalent(new[]{"(", "2", "+", "2", ")" }, lexer.EnumerateLexemes().Select(lexeme => lexeme.Text));
        }
    }
}