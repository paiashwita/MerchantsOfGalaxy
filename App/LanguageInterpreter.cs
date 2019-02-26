using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App
{
    public class LanguageInterpreter
    {
        #region Private Fields

        private readonly RomanTranslator _romantranslator;
        private Dictionary<string, string> _galaxyToRomanMap = new Dictionary<string, string>();
        private Dictionary<string, double> _perUnitCredits = new Dictionary<string, double>();

        private const char INPUT_STATEMENT_SPLIT_CHARACTER = ' ';

        #endregion

        #region Constructor

        public LanguageInterpreter(RomanTranslator romantranslator)
        {
            _romantranslator = romantranslator;
        }

        #endregion

        #region Public Methods

        public List<string> ParseStatements(List<string> inputStatements)
        {
            string currentQuestion, currentAnswer;
            List<string> answers = new List<string>();

            foreach (var statement in inputStatements)
            {
                currentQuestion = string.Empty;
                currentAnswer = "NOT CALCULATED";

                if (statement.StartsWith(Constants.QUESTION_STATEMENTS_WITH_IS))
                {
                    currentQuestion = statement.Replace(Constants.QUESTION_STATEMENTS_WITH_IS, string.Empty).Replace("?", string.Empty).Trim();
                    currentAnswer = CalculateRomanValue(SplitTheInputStatements(currentQuestion, INPUT_STATEMENT_SPLIT_CHARACTER)).ToString();

                    answers.Add(string.Format("{0} is {1}", currentQuestion, currentAnswer));
                }
                else if (statement.StartsWith(Constants.QUESTION_STATEMENTS_WITHCREDITS))
                {
                    currentQuestion = statement.Replace(Constants.QUESTION_STATEMENTS_WITHCREDITS, string.Empty).Replace("?", string.Empty).Trim();
                    currentAnswer = CalculateCredits(SplitTheInputStatements(currentQuestion, INPUT_STATEMENT_SPLIT_CHARACTER)).ToString();

                    answers.Add(string.Format("{0} is {1} Credits", currentQuestion, currentAnswer));
                }
                else if (statement.Contains(Constants.ASSIGNMENT_STATEMENTS_IS) && !statement.Contains(Constants.ASSIGNMENT_STATEMENTS_WITHCREDITS))
                {
                    GenerateGalaxyToRomanMap(SplitTheInputStatements(statement, INPUT_STATEMENT_SPLIT_CHARACTER));
                }
                else if (statement.Contains(Constants.ASSIGNMENT_STATEMENTS_IS) && statement.Contains(Constants.ASSIGNMENT_STATEMENTS_WITHCREDITS))
                {
                    GeneratePerUnitCreditMeasure(SplitTheInputStatements(statement, INPUT_STATEMENT_SPLIT_CHARACTER));
                }
                else
                {
                    answers.Add(Constants.NO_IDEA);
                }
            }
            return answers;
        }

        #endregion

        #region Private Methods

        private int CalculateRomanValue(string[] input)
        {
            StringBuilder romanNumberBuilder = new StringBuilder();
            int romanTranslateValue = -1;
            string romanValue = string.Empty, metal = string.Empty;

            for (int i = 0; i < input.Length; i++)
            {
                if (_galaxyToRomanMap.TryGetValue(input[i], out romanValue))
                {
                    romanNumberBuilder.Append(romanValue);
                }
                else
                {
                    break;
                }
            }
            romanValue = romanNumberBuilder.ToString();
            if (_romantranslator.ValidateWord(romanValue))
            {
                romanTranslateValue = _romantranslator.CalculateWordValue(romanValue);
            }

            return romanTranslateValue;
        }

        private double CalculateCredits(string[] input)
        {
            int i, romanTranslateValue = -1;
            double perUnitValue = 0, credits = 0;
            string romanValue = string.Empty, metal = string.Empty;

            romanTranslateValue = CalculateRomanValue(input);

            for (i = 0; i < input.Length; i++)
            {
                if (!_galaxyToRomanMap.TryGetValue(input[i], out romanValue))
                {
                    break;
                }
            }

            metal = input[i];

            if (_perUnitCredits.TryGetValue(metal, out perUnitValue))
            {
                credits = perUnitValue * romanTranslateValue;
            }

            return credits;
        }

        private void GenerateGalaxyToRomanMap(string[] input)
        {
            var galaxyWord = input[0];
            var romanWord = input[2];

            if (!_romantranslator.ValidateWord(romanWord))
            {
                System.Console.WriteLine("This roman number is not supported.");
            }
            else
            {
                _galaxyToRomanMap.Add(galaxyWord, romanWord);
            }
        }

        private void GeneratePerUnitCreditMeasure(string[] input)
        {

            StringBuilder romanNumberBuilder = new StringBuilder();
            int i, romanTranslateValue = -1, credits = 0;
            string romanValue = string.Empty, metal = string.Empty;

            for (i = 0; i < input.Length; i++)
            {
                if (_galaxyToRomanMap.TryGetValue(input[i], out romanValue))
                {
                    romanNumberBuilder.Append(romanValue);
                }
                else
                {
                    break;
                }
            }
            romanValue = romanNumberBuilder.ToString();
            if (_romantranslator.ValidateWord(romanValue))
            {
                romanTranslateValue = _romantranslator.CalculateWordValue(romanValue);
            }

            metal = input[i];
            credits = Int32.Parse(input[i + 2]);
            _perUnitCredits.Add(metal, (double)credits / romanTranslateValue);
        }

        private string[] SplitTheInputStatements(string input, char splitCharacter)
        {
            return input.Split(splitCharacter);
        }

        #endregion
    }
}
