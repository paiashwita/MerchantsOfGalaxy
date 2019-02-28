using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public class RomanQueryStatement : Statement
    {
        public RomanQueryStatement(string[] StatementWords) : base(StatementType.RomanQuery, StatementWords) { }
    }
}
