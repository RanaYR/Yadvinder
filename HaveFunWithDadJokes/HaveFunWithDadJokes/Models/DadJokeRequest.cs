using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HaveFunWithDadJokes.Models
{
    public class DadJokeRequest
    {
        public string BaseAddress { get; set; }
        public string Uri { get; set; }
        public string MediaType { get; set; }
    }
}