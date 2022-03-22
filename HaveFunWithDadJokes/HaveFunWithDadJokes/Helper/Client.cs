using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace HaveFunWithDadJokes.Helper
{
    public class Client : IClient
    {
        public HttpClient JokeHttpClient { get; set; }
        public HttpClient CreateClinet()
        {
            JokeHttpClient = new HttpClient();
            JokeHttpClient.DefaultRequestHeaders.Accept.Clear();
            JokeHttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return JokeHttpClient;
        }
    }
}