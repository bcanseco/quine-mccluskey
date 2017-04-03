using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Algorithm.Tests
{
    [TestClass]
    public class TestExpression
    {
        [TestMethod]
        public async Task Should_Have_No_Affect_On_Primes()
        {
            var prime1 = new Expression("A");
            var prime2 = new Expression("A * B");
            var prime3 = new Expression("A + B");

            var prime1Simplified =  prime1.Simplify();
            var prime2Simplified =  prime2.Simplify();
            var prime3Simplified =  prime3.Simplify();

            Assert.AreEqual(prime1, await prime1Simplified);
            Assert.AreEqual(prime2, await prime2Simplified);
            Assert.AreEqual(prime3, await prime3Simplified);
        }

        [TestMethod]
        public async Task Should_Successfully_Simplify_Complex_Input()
        {
            const string expression1 = "A * B * C + A * !B * C";
            const string expression2 = "A * B * C + A * !B * C + A * B * !C";
            const string expression3 = "!A!B!C!D + !A!B!CD + !AB!C!D + !AB!CD";
            const string expression4 = expression3 + " + !ABCD + A!BCD";

            var simplified1 = new Expression(expression1).Simplify();
            var simplified2 = new Expression(expression2).Simplify();
            var simplified3 = new Expression(expression3).Simplify();
            var simplified4 = new Expression(expression4).Simplify();

            Assert.AreEqual($"{await simplified1}", "A * C");
            Assert.AreEqual($"{await simplified2}", "A * C + A * B");
            Assert.AreEqual($"{await simplified3}", "!A * !C");
            Assert.AreEqual($"{await simplified4}", "A * !B * C * D + !A * B * D + !A * !C");
        }
    }
}
