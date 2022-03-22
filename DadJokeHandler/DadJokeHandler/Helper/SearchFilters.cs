using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DadJokeHandler.Models;

namespace DadJokeHandler.Models
{
    class SearchFilters
    {
        public string Term { get; set; }
        public int Limit { get => Constants.Limit; }
        public int Page { get => Constants.Page; }
        public string BaseUrl { get => Constants.BaseURL; }
    }
}
