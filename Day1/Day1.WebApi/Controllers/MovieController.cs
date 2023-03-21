using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Day1.WebApi.Controllers
{
    public class MovieController : ApiController
        {
        static List<Movie> movies = new List<Movie>()
        { new Movie { Id = "001", Title = "The Godfather" },
            new Movie { Id = "002", Title = "The Shawshank Redemption" } ,
            new Movie { Id = "003", Title = "The Dark Knight" } };
        // GET api/<controller>
        
        public IEnumerable<Movie> GetMovies()
            {

            if (movies == null)
            {
                throw new Exception("There is nothing to get!");
            }
            else
            {
                return movies;
            }
                
            
            }

            // GET api/<controller>/5
            public Movie GetMoviesById(string Id)
            {

            try
            {
                var movieById = (from m in movies
                                 where m.Id == Id
                                 select m).FirstOrDefault();
                return movieById;
            }
            catch 
            {
                throw new Exception($"There is no movie with the entered Id!");
            }
            
        }

            // POST api/<controller>
            public IEnumerable<Movie> PostMovie(Movie movie)
            {
                List<Movie> movies = new List<Movie>();
                movies.Add(movie);
                return movies;
            }

            // PUT api/<controller>/5
            public Movie PutMovie(string Id,Movie movie)
            {
                var movieToUpdate = (from m in movies
                                     where m.Id == Id
                                     select m).FirstOrDefault();
            
            if (movieToUpdate == null)
            {
                throw new Exception ($"Movie with ID: {Id} does not exist!");
            }

            movieToUpdate.Id = Id;
            movieToUpdate.Title = movie.Title;

            return movieToUpdate;
            }

            // DELETE api/<controller>/5
            public void DeleteMovie(string Id, Movie movie)
            {
            }
    }
}
