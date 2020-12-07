using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Mephist.Tests
{
    public class FuzzySearchTest
    {
        static int similarity = 50;

        [Fact]
        public void Light()
        {
            List<(string, string)> list = new List<(string, string)>();
            list.Add(("Самедов", "Самедов Виктор Витальевич"));
            list.Add(("Виктор витальевич", "Самедов Виктор Витальевич"));
            list.Add(("Самдедов", "Самедов Виктор Витальевич"));
            list.Add(("Самедоввиктор витальевич", "Самедов Виктор Витальевич"));
            list.Add(("Самедов итальевич", "Самедов Виктор Витальевич"));
            list.Add(("Виктор самедов ", "Самедов Виктор Витальевич"));


            foreach (var tuple in list)
                Assert.True(FuzzySharp.Fuzz.PartialRatio(tuple.Item1, tuple.Item2) >= similarity);
        }

        [Fact]
        public void Medium()
        {
            List<(string, string)> list = new List<(string, string)>();
            list.Add(("самедов киткор виталивч", "Самедов Виктор Витальевич"));
            list.Add(("Витальевибч Мамедов", "Самедов Виктор Витальевич"));
            list.Add(("Виктор С1амедов", "Самедов Виктор Витальевич"));
            list.Add(("Самедоввиктор витальевич", "Самедов Виктор Витальевич"));
            list.Add((" самдеав виктор ", "Самедов Виктор Витальевич"));

            foreach (var tuple in list)
                Assert.True(FuzzySharp.Fuzz.PartialRatio(tuple.Item1, tuple.Item2) >= similarity);
        }

        [Fact]
        public void Hard()
        {
            List<(string, string)> list = new List<(string, string)>();
            list.Add(("саМедОв вик4тор", "Самедов Виктор Витальевич"));
            list.Add(("викто3р со.мюедов", "Самедов Виктор Витальевич"));
            list.Add(("Самеюдов Ивктор Виюбталеьвич2", "Самедов Виктор Витальевич"));
            list.Add(("самдел виктор виатловчи", "Самедов Виктор Витальевич"));
            list.Add(("виктр самедов витаевч", "Самедов Виктор Витальевич"));

            foreach (var tuple in list)
                Assert.True(FuzzySharp.Fuzz.PartialRatio(tuple.Item1, tuple.Item2) >= similarity);
        }

        [Fact]
        public void Impossible()
        {
            List<(string, string)> list = new List<(string, string)>();
            list.Add(("сс3амелов ви.ктрп", "Самедов Виктор Витальевич"));
            list.Add(("витр вит4альевч", "Самедов Виктор Витальевич"));
            list.Add(("ввкт.ар вит6алиюч", "Самедов Виктор Витальевич"));
            list.Add(("с.амоевдов вита.ич в.иктл", "Самедов Виктор Витальевич"));


            foreach (var tuple in list)
                Assert.True(FuzzySharp.Fuzz.PartialRatio(tuple.Item1, tuple.Item2) >= similarity);
        }

        [Fact]
        public void Cant()
        {
            List<(string, string)> list = new List<(string, string)>();
            list.Add(("вснлдорва", "Самедов Виктор Витальевич"));
            list.Add(("ваикеапотьр", "Самедов Виктор Витальевич"));
            list.Add(("ви кеирогп ваитапольепвимч", "Самедов Виктор Витальевич"));
            


            foreach (var tuple in list)
                Assert.False(FuzzySharp.Fuzz.PartialRatio(tuple.Item1, tuple.Item2) >= similarity);
        }
    }
}
