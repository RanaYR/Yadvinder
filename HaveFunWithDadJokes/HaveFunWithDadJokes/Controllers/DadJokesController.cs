using HaveFunWithDadJoke.Helper;
using HaveFunWithDadJokes.Helper;
using HaveFunWithDadJokes.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace HaveFunWithDadJokes.Controllers
{
    public class DadJokesController : ApiController
    {
        public readonly IAPIHelper _apiHelper;
        public readonly IClient _client;
        private ILogger<DadJokesController> _log;


        public DadJokesController(IAPIHelper apiHelper, IClient client, ILogger<DadJokesController> log)
        {
            _apiHelper = apiHelper;
            _client = client;
            _log = log;
        }

        [HttpGet]
        public async Task<HttpResponseMessage> GetRandomJoke()
        {
            DadJokeResponse jokeResponse = null;
            try
            {
                using (HttpResponseMessage response = await _client.CreateClinet().GetAsync("https://icanhazdadjoke.com/"))
                {
                    _log.LogInformation($"Get Random Joke request recieved");
                    if (response.IsSuccessStatusCode)
                    {                        
                        jokeResponse = await response.Content.ReadAsAsync<DadJokeResponse>();
                        _log.LogInformation($"Get Random Joke request processed succesfully. StatusCode: {response.StatusCode}");
                    }                    
                }

                if (jokeResponse == null)
                {
                    _log.LogInformation($"Null response recieved for Get Random Joke request");
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound,"Not Found");
                }
            }
            catch (Exception ex)
            {
                _log.LogError($"Get Random Joke: Failed to retieve Jokes {ex.Message}");
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError,"Internal Server Error");

            }
            return Request.CreateResponse(HttpStatusCode.OK, jokeResponse); 
        }

        [HttpGet]
        public async Task<HttpResponseMessage> GetFilteredJokes(string term, int limit=30, int page=1)
        {
            JokesSearchResponse jokeListResponse = new JokesSearchResponse();
            try
            {
                using (HttpResponseMessage response = await _client.CreateClinet().GetAsync(_apiHelper.CreateUrl("https://icanhazdadjoke.com/search", term, limit, page)))
                {
                    _log.LogInformation($"Get Filtered Jokes request recieved for term {term}");
                    if (response.IsSuccessStatusCode)
                    {
                        var jsonResponse = await response.Content.ReadAsStringAsync();
                        jokeListResponse = JsonConvert.DeserializeObject<JokesSearchResponse>(jsonResponse);
                        _log.LogInformation($"Get Filtered Jokes request processed succesfully for term {term}. Status: {response.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                _log.LogError($"Get Filtered Jokes: Failed to retieve Jokes {ex.Message}");
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Internal Server Error");
            }

            return Request.CreateResponse(HttpStatusCode.OK, jokeListResponse);

        }
    }
}
