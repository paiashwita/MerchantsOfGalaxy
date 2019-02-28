﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Concrete
{
    public class AssignmentStatementType : IStatementTypeDecider
    {
        public StatementType StatementType { get => StatementType.Assignment;}

        public bool isTrue(string inputStatement)
        {
            return (inputStatement.Contains(Constants.ASSIGNMENT_STATEMENTS_IS) 
                   && !inputStatement.Contains(Constants.ASSIGNMENT_STATEMENTS_WITHCREDITS)
                   && !inputStatement.Contains(Constants.QUESTION_STATEMENTS_WITHCREDITS)
                   && !inputStatement.Contains(Constants.QUESTION_STATEMENTS_WITH_IS));
        }
    }
}
