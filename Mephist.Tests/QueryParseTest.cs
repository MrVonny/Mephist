using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xunit;

namespace Mephist.Tests
{
    public class QueryParseTest
    {
        [Fact]
        public void Test1()
        {
            var query = "0,423.png,0,025433.png,0,3.png,0,64523.txt";
            Regex.Matches(query, @"\,?(.*\.[^\,]*)\,?").Select(m =>
            {
                return m.Groups[1].Name;
            }).ToList();

            var str = "";
        }
    }
}
