using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Day1.WebApi.Controllers
{
    public /* static */ class MovieController : ApiController
        {
        static List<Movie> movies = new List<Movie>()
        { new Movie { Id = "001", Title = "The Godfather" },
            new Movie { Id = "002", Title = "The Shawshank Redemption" } ,
            new Movie { Id = "003", Title = "The Dark Knight" } };
        // GET api/<controller>
        
        public IEnumerable<Movie> GetMovies()
            {
                return movies;
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
