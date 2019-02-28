using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Concrete
{
    public class CreditsAssignmentStatementType : IStatementTypeDecider
    {
        public StatementType StatementType { get => StatementType.CreditsAssignment; }
        public bool isTrue(string inputStatement)
        {
            return inputStatement.Contains(Constants.ASSIGNMENT_STATEMENTS_IS) 
                && inputStatement.Contains(Constants.ASSIGNMENT_STATEMENTS_WITHCREDITS)
                && !inputStatement.Contains(Constants.QUESTION_STATEMENTS_WITHCREDITS)
                && !inputStatement.Contains(Constants.QUESTION_STATEMENTS_WITH_IS);
        }
    }
}
