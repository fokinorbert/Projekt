using filmwebManager.Context;
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
    public class userGenreResponse
    {
        public int User_id { get; set; }
        public int Genre_id { get; set; }
    }
    public class userGenresController : ApiController
    {
        FilmContext ctx = new FilmContext();

      
        public IHttpActionResult Get()
        {
            return Ok(ctx.UserGenres
                .Include(x=>x.User)
                .Include(x=>x.Genres)
                .Select(x=>new userGenreResponse 
                {
                    User_id=x.User_id,
                    Genre_id=x.Genre_id
                })
                .ToList());
        }


        public IHttpActionResult Get(int id)
        {
            return Ok(ctx.UserGenres
                .Include(x => x.User)
                .Include(x => x.Genres)
                .Where(x => x.User_id == id)
                .Select(x => new userGenreResponse
                {
                    User_id = x.User_id,
                    Genre_id = x.Genre_id
                })
                .ToList());
        }

  
        public IHttpActionResult Post([FromBody] userGenreResponse value)
        {
            try
            {
                ctx.UserGenres.Add(new UserGenres 
                { 
                    User_id =value.User_id, 
                    Genre_id = value.Genre_id
                }); 
                ctx.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult Put(int id, [FromBody] userGenreResponse value)
        {
            try
            {
                var responseResult = ctx.UserGenres
                    .Include(x => x.User)
                    .Include(x => x.Genres)
                    .Where(x => x.User_id == id)
                    .Select(x => new userGenreResponse
                    {
                        User_id = x.User_id,
                        Genre_id = x.Genre_id
                    })
                    .FirstOrDefault();
                responseResult.User_id = value.User_id;
                responseResult.Genre_id = value.Genre_id;
                ctx.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        public IHttpActionResult Delete(int id)
        {
            try
            {
                var deleteUser = ctx.UserGenres
                    .Include(x => x.User)
                    .Include(x => x.Genres)
                    .Where(x => x.User_id == id)
                    .FirstOrDefault();
                ctx.UserGenres.Remove(deleteUser);
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