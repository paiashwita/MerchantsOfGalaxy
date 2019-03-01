using Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Common.Custom;

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

            if (!IsRomanNumberValid(romanWord))
            {
                throw new RomanNumberNotValidExcpetion();
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
            int i, credits = 0;
            string romanValue = string.Empty, metal = string.Empty;
            double perUnit;

            for (i = 0; i < statement.StatementWords.Length; i++)
            {
                if (!_galaxyToRomanMap.TryGetValue(statement.StatementWords[i], out romanValue))
                {
                    break;
                }
            }

            metal = statement.StatementWords[i];
            credits = Int32.Parse(statement.StatementWords[i + 2]);
            perUnit = (double)credits / CalculateRomanValue(statement.StatementWords);
            _perUnitCredits.Add(metal, perUnit);

            return new Answer()
            {
                StatementType = statementType,
                AnswerText = string.Format("Assigned [{0}] is [{1}] per unit", metal, perUnit)
            };
        }

        public Answer Execute(StatementType statementType ,RomanQueryStatement statement)
        {
            return new Answer()
            {
                StatementType =statementType,
                AnswerText = string.Format("{0} is {1}", string.Join(" ", statement.StatementWords), CalculateRomanValue(statement.StatementWords))
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
            string romanValue = string.Empty;

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
            if (IsRomanNumberValid(romanValue))
            {
                romanTranslateValue = _romantranslator.CalculateWordValue(romanValue);
                if (romanTranslateValue == -1)
                {
                    throw new RomanNumberNotValidExcpetion();
                }
            }
            else
            {
                throw new RomanNumberNotValidExcpetion();
            }
            return romanTranslateValue;
        }

        private bool IsRomanNumberValid(string romanValue)
        {
            return _romantranslator.ValidateWord(romanValue);
        }
    }
}
