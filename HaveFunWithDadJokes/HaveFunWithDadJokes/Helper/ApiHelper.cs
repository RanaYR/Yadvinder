using HaveFunWithDadJokes.Models;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace HaveFunWithDadJoke.Helper
{
    public class ApiHelper : IAPIHelper
    {
        
        public DadJokeResponse JokeResponse { get; set; }
        public string Url { get; set; } 

        public string CreateUrl(string BaseUrl, string term, int limit = 30, int page = 1)
        {
            var url = new UriBuilder(BaseUrl);
            var query = HttpUtility.ParseQueryString(url.Query);
            query.Add("term", term);
            query.Add("limit", limit.ToString());
            query.Add("page", page.ToString());
            url.Query = query.ToString();
            return url.ToString();

        }
    }
}
