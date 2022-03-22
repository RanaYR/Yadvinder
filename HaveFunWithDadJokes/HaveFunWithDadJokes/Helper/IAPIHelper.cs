using System.Net.Http;

namespace HaveFunWithDadJoke.Helper
{
    public interface IAPIHelper
    {        
        string CreateUrl(string BaseUrl, string term, int limit, int page);
    }
}
