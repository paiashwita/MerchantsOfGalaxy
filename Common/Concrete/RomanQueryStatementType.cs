using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Concrete
{
    public class RomanQueryStatementType : IStatementTypeDecider
    {
        public StatementType StatementType { get => StatementType.RomanQuery; }
        public bool isTrue(string inputStatement)
        {
            return inputStatement.StartsWith(Constants.QUESTION_STATEMENTS_WITH_IS);
        }
    }
}
