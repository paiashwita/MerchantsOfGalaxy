using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App
{
    public class RomanNumbers
    {
        private readonly Dictionary<char, int> _allCharacters;
        private readonly char[] _repeatableCharacters;

        public RomanNumbers(Dictionary<char, int> allCharacters, char[] repeatableCharacters)
        {
            _allCharacters = allCharacters;
            _repeatableCharacters = repeatableCharacters;
        }

        public int? GetOrdinal(char character)
        {
            bool found = false;
            int count = 1;
            foreach (var item in _allCharacters)
            {
                if (character == item.Key)
                {
                    found = true;
                    break;
                }
                count++;
            }
            return found ? count : 0;
        }

        public bool isCharacterValid(char character)
        {
            int translatedValue = 0;
            return _allCharacters.TryGetValue(character, out translatedValue);
        }

        public bool isRepeatable(char current)
        {
            return _repeatableCharacters.Contains(current);
        }

        public int? GetTranslatedCharacterValue(char character)
        {
            int translatedValue = 0;
            return _allCharacters.TryGetValue(character, out translatedValue) ? translatedValue : (int?)null;
        }
    }
}
