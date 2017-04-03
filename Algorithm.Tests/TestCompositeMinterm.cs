using System.Threading.Tasks;
using Algorithm.Minterms;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Algorithm.Tests
{
    [TestClass]
    public class TestCompositeMinterm
    {
        [TestMethod]
        public async Task Should_Fail_To_Combine_With_Different_Lengths()
        {
            var shortMinterm = new Minterm("A");
            var longMinterm = new Minterm("A * B");

            var canCombine = await shortMinterm.TryCombineWith(longMinterm, out MintermBase value);

            Assert.IsFalse(canCombine);
            Assert.IsNull(value);
        }

        [TestMethod]
        public async Task Should_Fail_To_Combine_With_Equal_Minterms()
        {
            var minterm = new Minterm("A * B");

            var canCombine = await minterm.TryCombineWith(minterm, out MintermBase value);

            Assert.IsFalse(canCombine);
            Assert.IsNull(value);
        }

        [TestMethod]
        public async Task Should_Fail_To_Combine_With_More_Than_One_Bit_Different()
        {
            var negative = new Minterm("!A * !B");
            var positive = new Minterm("A * B");

            var canCombine = await negative.TryCombineWith(positive, out MintermBase value);

            Assert.IsFalse(canCombine);
            Assert.IsNull(value);
        }

        [TestMethod]
        public async Task Should_Successfully_Combine_With_Only_One_Bit_Different()
        {
            var negative = new Minterm("A * !B");
            var positive = new Minterm("A * B");

            var canCombine = await negative.TryCombineWith(positive, out MintermBase value);

            Assert.IsTrue(canCombine);
            Assert.IsNotNull(value);
        }

        [TestMethod]
        public async Task Should_Match_Domains_If_Dashes_Line_Up()
        {
            var positiveNotB = new Minterm("A * !B * C");
            var positiveB = new Minterm("A * B * C");

            var negativeNotB = new Minterm("!A * !B * !C");
            var negativeB = new Minterm("!A * B * !C");

            await positiveNotB.TryCombineWith(positiveB, out MintermBase composite1);
            await negativeNotB.TryCombineWith(negativeB, out MintermBase composite2);

            // The middle bits are dashes since B has no effect on either composite.
            Assert.IsTrue(composite1.MatchesDomain(composite2));
        }
    }
}
