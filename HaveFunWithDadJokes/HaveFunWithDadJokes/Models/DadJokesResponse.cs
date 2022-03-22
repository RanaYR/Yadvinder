using System.Collections.Generic;
using System.Runtime.Serialization;

namespace HaveFunWithDadJokes.Models
{
    [DataContract]
    public class JokesSearchResponse : BaseResponse
    {
        [DataMember(Name = "results")]
        public List<DadJokeResponse> results { get; set; }       
        [DataMember(Name = "status")]
        public int status { get; set; }

    }
}
