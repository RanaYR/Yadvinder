using DadJokeHandler;
using DadJokeHandler.Helper;
using DadJokeHandler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter 1 to display a random joke.");
            Console.WriteLine("Please enter 2 to display jokes based on term");
            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    {
                        GetRandomJoke();
                        break;
                    }
                case "2":
                    {
                        SearchJokes();
                            break;
                    }
                default:
                    {
                        Console.WriteLine("Invalid Entry");
                        break;
                    }
            }
        }

        public static void GetRandomJoke()
        {
         
            JokeHandler handler = new JokeHandler();
            var task = handler.GetRandomJoke();
            task.Wait();
            var result = task.Result;
            Console.WriteLine(result.joke);
            Console.ReadLine();

        }

        public static void SearchJokes()
        {
            List<DadJokesResponse> shortJokeList = new List<DadJokesResponse>();
            List<DadJokesResponse> mediumJokeList = new List<DadJokesResponse>();
            List<DadJokesResponse> longJokeList = new List<DadJokesResponse>();
            Console.WriteLine("Please enter term to search.");
            var term = Console.ReadLine();
            JokeHandler handler = new JokeHandler();
            
            var task = handler.SearchDadJokes(term);
            task.Wait();
            var result = task.Result;

            ResponseHelper helper = new ResponseHelper();

            helper.GroupByWordCountAndFormat(result, ref shortJokeList, ref mediumJokeList, ref longJokeList, term);


            Console.WriteLine("-----------------SHORT LENGTH JOKES------------------");
            foreach ( DadJokesResponse response in shortJokeList)
            {
               
                Console.WriteLine(response.joke);
            }
            Console.WriteLine("-----------------MEDIUM LENGTH JOKES------------------");
            foreach (DadJokesResponse response in mediumJokeList)
            {
                
                Console.WriteLine(response.joke);
            }
            Console.WriteLine("-----------------LONG LENGTH JOKES------------------");
            foreach (DadJokesResponse response in longJokeList)
            {
                
                Console.WriteLine(response.joke);
            }


            Console.ReadLine();

        }
    }
}
