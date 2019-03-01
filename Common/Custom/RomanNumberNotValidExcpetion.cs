using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Custom
{
    public class RomanNumberNotValidExcpetion : Exception
    {
        public RomanNumberNotValidExcpetion() : base(Constants.ROMAN_NUMBER_NOT_VALID)
        {

        }

        public RomanNumberNotValidExcpetion(string message)
            : base(message)

        {

        }
    }
}
