using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Day1.WebApi.Controllers
{
    public class Movie
    {
        public string Id { get; set; }
        public string Title { get; set; }
    }

    public class Program
    {
        public static void Main()
        {
            // Create a new List of Movie objects
            List<Movie> movies = new List<Movie>();

            // Add some movies to the list
            movies.Add(new Movie { Id = "001", Title = "The Godfather" });
            movies.Add(new Movie { Id = "002", Title = "The Shawshank Redemption" });
            movies.Add(new Movie { Id = "003", Title = "The Dark Knight" });
        }

        public class MovieController : ApiController
        {
            // GET api/<controller>
            public IEnumerable<string> Get()
            {
                return new string[] { "value1", "value2" };
            }

            // GET api/<controller>/5
            public string Get(int id)
            {
                return "value";
            }

            // POST api/<controller>
            public void Post([FromBody] string value)
            {
            }

            // PUT api/<controller>/5
            public void Put(int id, [FromBody] string value)
            {
            }

            // DELETE api/<controller>/5
            public void Delete(int id)
            {
            }
        }
    }
}