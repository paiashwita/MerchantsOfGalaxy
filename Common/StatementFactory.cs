using Common.Custom;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public class StatementFactory
    {
        public static Statement GetStatement(StatementType statementType, string[] statementWords)
        {
            Statement statement = null;
            switch (statementType)
            {
                case StatementType.Assignment:
                    statement = new AssignmentStatement(statementWords);
                    break;
                case StatementType.CreditsQuery:
                    statement = new CreditsQueryStatement(statementWords);
                    break;
                case StatementType.RomanQuery:
                    statement = new RomanQueryStatement(statementWords);
                    break;
                case StatementType.CreditsAssignment:
                    statement = new CreditsAssignmentStatement(statementWords);
                    break;
                case StatementType.NoIdea: throw new StatementTypeNotFoundException(string.Join(" ", statementWords));
                default:
                    break;
            }

            return statement;
        }
    }
}
