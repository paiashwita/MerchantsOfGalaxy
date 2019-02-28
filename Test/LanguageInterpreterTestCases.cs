using App;
using Common;
using Common.Custom;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Test
{
    [TestClass]
    public class LanguageInterpreterTestCases
    {
        RomanTranslator romanTranslator;
        RomanNumbers romanNumbers;
        LanguageInterpreter interpreter;

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
            interpreter = new LanguageInterpreter(new Processor(romanTranslator));
        }

        [TestMethod]
        public void LanguageInterpreter_AssignmentStatement()
        {
            var expected = new Answer() { StatementType = StatementType.Assignment, AnswerText = "Assigned [glob] is [I]" };
            var actual = interpreter.ParseStatements("glob is I");
            Assert.AreEqual(actual.AnswerText, expected.AnswerText);
        }

        [TestMethod]
        public void LanguageInterpreter_CreditsAssignmentStatement()
        {
            var expected = new Answer() { StatementType = StatementType.Assignment, AnswerText = "Assigned [Gold] is [14450] per unit" };
            interpreter.ParseStatements("glob is I");
            interpreter.ParseStatements("prok is V");
            var actual = interpreter.ParseStatements("glob prok Gold is 57800 Credits");
            Assert.AreEqual(actual.AnswerText, expected.AnswerText);
        }

        [TestMethod]
        public void LanguageInterpreter_RomanQueryStatement()
        {
            var expected = new Answer() { StatementType = StatementType.Assignment, AnswerText = "pish tegj glob glob is 42" };
            interpreter.ParseStatements("glob is I");
            interpreter.ParseStatements("prok is V");
            interpreter.ParseStatements("pish is X");
            interpreter.ParseStatements("tegj is L");
            var actual = interpreter.ParseStatements("how much is pish tegj glob glob ?");
            Assert.AreEqual(actual.AnswerText, expected.AnswerText);
        }

        [TestMethod]
        public void LanguageInterpreter_CreditsQueryStatement()
        {
            var expected = new Answer() { StatementType = StatementType.Assignment, AnswerText = "glob prok Iron is 782 Credits" };
            interpreter.ParseStatements("glob is I");
            interpreter.ParseStatements("prok is V");
            interpreter.ParseStatements("pish is X");
            interpreter.ParseStatements("tegj is L");
            interpreter.ParseStatements("pish pish Iron is 3910 Credits");
            var actual = interpreter.ParseStatements("how many Credits is glob prok Iron ?");
            Assert.AreEqual(actual.AnswerText, expected.AnswerText);
        }

        [TestMethod]
        public void StatementTypeNotFoundException()
        {
            Assert.ThrowsException<StatementTypeNotFoundException>(() => interpreter.ParseStatements("how much wood could a woodchuck chuck if a woodchuck could chuck wood ?"));
        }
    }
}
