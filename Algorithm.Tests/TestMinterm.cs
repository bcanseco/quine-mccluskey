using System;
using Algorithm.Minterms;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Algorithm.Tests
{
    [TestClass]
    public class TestMinterm
    {
        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void Should_Throw_Exception_With_Duplicate_Variables()
        {
            var invalidMinterm = new Minterm("A * A");
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void Should_Throw_Exception_With_Unknown_Symbol()
        {
            var invalidMinterm = new Minterm("A ^ B");
        }

        [TestMethod]
        public void Should_Create_Instance_With_Valid_Input()
        {
            var mintermWithSpacing = new Minterm("A * B");
            var mintermWithoutSpacing = new Minterm("A*B");
            var mintermWithoutStar = new Minterm("AB");

            Assert.AreEqual(mintermWithSpacing, mintermWithoutSpacing);
            Assert.AreEqual(mintermWithoutSpacing, mintermWithoutStar);
        }

        [TestMethod]
        public void Should_Allow_Arbitrary_Number_Of_Bangs()
        {
            var normalMinterm = new Minterm("A * B");
            var doubleBangMinterm = new Minterm("A * !!B");

            Assert.AreEqual(normalMinterm, doubleBangMinterm);
        }

        [TestMethod]
        public void Should_Show_Correct_Number_Of_Ones()
        {
            var one = new Minterm("A");
            var two = new Minterm("B * C");
            var three = new Minterm("A * B * !C * D");

            Assert.IsTrue(one.NumberOfOnes == 1);
            Assert.IsTrue(two.NumberOfOnes == 2);
            Assert.IsTrue(three.NumberOfOnes == 3);
        }

        [TestMethod]
        public void Should_Show_Correct_Binary_Representation()
        {
            var alternating = new Minterm("A * !B * !!C * !!!D");
            Assert.IsTrue(alternating.InBinary == "1010");
        }

        [TestMethod]
        public void Should_Maintain_Correct_Raw_Form()
        {
            var noSpacing = new Minterm("d*e*f");
            Assert.IsTrue(noSpacing.Raw() == "d * e * f");
        }

        [TestMethod]
        public void Should_Implement_IEquatable()
        {
            var mintermOne = new Minterm("x * !y * z");
            var mintermUno = new Minterm("x * !y * z");

            Assert.AreEqual(mintermOne, mintermUno);
        }
    }
}
