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
    public class PersonsController : ApiController
    {
        FilmContext ctx = new FilmContext();
        public PersonsController(FilmContext context)
        {
            ctx = context;
        }

        // GET api/<controller>


        public IHttpActionResult Get()
        {
            return Ok(ctx.Persons
                .ToList());
        }

        // GET api/<controller>/5
        public IHttpActionResult Get(int id)
        {
            return Ok(ctx.Persons.Where(x => x.Person_id == id).FirstOrDefault());
        }

        // POST api/<controller>
        public IHttpActionResult Post([FromBody] Persons value)
        {
            var person = ctx.Persons.Where(x => x.Person_id == value.Person_id).FirstOrDefault();
            try
            {
                if (person == null)
                {
                    ctx.Persons.Add(new Persons() { Name = value.Name, Person_id = value.Person_id, Birth_Place = value.Birth_Place, Date = value.Date, Role = value.Role });
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
        public IHttpActionResult Put(int id, [FromBody] Persons value)
        {
            var responseResult = ctx.Persons.Where(x => x.Person_id == id).FirstOrDefault();
            try
            {
                if (responseResult != null)
                {
                    responseResult.Name = responseResult.Name;
                    responseResult.Birth_Place = responseResult.Birth_Place;
                    responseResult.Date = responseResult.Date;
                    responseResult.Role = responseResult.Role;
                    responseResult.Person_id = responseResult.Person_id;
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
                var deletePerson = ctx.Persons.Where(x => x.Person_id == id).FirstOrDefault();

                var userPersons = ctx.UserPersons
                    .Where(x => x.Person_id == id)
                    .ToList();

                var movieActors = ctx.MovieActors
                    .Where(x => x.Person_id == id)
                    .ToList();

                var movies = ctx.Movies
                    .Where(x => x.Director_id == id)
                    .ToList();
                if (deletePerson != null)
                {
                    foreach (var item in userPersons)
                    {
                        ctx.UserPersons.Remove(item);
                        ctx.SaveChanges();
                    }

                    foreach (var item in movieActors)
                    {
                        ctx.MovieActors.Remove(item);
                        ctx.SaveChanges();
                    }

                    foreach (var item in movies)
                    {
                        ctx.Movies.Remove(item);
                        ctx.SaveChanges();
                    }

                    ctx.Persons.Remove(deletePerson);
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