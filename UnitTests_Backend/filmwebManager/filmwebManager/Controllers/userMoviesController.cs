using filmwebManager.Context;
using filmwebManager.encryptionManager;
using filmwebManager.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace filmwebManager.Controllers
{
    public class userMoviesResponse
    {
        public int User_id { get; set; }
        public int Movie_id { get; set; }
        public string Status { get; set; }
    }
    public class userMoviesController : ApiController
    {
        FilmContext ctx = new FilmContext();
        public userMoviesController(FilmContext context)
        {
            ctx = context;
        }

        // GET api/<controller>
        public IHttpActionResult Get()
        {
            return Ok(ctx.UserMovies
                .Include(x=>x.User)
                .Include(x=>x.Movie)
                .Select(x=>new userMoviesResponse 
                {
                    User_id=x.User_id,
                    Movie_id=x.Movie_id,
                    Status=x.Status
                })
                .ToList());
        }

        // GET api/<controller>/5
        public IHttpActionResult Get(int id)
        {
            return Ok(ctx.UserMovies
                .Include(x => x.User)
                .Include(x => x.Movie)
                .Where(x => x.User_id == id)
                .Select(x => new userMoviesResponse
                {
                    User_id = x.User_id,
                    Movie_id = x.Movie_id,
                    Status =x.Status
                })
                .FirstOrDefault());
        }

        // POST api/<controller>
        public IHttpActionResult Post([FromBody] userMoviesResponse value)
        {
            var userMovie = ctx.UserMovies
                .Where(x => x.User_id == value.User_id && x.Movie_id == value.Movie_id)
                .FirstOrDefault();
            try
            {
                if (userMovie == null)
                {
                    ctx.UserMovies.Add(new UserMovies { Movie_id = value.Movie_id, User_id = value.User_id, Status = value.Status });
                    ctx.SaveChanges();
                    return Ok();
                }
                return Conflict();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PUT api/<controller>/5
        public IHttpActionResult Put([FromBody] userMoviesResponse value)
        { 
            try
            {
                var responseResult = ctx.UserMovies
                    .Include(x => x.User)
                    .Include(x => x.Movie)
                    .Where(x => x.User_id == value.User_id && x.Movie_id == value.Movie_id)
                    .Select(x => new userMoviesResponse
                    {
                        User_id = x.User_id,
                        Movie_id = x.Movie_id,
                        Status =x.Status
                    })
                    .FirstOrDefault();
                if (responseResult != null)
                {
                    responseResult.User_id = responseResult.User_id;
                    responseResult.Movie_id = responseResult.Movie_id;
                    responseResult.Status = value.Status;
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
        public IHttpActionResult Delete(int userId, int movieId)
        {
            try
            {
                var deleteListMovie = ctx.UserMovies
                    .Include(x => x.User)
                    .Include(x => x.Movie)
                    .Where(x => x.User_id == userId && x.Movie_id == movieId)
                    .FirstOrDefault();
                if (deleteListMovie != null)
                {
                    ctx.UserMovies.Remove(deleteListMovie);
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