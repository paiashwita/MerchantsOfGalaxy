using Common;
using Common.Custom;
using System.Collections.Generic;

namespace App
{
    public class GalaxyApp
    {
        private LanguageInterpreter _interpreter;

        public GalaxyApp(Processor processor)
        {
            _interpreter = new LanguageInterpreter(processor);
        }

        public void Run(List<string> inputStatements)
        {
            foreach (var inputStatement in inputStatements)
            {
                try
                {
                    var currentAnswer = _interpreter.ParseStatements(inputStatement);
                    System.Console.WriteLine(currentAnswer.AnswerText);
                }
                catch (StatementTypeNotFoundException ex)
                {
                    System.Console.WriteLine(ex.Message);
                }
                catch (System.Exception ex)
                {
                    System.Console.WriteLine(ex.Message);
                }
                
            }
            
        }

    }
}
