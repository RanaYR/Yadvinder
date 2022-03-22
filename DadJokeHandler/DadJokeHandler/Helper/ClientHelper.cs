using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DadJokeHandler.Helper
{
    class ClientHelper
    {
        //replace with DI
        public static HttpClient JokeHttpClient { get; set; }        

        public static void CreateClinet()
        {
            JokeHttpClient = new HttpClient();
            JokeHttpClient.DefaultRequestHeaders.Accept.Clear();
            JokeHttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
