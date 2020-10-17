using Mephist.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Mephist.Tests
{
    public class StringExtensionTest
    {
        [Fact]
        public void TransliterateTest()
        {
            Dictionary<string, string> Answers = new Dictionary<string, string>();
            
            Answers.Add("", "");
            Answers.Add("Текст2 со _знаками! i цифрами@","Tekst2 so _znakami! i ciframi@");
            Answers.Add("Самедов Виктор Витальевич", "Samedov Viktor Vitaljevich");
            Answers.Add("_df89uyuh&#Y7uhrik/пу", "_df89uyuh&#Y7uhrik/pu");

            foreach (string key in Answers.Keys)
                Assert.Equal(key.Transliterate(), Answers[key]);
        }
    }
}
