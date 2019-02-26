using System;
using System.Collections.Generic;
using System.Text;

namespace App
{
    public class GalaxyAppSettings
    {
        private static GalaxyAppSettings _appSettings;

        public  RomanTranslator RomanTranslator { get; set; }
        public  RomanNumbers RomanNumbers { get; set; }

        private GalaxyAppSettings() { }

        public static GalaxyAppSettings GetGalaxyAppSettings()
        {
            if (_appSettings ==null)
            {
                var romanNumbersDictionay = new Dictionary<char, int>();
                romanNumbersDictionay.Add('I', 1);
                romanNumbersDictionay.Add('V', 5);
                romanNumbersDictionay.Add('X', 10);
                romanNumbersDictionay.Add('L', 50);
                romanNumbersDictionay.Add('C', 100);
                romanNumbersDictionay.Add('D', 500);
                romanNumbersDictionay.Add('M', 1000);

                var romanNumbers = new RomanNumbers(romanNumbersDictionay, new char[] { 'I', 'X', 'C', 'M' });

                _appSettings = new GalaxyAppSettings()
                {
                    RomanNumbers = romanNumbers,
                    RomanTranslator = new RomanTranslator(romanNumbers)
                };
            }
            return _appSettings;
        }
    }
}
