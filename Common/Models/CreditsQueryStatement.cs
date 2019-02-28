using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public class CreditsQueryStatement : Statement
    {
        public CreditsQueryStatement(string[] StatementWords) : base(StatementType.CreditsQuery, StatementWords) { }
    }
}
