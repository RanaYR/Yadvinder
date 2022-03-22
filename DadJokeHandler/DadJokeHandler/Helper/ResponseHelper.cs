using DadJokeHandler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DadJokeHandler.Helper
{
    public class ResponseHelper
    {
        public int CountWordsInString(string jokeString)
        {
            MatchCollection collection = Regex.Matches(jokeString, @"[\S]+");
            return collection.Count;
        }

        public void GroupByWordCountAndFormat(JokesSearchResponse dadJokesResponse, ref List<DadJokesResponse> shortLenght, 
            ref List<DadJokesResponse> mediumLenght, ref List<DadJokesResponse> longLenght, string term)
        {
            string searchTerm =  term;

            foreach (DadJokesResponse dadJoke in dadJokesResponse.results)
            {
                dadJoke.joke = Regex.Replace(dadJoke.joke, searchTerm, $"<{term.ToUpper()}>");

                int wordCount = CountWordsInString(dadJoke.joke);

                if (wordCount < 10)
                {
                    shortLenght.Add(dadJoke);
                }
                else if (wordCount > 9 && wordCount < 20)
                {
                    mediumLenght.Add(dadJoke);
                }
                else
                {
                    longLenght.Add(dadJoke);
                }
                
            }

        }        


    }
}
