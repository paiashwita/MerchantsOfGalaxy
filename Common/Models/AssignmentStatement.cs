using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public class AssignmentStatement : Statement
    {
        public AssignmentStatement(string[] StatementWords) :base(StatementType.Assignment, StatementWords) { }
    }
}
