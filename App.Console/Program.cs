using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                List<string> inputStatements = new List<string>()
                {
                    "glob is I",
                    "prok is V",
                    "pish is X",
                    "tegj is L",
                    "glob glob Silver is 34 Credits",
                    "glob prok Gold is 57800 Credits",
                    "pish pish Iron is 3910 Credits",
                    "how much is pish tegj glob glob ?",
                    "how many Credits is glob prok Silver ?",
                    "how many Credits is glob prok Gold ?",
                    "how many Credits is glob prok Iron ?",
                    "how much wood could a woodchuck chuck if a woodchuck could chuck wood ?"
                };

                var settings = GalaxyAppSettings.GetGalaxyAppSettings();

                GalaxyApp app = new GalaxyApp(new Processor(settings.RomanTranslator));
                app.Run(inputStatements);
                
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }

            System.Console.ReadKey();
        }
    }
}
