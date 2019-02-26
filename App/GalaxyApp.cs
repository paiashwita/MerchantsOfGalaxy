using System.Collections.Generic;

namespace App
{
    public class GalaxyApp
    {
        private LanguageInterpreter _interpreter;

        public GalaxyApp(RomanTranslator romantranslator)
        {
            _interpreter = new LanguageInterpreter(romantranslator);
        }

        public void Run(List<string> inputStatements)
        {
            var answers =_interpreter.ParseStatements(inputStatements);
            foreach (var answer in answers)
            {
                System.Console.WriteLine(answer);
            }
        }

    }
}
