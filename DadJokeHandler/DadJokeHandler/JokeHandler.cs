using DadJokeHandler.Helper;
using DadJokeHandler.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;


namespace DadJokeHandler
{
    public class JokeHandler
    {
        private SearchFilters _filters;

        public JokeHandler()
        {
            _filters = new SearchFilters();            
            ClientHelper.CreateClinet();
        }

        public async Task<JokesSearchResponse> SearchDadJokes(string term)
        {
            JokesSearchResponse jokeList = null;
            try
            {
                _filters.Term = term;
                var builder = new UriBuilder(_filters.BaseUrl + "/search");
                var query = HttpUtility.ParseQueryString(builder.Query);
                query.Add("term", _filters.Term);
                query.Add("limit", _filters.Limit.ToString());
                query.Add("page", _filters.Page.ToString());
                builder.Query = query.ToString();
                string url = builder.ToString();

                using (HttpResponseMessage response = await ClientHelper.JokeHttpClient.GetAsync(url))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var jsonResponse = await response.Content.ReadAsStringAsync();
                        jokeList = JsonConvert.DeserializeObject<JokesSearchResponse>(jsonResponse);
                    }                   
                    
                }
                return jokeList;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return jokeList;               
            }

        }

        public async Task<DadJokesResponse> GetRandomJoke()
        {
            DadJokesResponse jokeList = null;
            try
            {              

                using (HttpResponseMessage response = await ClientHelper.JokeHttpClient.GetAsync(_filters.BaseUrl))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        jokeList = await response.Content.ReadAsAsync<DadJokesResponse>();
                    }
                }
                return jokeList;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return jokeList;
            }

        }
    }
}
