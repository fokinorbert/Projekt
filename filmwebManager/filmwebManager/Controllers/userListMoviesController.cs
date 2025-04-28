using filmwebManager.Context;
using filmwebManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace filmwebManager.Controllers
{
    public class UserListMoviesPutModel
    {
        public int movie_id { get; set; }
    }
    public class userListsMoviesController : ApiController
    {
        FilmContext ctx = new FilmContext();

        public IHttpActionResult Get()
        {
            return Ok(ctx.UserlistMovies.ToList());
        }

        public IHttpActionResult Get(int id)
        {
            return Ok(ctx.UserlistMovies
                .Where(x => x.list_id == id)
                .ToList());
        }


        public IHttpActionResult Post([FromBody] UserlistMovies value)
        {
            var userlist = ctx.UserlistMovies
                .Where(x => x.list_id == value.list_id && x.movie_id==value.movie_id)
                .FirstOrDefault();
            try
            {
                if (userlist == null)
                {
                    ctx.UserlistMovies.Add(new UserlistMovies 
                    { 
                        list_id = value.list_id,
                        movie_id = value.movie_id 
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


        public IHttpActionResult Put(int Listid, int MovieId, [FromBody] UserListMoviesPutModel value)
        {
            var responseResult = ctx.UserlistMovies
                .Where(x => x.list_id == Listid && x.movie_id==MovieId)
                .FirstOrDefault();
            try
            {
                if (responseResult != null)
                {
                    ctx.UserlistMovies.Remove(responseResult);
                    ctx.SaveChanges();
                    ctx.UserlistMovies.Add(new UserlistMovies
                    {
                        list_id = Listid,
                        movie_id = value.movie_id
                    });
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


        public IHttpActionResult Delete(int id, int MovieId)
        {
            try
            {
                var deleteUserList = ctx.UserlistMovies
                    .Where(x => x.list_id == id && x.movie_id==MovieId)
                    .FirstOrDefault();
                ctx.UserlistMovies.Remove(deleteUserList);
                ctx.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}