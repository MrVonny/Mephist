using Mephist.Algorithms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Mephist.Tests
{
    public class ParserTest
    {
        [Theory]
        [InlineData("биохимия", "Биохимия")]
        [InlineData("общая физика", "Общая физика")]
        [InlineData("Общая физика", "Общая физика")]
        [InlineData("1Общая физика", "1Общая физика")]
        [InlineData("  Общая физика", "Общая физика")]
        public void SubjectNormalaize_FirstLetter(string input, string ouput)
        {
            Assert.Equal(ouput, EmployeeParser.NormalizeSubject(input));
        }

        [Theory]
        [InlineData(" Общая физика(механика)", "Общая физика (механика)")]
        [InlineData("Общая физика ( механика)", "Общая физика (механика)")]
        [InlineData("   Общая физика ( механика )", "Общая физика (механика)")]
        [InlineData("Общая физика  ( механика   )", "Общая физика (механика)")]
        [InlineData("Общая физика (механика) ", "Общая физика (механика)")]
        [InlineData("Общая физика  (    механика   ) ", "Общая физика (механика)")]
        [InlineData("  Общая   физика   ( механика   )   ", "Общая физика (механика)")]
        [InlineData("общая физика(механика)", "Общая физика (механика)")]
        [InlineData("общая физика ( механика)", "Общая физика (механика)")]
        [InlineData(" \t  общая физика ( механика )", "Общая физика (механика)")]
        [InlineData("\tобщая физика\t  \t( механика    \t  )", "Общая физика (механика)")]
        [InlineData("  общая физика (   механика\t \t)\t  \t", "Общая физика (механика)")]
        [InlineData("общая физика  ( \t   механика  \t ) ", "Общая физика (механика)")]
        [InlineData("  Общая   \t  физика \t  ( механика   )   ", "Общая физика (механика)")]
        [InlineData("\t 1общая     физика", "1общая физика")]
        public void SubjectNormalaize_All(string input, string ouput)
        {
            Assert.Equal(ouput,EmployeeParser.NormalizeSubject(input));
        }


    }
}
