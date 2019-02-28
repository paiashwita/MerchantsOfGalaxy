using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Concrete
{
    public class CreditsQueryStatementType : IStatementTypeDecider
    {
        public StatementType StatementType { get => StatementType.CreditsQuery; }
        public bool isTrue(string inputStatement)
        {
            return inputStatement.StartsWith(Constants.QUESTION_STATEMENTS_WITHCREDITS);
        }
    }
}
