using filmwebManager.Context;
using filmwebManager.Models;
using Org.BouncyCastle.Math.EC;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace filmwebManager.Controllers
{
    public class genreResponse
    {
        public int Genre_id { get; set; }
        public string Genre_name { get; set; }
    }

    public class genrePostModel
    {
        public string Genre_name { get; set; }
    }


    public class genresController : ApiController
    {
        FilmContext ctx = new FilmContext();
        public genresController(FilmContext context)
        {
            ctx = context;
        }

        // GET api/<controller>
        public IHttpActionResult Get()
        {
            var genrek= ctx.Genres
                .Select(x => new genreResponse
                {
                    Genre_id = x.Genre_id,
                    Genre_name = x.Genre_name
                })
                .ToList();
            if (genrek != null) return Ok(genrek);
            return Content(HttpStatusCode.NoContent, "");


        }

        // GET api/<controller>/5
        public IHttpActionResult Get(int id)
        {
            var genre= ctx.Genres
                .Where(x => x.Genre_id == id)
                .Select(x => new genreResponse
                {
                    Genre_id = x.Genre_id,
                    Genre_name = x.Genre_name
                })
                .FirstOrDefault();
            if(genre!=null) return Ok(genre);
            return NotFound();
        }

        // POST api/<controller>
        public IHttpActionResult Post([FromBody] genrePostModel value)
        {
            var genre = ctx.Genres
                .Where(x => x.Genre_name == value.Genre_name)
                .FirstOrDefault();
            try
            {
                if (genre == null)
                {
                    ctx.Genres.Add(new Genres
                    {
                        Genre_name = value.Genre_name
                    });
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
        public IHttpActionResult Put(int id, [FromBody] genrePostModel value)
        {
            try
            {
                var responseResult = ctx.Genres.Where(x => x.Genre_id == id).FirstOrDefault();
                if (responseResult != null)
                {
                    responseResult.Genre_id = id;
                    responseResult.Genre_name = value.Genre_name;
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
                var deleteGenre = ctx.Genres.Where(x => x.Genre_id == id).FirstOrDefault();
                var userGenre = ctx.UserGenres
                    .Where(x => x.Genre_id == id)
                    .ToList();
                var movies = ctx.Movies
                    .Where(x => x.Genre_id == id)
                    .ToList();

                if (deleteGenre != null)
                {
                    foreach (var item in userGenre) 
                    {
                        ctx.UserGenres.Remove(item);
                        ctx.SaveChanges();

                    }
                    foreach (var item in movies)
                    {
                        ctx.Movies.Remove(item);
                        ctx.SaveChanges();
                    }
                    ctx.Genres.Remove(deleteGenre);
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