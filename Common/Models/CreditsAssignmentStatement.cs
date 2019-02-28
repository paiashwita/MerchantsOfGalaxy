using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public class CreditsAssignmentStatement : Statement
    {
        public CreditsAssignmentStatement(string[] StatementWords) : base(StatementType.CreditsAssignment, StatementWords) { }
    }
}
