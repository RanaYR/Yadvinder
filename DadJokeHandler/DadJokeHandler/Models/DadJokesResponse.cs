using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DadJokeHandler.Models
{

    public class DadJokesResponse
    {        
        public string id { get; set; }        
        public string joke { get; set; }
        public string status { get; set; }
    }


    public class JokesSearchResponse
    {
        
        public List<DadJokesResponse> results { get; set; }
      
        public int status { get; set; }

    }

}
