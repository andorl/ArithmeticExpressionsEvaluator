using ArithmeticExpressionsRecognizer;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public static class ParserTests
    {
        [TestCase("2", 2)]
        [TestCase("400", 400)]
        [TestCase("-28", -28)]
        [TestCase("0", 0)]
        [TestCase("134560910", 134560910)]
        public static void Constants(string expression, int expectedResult)
        {
            Assert_ExpressionHasResult(expression, expectedResult);
        }

        [TestCase("-2+1", -1)]
        [TestCase("400-398", 2)]
        [TestCase("4*9", 36)]
        [TestCase("72/9", 8)]
        [TestCase("2^7", 128)]
        public static void SingleOperation(string expression, int expectedResult)
        {
            Assert_ExpressionHasResult(expression, expectedResult);
        }

        [TestCase("1+2+3+4+5+6", 1 + 2 + 3 + 4 + 5 + 6)]
        [TestCase("1-3-5-7-9-11", 1 - 3 - 5 - 7 - 9 - 11)]
        [TestCase("11-23+490-0+0-28", 11 - 23 + 490 - 0 + 0 - 28)]
        [TestCase("1000000000/5/2/100/4/10", 1000000000 / 5 / 2 / 100 / 4 / 10)]
        [TestCase("-10+11*78/2+35*2*7-89*90+23-45*19/7+8",
            -10 + 11 * 78 / 2 + 35 * 2 * 7 - 89 * 90 + 23 - 45 * 19 / 7 + 8)]
        public static void MultipleOperations_NoBrackets(string expression, int expectedResult)
        {
            Assert_ExpressionHasResult(expression, expectedResult);
        }

        [TestCase("(0)", 0)]
        [TestCase("(-129)", -129)]
        [TestCase("-(5001)", -5001)]
        public static void Brackets(string expression, int expectedResult)
        {
            Assert_ExpressionHasResult(expression, expectedResult);
        }


        private static void Assert_ExpressionHasResult(string expression, int expectedResult)
        {
            var parser = new Parser(expression);
            Assert.AreEqual(expectedResult, actual: parser.ParseExpression());
        }
    }
}