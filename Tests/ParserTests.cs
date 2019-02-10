using System;
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
        [TestCase("-0", 0)]
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
        [TestCase("2^7-3*5^2+4^13-3222/2*11+33^3-7*2^1",
            (1 << 7) - 3 * (5*5) + (1 << 26) - 3222 / 2 * 11 + (33*33*33) - 7 * 2)]
        public static void MultipleOperations_NoBrackets(string expression, int expectedResult)
        {
            Assert_ExpressionHasResult(expression, expectedResult);
        }

        [TestCase("(0)", 0)]
        [TestCase("(-129)", -129)]
        [TestCase("-(5001)", -5001)]
        [TestCase("-(100+3)", -103)]
        [TestCase("(-100+3)", -97)]
        [TestCase("(5-25)", -20)]
        [TestCase("(-5)*(-30)", 150)]
        [TestCase("(((100)))/5", 20)]
        [TestCase("-(1^0)", -1)]
        [TestCase("(-1)^0", 1)]
        [TestCase("-(-(-3))", -3)]
        public static void Brackets_Simple(string expression, int expectedResult)
        {
            Assert_ExpressionHasResult(expression, expectedResult);
        }

        [TestCase("4-(17-1^(9+0-9))*(16/8)", 4 - (17 - 1) * (16 / 8))]

        [TestCase("(15-0*14*4/(18*11*17))-((-60/15)+6-4/8-6+20)",
            (15 - 0 * 14 * 4 / (18 * 11 * 17)) - ((-60 / 15) + 6 - 4 / 8 - 6 + 20))]

        [TestCase("(8^(3)/((1800/14)/2)*11)", (8 * 8 * 8 / ((1800 / 14) / 2) * 11))]

        [TestCase("(11*122/9)-10^(8/8*8)-(4*8)", (11 * 122 / 9) - 100000000 - (4 * 8))]

        [TestCase("-11*13-15-6+2+5-1-3*12/14",
            -11 * 13 - 15 - 6 + 2 + 5 - 1 - 3 * 12 / 14)]

        [TestCase("(4^5-20^(-3*0*20))", ((1 << 10) - 1))]

        [TestCase("12*12/8*16*(-7-4)+(13+3)", 12 * 12 / 8 * 16 * (-7 - 4) + (13 + 3))]

        [TestCase("4*(13-4^6)", 4 * (13 - (1 << 12)))]

        [TestCase("14-2-18+(18*1)+(15*7/3)", 14 - 2 - 18 + (18 * 1) + (15 * 7 / 3))]

        [TestCase("(((19+9^3)-16/1)-9/3)", (((19 + 9 * 9 * 9) - 16 / 1) - 9 / 3))]

        [TestCase("-4-1^(10-2)-7+0+16", -4 - 1 - 7 + 0 + 16)]

        [TestCase("20+0/7/18-12-17^1-20/((9*4)-15+16*(6/6)-13/((13*17)/3))",
            20 + 0 / 7 / 18 - 12 - 17 - 20 / ((9 * 4) - 15 + 16 * (6 / 6) - 13 / ((13 * 17) / 3)))]

        [TestCase("(2-19-2+15*12+19)", (2 - 19 - 2 + 15 * 12 + 19))]

        [TestCase("-0*15*(6/9)+12*11", -0 * 15 * (6 / 9) + 12 * 11)]

        [TestCase("-1+9+7/1+(11+(13+4)*9)", -1 + 9 + 7 / 1 + (11 + (13 + 4) * 9))]

        [TestCase("((19/(15+0))-15+18*1)/(19-14*8-(12-11))*(-1)-5-18/6",
            ((19 / (15 + 0)) - 15 + 18 * 1) / (19 - 14 * 8 - (12 - 11)) * (-1) - 5 - 18 / 6)]

        [TestCase("((-19/19)+18+8-20)", ((-19 / 19) + 18 + 8 - 20))]

        [TestCase("-2-15-(15+19)+6*(0*3)", -2 - 15 - (15 + 19) + 6 * (0 * 3))]
        public static void Brackets_Complicated(string expression, int expectedResult)
        {
            Assert_ExpressionHasResult(expression, expectedResult);
        }


        [TestCase("     -721  / 18 * 16 -   8 / 4 -   16 / (16 /     15 *   18 * 16) ",
            -721 / 18 * 16 - 8 / 4 - 16 / (16 / 15 * 18 * 16))]

        [TestCase("16 / (1 ^ (7 / 2)) - 20 ", 16 - 20)]

        [TestCase("(17 *    6  * (18 / 2     /   4) + 3   / 10 + (20 - 7)) ",
            (17 * 6 * (18 / 2 / 4) + 3 / 10 + (20 - 7)))]

        [TestCase("4 + 2 * ((19 * 4) * 18 - 7 ^ 3 * (17 + 14 / 7)) ",
            4 + 2 * ((19 * 4) * 18 - 7 * 7 * 7 * (17 + 14 / 7)))]

        [TestCase("19 *   0 +  17 / (17        - 13) / 16 + 2 -     4 ",
            19 * 0 + 17 / (17 - 13) / 16 + 2 - 4)]

        [TestCase("(19 +18 -(-4 /   4)) / (15 + 13  ) ", (19 + 18 - (-4 / 4)) / (15 + 13))]

        [TestCase("0 + 12 /     4 -    (6 ^ 4)    - (15 * (18 - 10)/14) /20 * 1 ",
            0 + 12 / 4 - (36 * 36) - (15 * (18 - 10) / 14) / 20 * 1)]

        [TestCase("(0 - 18) + (4 - 19) *    9 - 2 * 9 + 10 * 20 * 12 ",
            (0 - 18) + (4 - 19) * 9 - 2 * 9 + 10 * 20 * 12)]

        [TestCase("(-12 *(13 + 5   ) - 360 / 18   / 1 -   3 * 20 -    11 + 13) ",
            (-12 * (13 + 5) - 360 / 18 / 1 - 3 * 20 - 11 + 13))]

        [TestCase("13 + 10*1 / (2 * 13 +    4) - ((3 + 6) * 14) ",
            13 + 10 * 1 / (2 * 13 + 4) - ((3 + 6) * 14))]

        [TestCase("12 +      17 +       7 ^     0 - 18 -     19 + 12 ", 12 + 17 + 1 - 18 - 19 + 12)]

        [TestCase("9 - (9 *0 + 17 - 8) / (16/(14 * 16) + (  (   13 -      20) - 7   )  ) ",
            9 - (9 * 0 + 17 - 8) / (16 / (14 * 16) + ((13 - 20) - 7)))]

        [TestCase("9 +   (  17 - 7   ) + 2 ", 9 + (17 - 7) + 2)]

        [TestCase("((0   / (6 -         18) *    (15 ^ 0)) - 17 -   13   /     10  / 6) ",
            ((0 / (6 - 18) * 1) - 17 - 13 / 10 / 6))]

        [TestCase("11 *      10 / 5 - 16 / (8   /   2) ", 11 * 10 / 5 - 16 / (8 / 2))]

        [TestCase("(    15    + 19     ) ^    2 + 19 * 20 -   5 ", (15 + 19) * (15 + 19) + 19 * 20 - 5)]

        [TestCase("     (9 - 9) - 4 ^ 11 - (3 * 17 * 4 - 3) ", (9 - 9) - (1 << 22) - (3 * 17 * 4 - 3))]

        [TestCase("     (-   8 + (16) - 12 / 1 / 12 ) ", (-8 + (16) - 12 / 1 / 12))]

        public static void Whitespace_Complicated(string expression, int expectedResult)
        {
            Assert_ExpressionHasResult(expression, expectedResult);
        }

        [TestCase("Hello world!", 'H')]
        [TestCase("(1+7)*(z-5)", 'z')]
        [TestCase("4/2<3", '<')]
        [TestCase("8+3.5", '.')]
        [TestCase("2*50%", '%')]
        public static void ExceptionOnIncorrect_InvalidCharacters(string expression, char expectedCharCaused)
        {
            var parserException = Assert.Throws<ParserException>(() => new Parser(expression).ParseExpression());

            Assert.AreEqual(expectedCharCaused, parserException.CharCaused);
            Assert.AreEqual(expression.IndexOf(expectedCharCaused), parserException.Position);

            Assert.That(parserException.InnerException, Is.TypeOf<LexerException>());
        }

        [TestCase("(0", char.MinValue)]
        [TestCase("--2", 'H')]
        [TestCase("+3-1", '+')]
        [TestCase(")", ')')]
        [TestCase("8*+7", '+')]
        public static void ExceptionOnIncorrect_InvalidCombinations(string expression, char expectedCharCaused)
        {
            var parserException = Assert.Throws<ParserException>(() => new Parser(expression).ParseExpression());

            Assert.AreEqual(expectedCharCaused, parserException.CharCaused);
            Assert.AreEqual(expression.IndexOf(expectedCharCaused), parserException.Position);

            Assert.That(parserException, Has.No.InnerException);
        }

        private static void Assert_ExpressionHasResult(string expression, int expectedResult)
        {
            var parser = new Parser(expression);
            Assert.AreEqual(expectedResult, actual: parser.ParseExpression());
        }
    }
}