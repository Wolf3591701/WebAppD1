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
            new Movie { Id = "003", Title = "The Dark Knight" } 
        };
        
        // GET api/<controller>
        public HttpResponseMessage GetMovies()
            {

                try
                {
                    if (movies != null)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, movies);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Movies not found!");
                    }
                } 
                catch (Exception)
                {
                        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "An error occured while executing your request!");
                }
            
            }

        // GET api/<controller>/5
        public HttpResponseMessage GetMoviesById(string id)
        {

            try
            {
                Movie movie = movies.Where(m => m.Id == id).FirstOrDefault();

                if (movie != null)
                {
                    return Request.CreateResponse<Movie>(HttpStatusCode.OK, movie);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Movie with that Id was not found!");
                }
            }
            catch (Exception)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "An error occured while executing your request!");
            }

        }

        // POST api/<controller>
        public HttpResponseMessage PostMovie(Movie movie)
        {
            try
            {
                if (movies != null)
                {
                    movies.Add(movie);
                    return Request.CreateResponse(HttpStatusCode.OK, movies);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Movie was not stored!");
                }
            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "An error ocurred while executing your request!");
            }
        }

            // PUT api/<controller>/5
            public HttpResponseMessage PutMovie(string id, Movie movie)
            {

                try
                {
                    Movie movieToUpdate = movies.Where(m => m.Id == id).FirstOrDefault();
                    movieToUpdate.Title = movie.Title;

                    if (movieToUpdate != null)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, movieToUpdate);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Movie title to be updated was not found!");
                    }
                }
                catch
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "An error ocurred while executing your request!");
                }

            }

            // DELETE api/<controller>/5
            public HttpResponseMessage DeleteMovie(string id)
            {
                try
                {

                    if (id == null && ModelState.IsValid)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, movies);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                    }
                }
                catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "An error ocurred while executing your request!");
            }
            
            }
    }
}
