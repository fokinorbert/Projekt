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
    public class ratingResponse
    {
        public int Rating_id { get; set; }
        public int User_id { get; set; }
        public int Movie_id { get; set; }
        public int Rating { get; set; }
        public string MovieTitle { get; set; }
        public string ImgUrl { get; set; }
    }

    public class ratingsController : ApiController
    {
        FilmContext ctx = new FilmContext();
        public ratingsController(FilmContext context)
        {
            ctx = context;
        }


        // GET api/ratings
        public IHttpActionResult Get()
        {
            try
            {
                var ratings = ctx.Ratings
                    .Include(x => x.Movies)
                    .Include(x => x.User)
                    .Select(x => new ratingResponse
                    {
                        Rating_id = x.Rating_id,
                        User_id = x.User_id,
                        Movie_id = x.Movie_id,
                        Rating = x.Rating,
                        MovieTitle = x.Movies.Title,
                        ImgUrl = string.IsNullOrEmpty(x.Movies.Img_Url)
                            ? "default-image-url.jpg"
                            : x.Movies.Img_Url
                    })
                    .ToList();
                Console.WriteLine($"Fetched all ratings: {ratings.Count} records");
                return Ok(ratings);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GET /api/ratings: {ex.Message}");
                return InternalServerError(ex);
            }
        }

        // GET api/ratings/{userId}
        public IHttpActionResult Get(int id)
        {
            try
            {
                var ratings = ctx.Ratings
                    .Include(x => x.Movies)
                    .Include(x => x.User)
                    .Where(x => x.User_id == id)
                    .Select(x => new ratingResponse
                    {
                        Rating_id = x.Rating_id,
                        User_id = x.User_id,
                        Movie_id = x.Movie_id,
                        Rating = x.Rating,
                        MovieTitle = x.Movies.Title,
                        ImgUrl = string.IsNullOrEmpty(x.Movies.Img_Url)
                            ? "default-image-url.jpg"
                            : x.Movies.Img_Url
                    })
                    .ToList();
                Console.WriteLine($"Fetched ratings for user {id}: {ratings.Count} records");
                return Ok(ratings);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GET /api/ratings/{id}: {ex.Message}");
                return InternalServerError(ex);
            }
        }

        // POST api/ratings
        public IHttpActionResult Post([FromBody] ratingResponse value)
        {
            try
            {
                var movieExists = ctx.Movies.Any(m => m.Movie_id == value.Movie_id);
                var userExists = ctx.Users.Any(u => u.User_id == value.User_id);

                if (!movieExists || !userExists)
                {
                    Console.WriteLine($"Invalid movie_id {value.Movie_id} or user_id {value.User_id}");
                    return BadRequest("Hibás movie_id vagy user_id");
                }

                var existingRating = ctx.Ratings.FirstOrDefault(
                    r => r.User_id == value.User_id && r.Movie_id == value.Movie_id
                );

                if (existingRating != null)
                {
                    // Update existing rating
                    existingRating.Rating = value.Rating;
                    Console.WriteLine($"Updated rating for user {value.User_id}, movie {value.Movie_id}: {value.Rating}");
                }
                else
                {
                    // Add new rating
                    ctx.Ratings.Add(new Ratings
                    {
                        User_id = value.User_id,
                        Movie_id = value.Movie_id,
                        Rating = value.Rating
                    });
                    Console.WriteLine($"Added new rating for user {value.User_id}, movie {value.Movie_id}: {value.Rating}");
                }

                ctx.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in POST /api/ratings: {ex.Message}");
                return InternalServerError(ex);
            }
        }

        // GET api/ratings/average/{movieId}
        [HttpGet]
        [Route("api/ratings/average/{movieId}")]
        public IHttpActionResult GetAverageRating(int movieId)
        {
            try
            {
                var filmRatings = ctx.Ratings
                    .Where(r => r.Movie_id == movieId)
                    .ToList();
                var total = filmRatings.Count;
                var average = total > 0
                    ? filmRatings.Average(r => r.Rating)
                    : 0;

                // Log the ratings being counted
                Console.WriteLine($"Ratings for movie {movieId}:");
                foreach (var rating in filmRatings)
                {
                    Console.WriteLine($"User {rating.User_id}: {rating.Rating}");
                }

                var result = new
                {
                    averageRating = average, // Match the frontend expected property name
                    totalRaters = total
                };
                Console.WriteLine($"Average rating for movie {movieId}: {average}, Total raters: {total}");
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GET /api/ratings/average/{movieId}: {ex.Message}");
                return InternalServerError(ex);
            }
        }

        // PUT api/ratings/{id}
        public IHttpActionResult Put(int id, [FromBody] ratingResponse value)
        {
            try
            {
                var responseResult = ctx.Ratings.FirstOrDefault(x => x.Rating_id == id);
                if (responseResult == null)
                {
                    Console.WriteLine($"Rating {id} not found for update.");
                    return NotFound();
                }
                responseResult.User_id = value.User_id;
                responseResult.Movie_id = value.Movie_id;
                responseResult.Rating = value.Rating;
                ctx.SaveChanges();
                Console.WriteLine($"Updated rating {id} successfully.");
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in PUT /api/ratings/{id}: {ex.Message}");
                return InternalServerError(ex);
            }
        }

        // DELETE api/ratings/{id}
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var deleteRating = ctx.Ratings.FirstOrDefault(x => x.Rating_id == id);
                if (deleteRating == null)
                {
                    Console.WriteLine($"Rating {id} not found for deletion.");
                    return NotFound();
                }
                ctx.Ratings.Remove(deleteRating);
                ctx.SaveChanges();
                Console.WriteLine($"Deleted rating {id} successfully.");
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in DELETE /api/ratings/{id}: {ex.Message}");
                return InternalServerError(ex);
            }
        }
    }
}