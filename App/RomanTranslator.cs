using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App
{
    public class RomanTranslator
    {
        private readonly RomanNumbers _romanNumbers;
        private const int MAX_REPETABLE_CHARACTER_COUNT = 3;
        private const int DIFFERENCE_IN_ORDINAL_OF_CHARACTERS = 3;

        public RomanTranslator(RomanNumbers romanNumbers)
        {
            _romanNumbers = romanNumbers;
        }

        public bool ValidateWord(string word)
        {
            bool result = true;

            char current = ' ', previous = ' ';
            int character_repeat_count = 0;
            int? current_ordinal = null, previous_ordinal = null;
            for (int i = 0; i < word.Length; i++)
            {
                current = word[i];

                /*Repeating characters*/
                if (!_romanNumbers.isCharacterValid(current))
                {
                    result = false;
                }
                else if (current == previous)
                {
                    /*Some characters cannot be repeated*/
                    if (!_romanNumbers.isRepeatable(current))
                    {
                        result = false;
                    }
                    /*can be repeated finite number of times*/
                    else if (_romanNumbers.isRepeatable(current) && character_repeat_count == MAX_REPETABLE_CHARACTER_COUNT)
                    {
                        result = false;
                    }
                    character_repeat_count++;
                }
                /*Precedence of the characters to be checked - when characters are different*/
                else if (previous != ' ' && current != previous)
                {
                    current_ordinal = _romanNumbers.GetOrdinal(current);
                    previous_ordinal = _romanNumbers.GetOrdinal(previous);

                    if (character_repeat_count > 1 && previous_ordinal < current_ordinal)
                    {
                        result = false;
                    }
                    else if (current_ordinal - previous_ordinal > DIFFERENCE_IN_ORDINAL_OF_CHARACTERS)
                    {
                        result = false;
                    }
                    else
                    {
                        character_repeat_count = 1;
                    }
                }
                else
                {
                    character_repeat_count = 1;
                }

                if (!result)
                {
                    break;
                }

                previous = current;
            }

            return result;
        }

        public int CalculateWordValue(string word)
        {
            int result = 0;
            int current_ordinal = 0, previous_ordinal = 0, current_translated_value = 0, previous_translated_value=0;

            result = (int)_romanNumbers.GetTranslatedCharacterValue(word[0]);
            for (int counter = 1; counter < word.Length; counter++)
            {
                current_ordinal = (int)_romanNumbers.GetOrdinal(word[counter]);
                previous_ordinal = (int)_romanNumbers.GetOrdinal(word[counter - 1]);
                current_translated_value = (int)_romanNumbers.GetTranslatedCharacterValue(word[counter]);
                previous_translated_value = (int)_romanNumbers.GetTranslatedCharacterValue(word[counter-1]);

                if (current_ordinal> previous_ordinal)
                {
                    result = result - previous_translated_value + (current_translated_value - previous_translated_value);
                }
                else
                {
                    result += current_translated_value;
                }
            }

            return result;
        }
    }
}
