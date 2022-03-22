using HaveFunWithDadJoke.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DadJokesFun.Tests
{
    public class APIHelper
    {
        [Fact]
        public void CreateUrl_Returns_Expected_Url_With_Default_Values()
        {
            var expected = "http://localhost:61478/api/dadjokes/search?term=forest&limit=30&page=1";

            var actual = new ApiHelper().CreateUrl("http://localhost:61478/api/dadjokes/search", "forest");

            Assert.True(expected == actual);
        }

        [Fact]
        public void CreateUrl_Returns_Expected_Url_With_Passed_Values()
        {
            var expected = "http://localhost:61478/api/dadjokes/search?term=forest&limit=20&page=1";

            var actual = new ApiHelper().CreateUrl("http://localhost:61478/api/dadjokes/search", "forest",20,1);

            Assert.True(expected == actual);
        }


    }
}
