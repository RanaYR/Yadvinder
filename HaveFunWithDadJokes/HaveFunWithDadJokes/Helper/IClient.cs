using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HaveFunWithDadJokes.Helper
{
    public interface IClient
    {
        HttpClient CreateClinet();
    }
}
