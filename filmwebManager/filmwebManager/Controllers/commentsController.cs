using filmwebManager.Context;
using filmwebManager.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace filmwebManager.Controllers
{
    public class commentsResponse
    {
        public int Comment_id { get; set; }
        public int User_id { get; set; }
        public int Movie_id { get; set; }
        public string Comment { get; set; }
        public DateTime Created_At { get; set; }
        public string MovieTitle { get; set; }
        public string ImgUrl { get; set; }
        public string UserName { get; set; } 
    }

    public class commentPutModel
    {
        public string Comment { get; set; }
    }

    public class commentsController : ApiController
    {
        FilmContext ctx = new FilmContext();

        public IHttpActionResult Get()
        {
            return Ok(ctx.Comments
                .Include(x => x.User)
                .Include(x => x.Movies)
                .Select(x => new commentsResponse
                {
                    Comment_id = x.Comment_id,
                    User_id = x.User_id,
                    Movie_id = x.Movie_id,
                    Comment = x.Comment,
                    Created_At = x.Created_At,
                    MovieTitle = x.Movies.Title,
                    UserName = x.User.UserName  
                })
                .ToList());
        }

        [HttpGet]
        [Route("api/comments/movie/{movieId}")]
        public IHttpActionResult GetCommentsByMovie(int movieId)
        {
            var comments = ctx.Comments
                .Include(x => x.User)
                .Include(x => x.Movies)
                .Where(x => x.Movie_id == movieId)
                .Select(x => new commentsResponse
                {
                    Comment_id = x.Comment_id,
                    User_id = x.User_id,
                    Movie_id = x.Movie_id,
                    Comment = x.Comment,
                    Created_At = x.Created_At,
                    MovieTitle = x.Movies.Title,
                    ImgUrl = string.IsNullOrEmpty(x.Movies.Img_Url) ? "default-image-url.jpg" : x.Movies.Img_Url,
                    UserName = x.User.UserName  
                })
                .ToList();

            return Ok(comments);
        }

 
        public IHttpActionResult Get(int id)
        {
            return Ok(ctx.Comments
                .Include(x => x.User)
                .Include(x => x.Movies)
                .Where(x => x.User_id == id)
                .Select(x => new commentsResponse
                {
                    Comment_id = x.Comment_id,
                    User_id = x.User_id,
                    Movie_id = x.Movie_id,
                    Comment = x.Comment,
                    Created_At = x.Created_At,
                    MovieTitle = x.Movies.Title,
                    ImgUrl = string.IsNullOrEmpty(x.Movies.Img_Url)
                    ? "default-image-url.jpg"
                    : x.Movies.Img_Url,
                    UserName = x.User.UserName  
                })
                .ToList());
        }

        public IHttpActionResult Post([FromBody] commentsResponse value)
        {
            try
            {
                var movieExists = ctx.Movies.Any(m => m.Movie_id == value.Movie_id);
                var userExists = ctx.Users.Any(u => u.User_id == value.User_id);

                if (!movieExists || !userExists)
                {
                    return BadRequest("Hibás movie_id vagy user_id");
                }

                ctx.Comments.Add(new Comments
                {
                    Comment = value.Comment,
                    Comment_id = value.Comment_id,
                    Movie_id = value.Movie_id,
                    User_id = value.User_id,
                    Created_At = value.Created_At
                });

                ctx.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception("Mentési hiba: " + ex.Message));
            }
        }

        public IHttpActionResult Put(int id, [FromBody] commentPutModel value)
        {
            try
            {
                var responseResult = ctx.Comments
                    .Include(x => x.User)
                    .Include(x => x.Movies)
                    .Where(x => x.Comment_id == id)
                    .FirstOrDefault();

                if (responseResult != null)
                {
                    responseResult.Comment = value.Comment;
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

        public IHttpActionResult Delete(int id)
        {
            try
            {
                var deleteComment = ctx.Comments
                    .Include(x => x.User)
                    .Include(x => x.Movies)
                    .Where(x => x.Comment_id == id)
                    .FirstOrDefault();

                if (deleteComment != null)
                {
                    ctx.Comments.Remove(deleteComment);
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
