using Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace App
{
    public class Processor
    {
        private readonly RomanTranslator _romantranslator;
        private Dictionary<string, string> _galaxyToRomanMap = new Dictionary<string, string>();
        private Dictionary<string, double> _perUnitCredits = new Dictionary<string, double>();

        public Processor(RomanTranslator romantranslator)
        {
            _romantranslator = romantranslator;
        }

        public Answer Execute(StatementType statementType, AssignmentStatement statement)
        {
            var galaxyWord = statement.StatementWords[0];
            var romanWord = statement.StatementWords[2];

            if (!_romantranslator.ValidateWord(romanWord))
            {
                System.Console.WriteLine("This roman number is not supported.");
            }
            else
            {
                _galaxyToRomanMap.Add(galaxyWord, romanWord);
            }

            return new Answer()
            {
                StatementType = statementType,
                AnswerText = string.Format("Assigned [{0}] is [{1}]", galaxyWord, romanWord)
            };
        }

        public Answer Execute(StatementType statementType, CreditsAssignmentStatement statement)
        {
            StringBuilder romanNumberBuilder = new StringBuilder();
            int i, romanTranslateValue = -1, credits = 0;
            string romanValue = string.Empty, metal = string.Empty;
            string[] input = statement.StatementWords;

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

            metal = statement.StatementWords[i];
            credits = Int32.Parse(statement.StatementWords[i + 2]);
            var perUnit = (double)credits / romanTranslateValue;
            _perUnitCredits.Add(metal, perUnit);

            return new Answer()
            {
                StatementType = statementType,
                AnswerText = string.Format("Assigned [{0}] is [{1}] per unit", metal, perUnit)
            };
        }

        public Answer Execute(StatementType statementType ,RomanQueryStatement statement)
        {
            StringBuilder romanNumberBuilder = new StringBuilder();
            int romanTranslateValue = -1;
            string romanValue = string.Empty, metal = string.Empty;
            string[] input = statement.StatementWords;

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

            return new Answer()
            {
                StatementType =statementType,
                AnswerText = string.Format("{0} is {1}", string.Join(" ", input), romanTranslateValue)
            };
        }

        public Answer Execute(StatementType statementType, CreditsQueryStatement statement)
        {
            int i, romanTranslateValue = -1;
            double perUnitValue = 0, credits = 0;
            string romanValue = string.Empty, metal = string.Empty;
            string[] input = statement.StatementWords;

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

            return new Answer()
            {
                StatementType = statementType,
                AnswerText = string.Format("{0} is {1} Credits", string.Join(" ",input), credits)
            };
        }
        
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
    }
}
