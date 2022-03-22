using System.Runtime.Serialization;

namespace HaveFunWithDadJokes.Models
{

    [DataContract]
    public class BaseResponse
    {        

        [DataMember(Name = "status")]
        public string Status { get; set; }

        [DataMember(Name = "statusCode")]
        public string StatusCode { get; set; }
    }
    [DataContract]
    public class DadJokeResponse : BaseResponse
    {
        [DataMember(Name = "joke")]        
        public string Text { get; set; }

        [DataMember(Name = "id")]
        public string UserName { get; set; }


    }
}
