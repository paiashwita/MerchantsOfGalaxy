using System;
using System.Collections.Generic;
using App;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MerchantsOfGalaxy
{
    [TestClass]
    public class RomanValidatorTestCases
    {
        RomanTranslator romanTranslator;
        RomanNumbers romanNumbers;

        [TestInitialize]
        public void TestInitialize()
        {
            var romanNumbersDictionay = new Dictionary<char, int>();
            romanNumbersDictionay.Add('I', 1);
            romanNumbersDictionay.Add('V', 5);
            romanNumbersDictionay.Add('X', 10);
            romanNumbersDictionay.Add('L', 50);
            romanNumbersDictionay.Add('C', 100);
            romanNumbersDictionay.Add('D', 500);
            romanNumbersDictionay.Add('M', 1000);

            romanNumbers = new RomanNumbers(romanNumbersDictionay, new char[] { 'I', 'X', 'C', 'M' });
            romanTranslator = new RomanTranslator(romanNumbers);

        }

        [TestMethod]
        public void RomanTranslator_ValidateRomanNumber_VALID_1()
        {
            Assert.IsTrue(romanTranslator.ValidateWord("MMVI"));
        }

        [TestMethod]
        public void RomanTranslator_ValidateRomanNumber_VALID_2()
        {
            Assert.IsTrue(romanTranslator.ValidateWord("MCMXLIV"));
        }

        [TestMethod]
        public void RomanTranslator_ValidateRomanNumber_INVALID()
        {
            Assert.IsFalse(romanTranslator.ValidateWord("VIDM"));
        }

        [TestMethod]
        public void RomanTranslator_Calculate_RomanNumber_VALID_1()
        {
            int actual, expected;
            actual = romanTranslator.CalculateWordValue("MMVI");
            expected = 2006;
            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void RomanTranslator_Calculate_RomanNumber_VALID_2()
        {
            int actual, expected;
            actual = romanTranslator.CalculateWordValue("MCMIII");
            expected = 1903;
            Assert.AreEqual(actual, expected);
        }
    }
}
