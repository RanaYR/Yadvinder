using DadJokeHandler.Helper;
using DadJokeHandler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DadHokeHandler.Tests
{
    public class Handler
    {
        JokesSearchResponse response;

        public Handler()
        {
             response = new JokesSearchResponse()
            {
                results = new List<DadJokesResponse>()
                {
                    new DadJokesResponse()
                    {
                        id = "1",
                        joke = "This is first joke in short joke list"
                    },
                    new DadJokesResponse()
                    {
                         id = "2",
                        joke = "This is first joke in medium joke list having more than ten words"
                    },
                    new DadJokesResponse()
                    {
                         id = "3",
                        joke = "This is first joke in medium joke list having more than twenty words. This is first joke in medium joke list having more than twenty words."
                    }
                }
            };

        }
            

        [Fact]
        public void GroupResponseList_Based_On_Length_Success()
        {
            ResponseHelper helper = new ResponseHelper();
            List<DadJokesResponse> sList = new List<DadJokesResponse>();
            List<DadJokesResponse> mList = new List<DadJokesResponse>();
            List<DadJokesResponse> lList = new List<DadJokesResponse>();

            helper.GroupByWordCountAndFormat(response, ref sList, ref mList, ref lList, "first");

            Assert.True(sList.Count.Equals(1));
            Assert.True(mList.Count.Equals(1));
            Assert.True(lList.Count.Equals(1));


        }
        [Fact]
        public void Word_Count_In_Joke_Success()
        {
            ResponseHelper helper = new ResponseHelper();
           

            var count = helper.CountWordsInString(response.results[0].joke);

            Assert.True(count.Equals(8));
          

        }
    }
}
