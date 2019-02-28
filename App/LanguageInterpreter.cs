using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Concrete;
using Common.Custom;

namespace App
{
    public class LanguageInterpreter
    {
        #region Private Fields

        private Dictionary<string, string> _galaxyToRomanMap = new Dictionary<string, string>();
        private Dictionary<string, double> _perUnitCredits = new Dictionary<string, double>();
        private const char INPUT_STATEMENT_SPLIT_CHARACTER = ' ';
        private List<IStatementTypeDecider> _statementTypes = new List<IStatementTypeDecider>();
        private Processor _langaugeProcessor;

        #endregion

        #region Constructor

        public LanguageInterpreter(Processor langaugeProcessor)
        {
            _langaugeProcessor = langaugeProcessor;

            var statementInterfaceType = typeof(IStatementTypeDecider);
            _statementTypes.Add(new AssignmentStatementType());
            _statementTypes.Add(new CreditsAssignmentStatementType());
            _statementTypes.Add(new RomanQueryStatementType());
            _statementTypes.Add(new CreditsQueryStatementType());
        }


        #endregion

        #region Public Methods

        public Answer ParseStatements(string inputStatement)
        {
            Answer answer = null;

            IStatementTypeDecider statementTypeSelector = GetStatementTypeSelector(inputStatement);
            if (statementTypeSelector is null)
            {
                throw new StatementTypeNotFoundException();
            }
            else
            {
                Statement statement = StatementFactory.GetStatement(statementTypeSelector.StatementType, SplitTheInputStatements(CleanQueryString(inputStatement)));

                switch (statementTypeSelector.StatementType)
                {
                    case StatementType.CreditsQuery:
                        answer = _langaugeProcessor.Execute(StatementType.CreditsQuery, statement as CreditsQueryStatement);
                        break;

                    case StatementType.RomanQuery:
                        answer = _langaugeProcessor.Execute(StatementType.RomanQuery, statement as RomanQueryStatement);
                        break;

                    case StatementType.CreditsAssignment:
                        answer = _langaugeProcessor.Execute(StatementType.CreditsQuery, statement as CreditsAssignmentStatement);
                        break;

                    case StatementType.Assignment:
                        answer = _langaugeProcessor.Execute(StatementType.Assignment, statement as AssignmentStatement);
                        break;

                    case StatementType.NoIdea:
                    default:
                        break;
                }
            }
            return answer;
        }

        #endregion

        #region Private Methods

        private IStatementTypeDecider GetStatementTypeSelector(string inputStatement)
        {
            return _statementTypes.FirstOrDefault(s => s.isTrue(inputStatement));
        }

        private static string[] SplitTheInputStatements(string input)
        {
            return input.Split(INPUT_STATEMENT_SPLIT_CHARACTER);
        }
   
        private static string CleanQueryString(string input)
        {
            return input.Replace("?", string.Empty)
                        .Replace(Constants.QUESTION_STATEMENTS_WITHCREDITS, string.Empty)
                        .Replace(Constants.QUESTION_STATEMENTS_WITH_IS, string.Empty)
                        .Trim();
        }
        #endregion
    }
}
