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
using System.Data.Entity;

namespace filmwebManager.Controllers
{
    public class movieActorsResponse
    {
        public int Movie_id { get; set; }
        public int Person_id { get; set; }
     
    }

    public class movieActorModel
    {
        public int PersonId { get; set; }
        public string Name { get; set; }

    }

    public class MovieModel
    {
        public int MovideId { get; set; }
        public string Title { get; set; }
    }
    public class moviActorPostModel
    {
        public string PersonName { get; set; }
        public string Title { get; set; }

        public class movieActorsController : ApiController
        {
            FilmContext ctx = new FilmContext();
            public movieActorsController(FilmContext context)
            {
                ctx = context;
            }


            // GET api/<controller>
            //[Route("api/movieActors")]
            public IHttpActionResult Get()
            {
                return Ok(ctx.MovieActors
                    .Include(x => x.Movies)
                    .Include(x => x.Persons)
                    .Select(x => new movieActorsResponse
                    {
                        Movie_id = x.Movie_id,
                        Person_id = x.Person_id,
                        
                    })
                    .ToList());
            }

            // GET api/<controller>/5
            //[Route("api/movieActors/{movieId}")]
            public IHttpActionResult Get(int id)
            {
                var actors= ctx.MovieActors
                    .Include(x => x.Movies)
                    .Include(x => x.Persons)
                    .Where(x => x.Movie_id == id)
                    .Select(x => new movieActorsResponse
                    {
                         Movie_id = x.Movie_id,
                        Person_id = x.Person_id,
                    })
                    .ToList();
                if (actors != null)
                {
                    return Ok(actors);
                }
                return NotFound();
            }



            //[Route("api/movieActors/Person/{personName}")]
            //public IHttpActionResult Get(int id,string personName)
            //{
            //    var actors = ctx.MovieActors
            //        .Include(x => x.Movies)
            //        .Include(x => x.Persons)
            //        .Where(x => x.Persons.Name == personName)
            //        .Select(x => new MovieModel
            //        {
            //            MovideId = x.Movie_id,
            //            Title = x.Movies.title,
            //        })
            //        .ToList();
            //    if (actors != null)
            //    {
            //        return Ok(actors);
            //    }
            //    return NotFound();
            //}



            // POST api/<controller>
            public IHttpActionResult Post([FromBody] moviActorPostModel value)
            {
                try
                {
                    var movie = ctx.Movies
                        .Where(x => x.Title == value.Title)
                        .Select(x=>x.Movie_id)
                        .FirstOrDefault();
                    var person = ctx.Persons
                                .Where(x => x.Name == value.PersonName)
                                .Select(x=>x.Person_id)
                                .FirstOrDefault();

                    if (movie != 0 && person != 0)
                    {
                        ctx.MovieActors.Add(new MovieActors
                        {
                            Person_id = person,
                            Movie_id = movie
                        });
                        ctx.SaveChanges();
                        return Ok();
                    }
                    return BadRequest();

                }
                catch (Exception ex)
                {
                    return InternalServerError(ex);
                }
            }

            // PUT api/<controller>/5
            public IHttpActionResult Put(int movieId, int actorId, [FromBody] moviActorPostModel value)
            {
                try
                {
                    var responseResult = ctx.MovieActors.Where(x => x.Movie_id == movieId && x.Person_id==actorId).FirstOrDefault();
                    if (responseResult != null)
                    {
                        ctx.MovieActors.Remove(responseResult);
                        ctx.SaveChanges();

                        var movie = ctx.Movies
                             .Where(x => x.Title == value.Title)
                             .Select(x => x.Movie_id)
                             .FirstOrDefault();
                        var person = ctx.Persons
                                    .Where(x => x.Name == value.PersonName)
                                    .Select(x => x.Person_id)
                                    .FirstOrDefault();

                        ctx.MovieActors.Add(new MovieActors
                        {
                            Movie_id=movie,
                            Person_id=person
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

            // DELETE api/<controller>/5
            public IHttpActionResult Delete(int movieId, int personId)
            {
                try
                {
                    var deleteActor = ctx.MovieActors
                        .Where(x => x.Movie_id == movieId && x.Person_id==personId)
                        .FirstOrDefault();

                    if (deleteActor != null)
                    {
                        ctx.MovieActors.Remove(deleteActor);
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
}