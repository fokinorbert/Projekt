using filmwebManager.Context;
using filmwebManager.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using filmwebManager.encryptionManager;
using System.Data.Entity;

namespace filmwebManager.Controllers
{
    public class MoviesResponse
    {
        public int Movie_id { get; set; }
        public string Title { get; set; }
        public int Genre_id { get; set; }
        public int Relase_Year { get; set; }
        public string Img_Url { get; set; }
    }
    public class MoviePostModel
    {
        public MoviePostModel() { }
        public int Movie_id { get; set; }
        public string Title { get; set; }
        public string Genre_name { get; set; }
        public int Relase_Year { get; set; }
        public string Img_Url { get; set; }
        public int Director_Id { get; set; }
    }

    public class moviesController : ApiController
    {
        FilmContext ctx = new FilmContext();
        public moviesController(FilmContext context)
        {
            ctx = context;
        }

        // GET api/<controller>
        public IHttpActionResult Get()
        {
            return Ok(ctx.Movies
                .Select(x => new MoviesResponse
                {
                    Movie_id = x.Movie_id,
                    Title = x.Title,
                    Genre_id = x.Genre_id,
                    Relase_Year = x.Relase_Year,
                    Img_Url = x.Img_Url,
                })
                .ToList());
        }

        [HttpGet]
        [Route("api/movies/genre/{genre}")]
        public IHttpActionResult GetByGenre(string genre)
        {
            var movies = ctx.Movies
                .Include(m => m.Genres)
                .Where(m => m.Genres.Genre_name.ToLower() == genre.ToLower())
                .Select(x => new
                {
                    Movie_id = x.Movie_id,
                    Title = x.Title,
                    Genre_name = x.Genres.Genre_name,
                    Release_Year = x.Relase_Year,
                    Img_Url = string.IsNullOrEmpty(x.Img_Url) ? "../images/default-image-url.jpg" : x.Img_Url,
                    Director = ctx.Persons
                        .Where(p => p.Person_id == x.Director_id)
                        .Select(p => p.Name)
                        .FirstOrDefault(),
                    Duration = x.Duration
                })
                .ToList();

            return Ok(movies);
        }
        [HttpGet]
        [Route("api/movies/genres")]
        public IHttpActionResult GetAllGenres()
        {
            var genres = ctx.Genres.Select(g => g.Genre_name).ToList();
            return Ok(genres);
        }

        [HttpGet]
        [Route("api/movies/search")]
        public IHttpActionResult SearchMovies(string query)
        {
            var lowerQuery = query.ToLower();
            var results = ctx.Movies
                .Include(m => m.Genres)
                .Where(m => m.Title.ToLower().Contains(lowerQuery))
                .Select(x => new
                {
                    id = x.Movie_id,
                    title = x.Title,
                    year = x.Relase_Year,
                    posterUrl = x.Img_Url,
                    director = ctx.Persons.Where(p => p.Person_id == x.Director_id).Select(p => p.Name).FirstOrDefault()
                })
                .ToList();

            return Ok(results);
        }
        [HttpGet]
        [Route("api/movies/{id}")]
        public IHttpActionResult GetMovieById(int id)
        {
            var movie = ctx.Movies
                .Where(x => x.Movie_id == id)
                .Select(x => new
                {
                    movie_id = x.Movie_id,
                    title = x.Title,
                    release_Year = x.Relase_Year,
                    img_Url = x.Img_Url,
                    duration = x.Duration,
                    description = x.Description, // ← NEW FIELD
                    director = ctx.Persons
                        .Where(p => p.Person_id == x.Director_id)
                        .Select(p => p.Name)
                        .FirstOrDefault()
                })
                .FirstOrDefault();

            if (movie == null)
                return NotFound();

            return Ok(movie);
        }



        // POST api/<controller>
        public IHttpActionResult Post([FromBody] MoviePostModel value)
        {
            var film = ctx.Movies
                .Include(x => x.Genres)
                .Select(x => new MoviesResponse
                {
                    Movie_id = x.Movie_id,
                    Title = x.Title,
                    Genre_id = x.Genre_id,
                    Relase_Year = x.Relase_Year,
                    Img_Url = x.Img_Url,
                })
                .Where(x => x.Movie_id == value.Movie_id)
                .FirstOrDefault();

            var genre = ctx.Genres
                .Where(x => x.Genre_name == value.Genre_name)
                .Select(x => x.Genre_id)
                .FirstOrDefault();
            try
            {
                if (film == null)
                {
                    ctx.Movies.Add(new Movies()
                    {
                        Movie_id = value.Movie_id,
                        Genre_id = genre,
                        Title = value.Title,
                        Relase_Year = value.Relase_Year,
                        Director_id = value.Director_Id,
                        Img_Url = value.Img_Url
                    });
                    ctx.SaveChanges();
                    return Ok();
                }
                else return Conflict();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PUT api/<controller>/5
        public IHttpActionResult Put(int id, [FromBody] Movies value)
        {
            var responseResult = ctx.Movies.Where(x => x.Movie_id == id).FirstOrDefault();
            try
            {
                if (responseResult != null)
                {
                    responseResult.Genre_id = value.Genre_id;
                    responseResult.Title = value.Title;
                    responseResult.Relase_Year = value.Relase_Year;
                    responseResult.Img_Url = value.Img_Url;
                    responseResult.Genres = value.Genres;
                    responseResult.Movie_id = responseResult.Movie_id;
                    ctx.SaveChanges();
                    return Ok();
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE api/<controller>/5
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var deleteMovie = ctx.Movies.Where(x => x.Movie_id == id).FirstOrDefault();
                if (deleteMovie != null)
                {
                    var actors = ctx.MovieActors
                    .Include(x => x.Movies)
                    .Where(x => x.Movie_id == id)
                    .ToList();

                    var userMovies = ctx.UserMovies
                        .Where(x => x.Movie_id == id)
                        .ToList();

                    var ratings = ctx.Ratings
                        .Where(x => x.Movie_id == id)
                        .ToList();

                    var comments = ctx.Comments
                        .Where(x => x.Movie_id == id)
                        .ToList();

                    var userlistmovies = ctx.UserlistMovies
                        .Where(x => x.movie_id == id)
                        .ToList();
                    foreach (var item in actors)
                    {
                        ctx.MovieActors.Remove(item);
                        ctx.SaveChanges();
                    }

                    foreach (var item in userMovies)
                    {
                        ctx.UserMovies.Remove(item);
                        ctx.SaveChanges();
                    }

                    foreach (var item in ratings)
                    {
                        ctx.Ratings.Remove(item);
                        ctx.SaveChanges();
                    }

                    foreach (var item in comments)
                    {
                        ctx.Comments.Remove(item);
                        ctx.SaveChanges();
                    }

                    foreach (var item in userlistmovies)
                    {
                        ctx.UserlistMovies.Remove(item);
                        ctx.SaveChanges();
                    }

                    ctx.Movies.Remove(deleteMovie);
                    ctx.SaveChanges();
                    return Ok();
                }
                return NotFound();

            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}