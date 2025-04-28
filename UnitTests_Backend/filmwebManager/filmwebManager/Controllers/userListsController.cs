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
    public class UserListPostModel
    {
        public int userId { get; set; }
        public string ListName { get; set; }
    }

    [RoutePrefix("api/userlists")]
    public class UserListsController : ApiController
    {
        FilmContext ctx = new FilmContext();
        public UserListsController(FilmContext context)
        {
            ctx = context;
        }


        [HttpGet]
        [Route("")]
        public IHttpActionResult Get()
        {
            var lists = ctx.UserLists
                .Include(x => x.user)
                .ToList();

            var result = lists.Select(list => new
            {
                list.list_id,
                list.list_name,
                list.user_id,
                films = (from ulm in ctx.UserlistMovies
                         join m in ctx.Movies on ulm.movie_id equals m.Movie_id
                         where ulm.list_id == list.list_id
                         select new
                         {
                             movie_id = m.Movie_id,
                             title = m.Title,
                             release_year = m.Relase_Year,
                             img_url = string.IsNullOrEmpty(m.Img_Url) ? "default-image-url.jpg" : m.Img_Url,
                             genre_id = m.Genre_id
                         }).ToList()
            });

            return Ok(result);
        }

        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            return Ok(ctx.UserLists
                .Include(x => x.user)
                .Where(x => x.list_id == id)
                .FirstOrDefault());
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult Post([FromBody] UserListPostModel value)
        {
            var list = ctx.UserLists
                .Include(x => x.user)
                .Where(x => x.list_name == value.ListName && x.user_id == value.userId)
                .FirstOrDefault();

            try
            {
                if (list == null)
                {
                    ctx.UserLists.Add(new UserLists()
                    {
                        user_id = value.userId,
                        list_name = value.ListName
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

        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult Put(int id, [FromBody] UserListPostModel value)
        {
            var responseResult = ctx.UserLists
                .Include(x => x.user)
                .Where(x => x.list_id == id)
                .FirstOrDefault();

            try
            {
                if (responseResult != null)
                {
                    responseResult.list_name = value.ListName;
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

        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var deleteList = ctx.UserLists
                    .Include(x => x.user)
                    .Where(x => x.list_id == id)
                    .FirstOrDefault();

                var movies = ctx.UserlistMovies
                    .Where(x => x.list_id == id)
                    .ToList();

                foreach (var item in movies)
                {
                    ctx.UserlistMovies.Remove(item);
                    ctx.SaveChanges();
                }

                ctx.UserLists.Remove(deleteList);
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