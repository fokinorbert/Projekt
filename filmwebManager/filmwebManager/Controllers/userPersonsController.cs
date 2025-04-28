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
    public class userPersonResponse
    {
        public int User_id { get; set; }
        public int Person_id { get; set; }
    }

    public class userPersonPostModel
    {
        public int UserId { get; set; }
        public string PersonName { get; set; }
    }
    public class userPersonsController : ApiController
    {
        FilmContext ctx = new FilmContext();

        public IHttpActionResult Get()
        {
            return Ok(ctx.UserPersons
                .Include(x=>x.Persons)
                .Include(x=>x.User)
                .Select(x=>new userPersonResponse 
                {
                    User_id=x.User_id,
                    Person_id=x.Person_id
                })
                .ToList());
        }


        public IHttpActionResult Get(int id)
        {
            return Ok(ctx.UserPersons
                .Include(x => x.Persons)
                .Include(x => x.User)
                .Where(x => x.User_id == id)
                .Select(x => new userPersonResponse
                {
                    User_id = x.User_id,
                    Person_id = x.Person_id
                })
                .FirstOrDefault());
        }

        [Route("person")]
        public IHttpActionResult Get(int id, string a)
        {
            return Ok(ctx.UserPersons
                .Include(x => x.Persons)
                .Include(x => x.User)
                .Where(x => x.Person_id == id)
                .Select(x => new userPersonResponse
                {
                    User_id = x.User_id,
                    Person_id = x.Person_id
                })
                .FirstOrDefault());
        }

        public IHttpActionResult Post([FromBody] userPersonPostModel value)
        {
            try
            {
                var person = ctx.Persons
                    .Where(x => x.Name == value.PersonName)
                    .FirstOrDefault();
                if (person != null)
                {
                    ctx.UserPersons.Add(new UserPersons
                    {
                        Person_id = person.Person_id,
                        User_id = value.UserId
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


        public IHttpActionResult Put(int personId, [FromBody] userPersonResponse value)
        {
            try
            {
                var responseResult = ctx.UserPersons
                    .Include(x => x.Persons)
                    .Include(x => x.User)
                    .Where(x => x.User_id == value.User_id && x.Person_id == personId)
                    .FirstOrDefault();

                if (responseResult != null)
                {
                    ctx.UserPersons.Remove(responseResult);
                    ctx.SaveChanges();

                    ctx.UserPersons.Add(new UserPersons
                    {
                        User_id = value.User_id,
                        Person_id = value.Person_id
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

        public IHttpActionResult Delete(int userId, int personId)
        {
            try
            {
                var deletePerson = ctx.UserPersons
                    .Include(x => x.Persons)
                    .Include(x => x.User)
                    .Where(x => x.User_id == userId && x.Person_id == personId)
                    .FirstOrDefault();

                if (deletePerson != null)
                {
                    ctx.UserPersons.Remove(deletePerson);
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