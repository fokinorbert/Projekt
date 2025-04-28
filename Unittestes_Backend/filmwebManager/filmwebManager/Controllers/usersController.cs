using filmwebManager.Context;
using filmwebManager.Models;
using filmwebManager.encryptionManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using System.Web.Http.Cors;

namespace filmwebManager.Controllers
{
    public class UsersPost
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public List<string> Preferences { get; set; }
        public List<string> FavoriteActors { get; set; }
    }

    public class UserPatchModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class FavoriteRequest
    {
        public int UserId { get; set; }
        public int MovieId { get; set; }
        public string Status { get; set; } // "favorite", "plan", "watched"
    }

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class usersController : ApiController
    {
        FilmContext ctx = new FilmContext();
        public usersController(FilmContext context)
        {
            ctx = context;
        }


        [HttpGet]
        [Route("api/users/profile/{id}")]
        public IHttpActionResult GetProfile(int id)
        {
            var user = ctx.Users.FirstOrDefault(x => x.User_id == id);
            if (user == null)
                return NotFound();

            var preferences = ctx.UserGenres
                .Include(g => g.Genres)
                .Where(g => g.User_id == id)
                .Select(g => g.Genres.Genre_name)
                .ToList();

            var actorIds = ctx.UserPersons
                .Where(up => up.User_id == id)
                .Select(up => up.Person_id)
                .ToList();

            var recommendedMovies = ctx.Movies
                .Include(m => m.Genres)
                .Where(m =>
                    preferences.Contains(m.Genres.Genre_name) ||
                    ctx.MovieActors.Any(ma => ma.Movie_id == m.Movie_id && actorIds.Contains(ma.Person_id)))
                .Select(m => new
                {
                    Movie_id = m.Movie_id,
                    Title = m.Title,
                    Genre_name = m.Genres.Genre_name,
                    Release_Year = m.Relase_Year,
                    img_Url = string.IsNullOrEmpty(m.Img_Url) ? "../images/default-image-url.jpg" : m.Img_Url,
                    Director = ctx.Persons
                        .Where(p => p.Person_id == m.Director_id)
                        .Select(p => p.Name)
                        .FirstOrDefault(),
                    Duration = m.Duration
                })
                .ToList();

            var watchedMovies = ctx.UserMovies
                .Include(um => um.Movie)
                .Where(um => um.User_id == id && um.Status.ToLower() == "watched")
                .Select(um => new
                {
                    Movie_id = um.Movie.Movie_id,
                    Title = um.Movie.Title,
                    Release_Year = um.Movie.Relase_Year,
                    img_Url = string.IsNullOrEmpty(um.Movie.Img_Url) ? "default-image-url.jpg" : um.Movie.Img_Url,
                    Director = ctx.Persons
                        .Where(p => p.Person_id == um.Movie.Director_id)
                        .Select(p => p.Name)
                        .FirstOrDefault(),
                    Duration = um.Movie.Duration
                })
                .ToList();

            var favoriteMovies = ctx.UserMovies
                .Include(um => um.Movie)
                .Where(um => um.User_id == id && um.Status.ToLower() == "favorite")
                .Select(um => new
                {
                    Movie_id = um.Movie.Movie_id,
                    Title = um.Movie.Title,
                    Release_Year = um.Movie.Relase_Year,
                    img_Url = string.IsNullOrEmpty(um.Movie.Img_Url) ? "default-image-url.jpg" : um.Movie.Img_Url,
                    Director = ctx.Persons
                        .Where(p => p.Person_id == um.Movie.Director_id)
                        .Select(p => p.Name)
                        .FirstOrDefault(),
                    Duration = um.Movie.Duration
                })
                .ToList();

            var plannedMovies = ctx.UserMovies
                .Include(um => um.Movie)
                .Where(um => um.User_id == id && um.Status.ToLower() == "plan")
                .Select(um => new
                {
                    Movie_id = um.Movie.Movie_id,
                    Title = um.Movie.Title,
                    Release_Year = um.Movie.Relase_Year,
                    img_Url = string.IsNullOrEmpty(um.Movie.Img_Url) ? "default-image-url.jpg" : um.Movie.Img_Url,
                    Director = ctx.Persons
                        .Where(p => p.Person_id == um.Movie.Director_id)
                        .Select(p => p.Name)
                        .FirstOrDefault(),
                    Duration = um.Movie.Duration
                })
                .ToList();

            var comments = ctx.Comments
                .Where(c => c.User_id == id)
                .Select(c => new { text = c.Comment, date = c.Created_At })
                .ToList()
                .Select(c => new { text = c.text, date = c.date.ToString("yyyy-MM-dd") })
                .ToList();

            var stats = new
            {
                totalWatched = watchedMovies.Count,
                favorites = favoriteMovies.Count,
                reviewsWritten = comments.Count
            };

            return Ok(new
            {
                name = user.UserName,
                preferences = preferences,
                favoriteMovies = favoriteMovies,
                plannedMovies = plannedMovies,
                watched = watchedMovies,
                comments = comments,
                statistics = stats,
                recommendedMovies = recommendedMovies,
                recommendedTitles = recommendedMovies.Select(m => m.Title).ToList()
            });
        }

        [HttpPost]
        [Route("api/users/status")]
        public IHttpActionResult AddStatus([FromBody] FavoriteRequest req)
        {
            try
            {
                // Bemeneti adatok logolása
                Console.WriteLine($"AddStatus called: UserId={req.UserId}, MovieId={req.MovieId}, Status={req.Status}");

                // Ellenőrizzük, hogy a bemenet érvényes-e
                if (req == null || req.UserId <= 0 || req.MovieId <= 0 || string.IsNullOrEmpty(req.Status))
                {
                    Console.WriteLine("Invalid input: Request is null or contains invalid values.");
                    return BadRequest("Érvénytelen bemenet: userId, movieId vagy status nem megfelelő.");
                }

                // Ellenőrizzük, hogy létezik-e a felhasználó és a film
                var userExists = ctx.Users.Any(u => u.User_id == req.UserId);
                var movieExists = ctx.Movies.Any(m => m.Movie_id == req.MovieId);
                if (!userExists || !movieExists)
                {
                    Console.WriteLine($"Not found: UserId={req.UserId} or MovieId={req.MovieId} does not exist.");
                    return NotFound();
                }

                // Ellenőrizzük, hogy a státusz már létezik-e
                var exists = ctx.UserMovies.Any(um =>
                    um.User_id == req.UserId &&
                    um.Movie_id == req.MovieId &&
                    um.Status.ToLower() == req.Status.ToLower());

                if (exists)
                {
                    Console.WriteLine($"Status already exists: UserId={req.UserId}, MovieId={req.MovieId}, Status={req.Status}");
                    return Ok(new { message = "Státusz már létezik" });
                }

                // Közvetlen SQL parancs a beszúráshoz
                var statusLower = req.Status.ToLower();
                var sql = "INSERT INTO usermovies (user_id, movie_id, Status) VALUES (@p0, @p1, @p2)";
                int rowsAffected = ctx.Database.ExecuteSqlCommand(sql, req.UserId, req.MovieId, statusLower);

                if (rowsAffected == 0)
                {
                    Console.WriteLine($"Insert failed: UserId={req.UserId}, MovieId={req.MovieId}, Status={req.Status}");
                    return Content(HttpStatusCode.InternalServerError, "Nem sikerült a státusz hozzáadása.");
                }

                Console.WriteLine($"Status added successfully: UserId={req.UserId}, MovieId={req.MovieId}, Status={req.Status}");
                return Ok(new { message = "Státusz hozzáadva" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding status: {ex.Message}");
                return Content(HttpStatusCode.InternalServerError, $"Hiba történt: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("api/users/status/{userId}/{movieId}/{status}")]
        public IHttpActionResult RemoveStatus(int userId, int movieId, string status)
        {
            try
            {
                // Ellenőrizzük, hogy létezik-e a felhasználó és a film
                var userExists = ctx.Users.Any(u => u.User_id == userId);
                var movieExists = ctx.Movies.Any(m => m.Movie_id == movieId);
                if (!userExists || !movieExists)
                {
                    return NotFound();
                }

                // Logolás: összes rekord a törlés előtt
                var recordsBefore = ctx.UserMovies
                    .Where(um => um.User_id == userId && um.Movie_id == movieId)
                    .ToList();
                Console.WriteLine($"Records before deletion for User_id={userId}, Movie_id={movieId}:");
                foreach (var record in recordsBefore)
                {
                    Console.WriteLine($"Status: {record.Status}");
                }

                // Közvetlen SQL parancs a törléshez
                var statusLower = status.ToLower();
                var sql = "DELETE FROM usermovies WHERE user_id = @p0 AND movie_id = @p1 AND LOWER(Status) = @p2";
                int rowsAffected = ctx.Database.ExecuteSqlCommand(sql, userId, movieId, statusLower);

                if (rowsAffected == 0)
                {
                    Console.WriteLine($"Status not found: User_id={userId}, Movie_id={movieId}, Status={status}");
                    return NotFound();
                }

                Console.WriteLine($"Status deleted successfully: User_id={userId}, Movie_id={movieId}, Status={status}");

                // Logolás: összes rekord a törlés után
                var recordsAfter = ctx.UserMovies
                    .Where(um => um.User_id == userId && um.Movie_id == movieId)
                    .ToList();
                Console.WriteLine($"Records after deletion for User_id={userId}, Movie_id={movieId}:");
                foreach (var record in recordsAfter)
                {
                    Console.WriteLine($"Status: {record.Status}");
                }

                return Ok(new { message = "Státusz törölve" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during deletion: {ex.Message}");
                return Content(HttpStatusCode.InternalServerError, $"Hiba történt: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("api/users/search")]
        public IHttpActionResult SearchUsers(string query)
        {
            var results = ctx.Users
                .Where(u => u.UserName.Contains(query))
                .Select(u => new
                {
                    id = u.User_id,
                    username = u.UserName
                })
                .ToList();

            return Ok(results);
        }








        [HttpPost]
        [Route("api/users/register")]
        public IHttpActionResult Post([FromBody] UsersPost value)
        {
            var email = ctx.Users.Where(x => x.Email == value.Email || x.UserName == value.UserName).FirstOrDefault();
            encryption.CreatePasswordHash(value.Password, out byte[] hash, out byte[] salt);
            try
            {
                if (email == null)
                {
                    var newUser = new Users
                    {
                        Email = value.Email,
                        UserName = value.UserName,
                        Password_hash = hash,
                        Password_salt = salt
                    };
                    ctx.Users.Add(newUser);
                    ctx.SaveChanges();

                    if (value.Preferences != null && value.Preferences.Any())
                    {
                        foreach (var preference in value.Preferences)
                        {
                            var genre = ctx.Genres.FirstOrDefault(g => g.Genre_name == preference);
                            if (genre != null)
                            {
                                ctx.UserGenres.Add(new UserGenres
                                {
                                    User_id = newUser.User_id,
                                    Genre_id = genre.Genre_id
                                });
                            }
                        }
                    }

                    if (value.FavoriteActors != null && value.FavoriteActors.Any())
                    {
                        foreach (var actorName in value.FavoriteActors)
                        {
                            var actor = ctx.Persons.FirstOrDefault(p => p.Name == actorName && p.Role == "Actor");
                            if (actor != null)
                            {
                                ctx.UserPersons.Add(new UserPersons
                                {
                                    User_id = newUser.User_id,
                                    Person_id = actor.Person_id
                                });
                            }
                        }
                    }

                    ctx.SaveChanges();
                    return Ok(new { message = "Sikeres regisztráció" });
                }
                else
                {
                    return Content(HttpStatusCode.Conflict, new { message = "Ez az email/felhasználónév már használatban van." });
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("api/users/login")]
        public IHttpActionResult Login([FromBody] UsersPost login)
        {
            try
            {
                var user = ctx.Users.FirstOrDefault(x => x.Email == login.Email);
                if (user == null)
                    return Content(HttpStatusCode.Unauthorized, new { message = "Felhasználó nem található." });

                if (!encryption.VerifyPasswordHash(login.Password, user.Password_hash, user.Password_salt))
                    return Content(HttpStatusCode.Unauthorized, new { message = "Hibás jelszó." });

                return Ok(new { message = "Sikeres bejelentkezés", userId = user.User_id });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult Put(int id, [FromBody] UsersPost value)
        {
            var result = ctx.Users.FirstOrDefault(x => x.User_id == id);
            try
            {
                if (result != null)
                {
                    encryption.CreatePasswordHash(value.Password, out byte[] hash, out byte[] salt);
                    result.Email = value.Email;
                    result.UserName = value.UserName;
                    result.Password_hash = hash;
                    result.Password_salt = salt;
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

        public IHttpActionResult Patch(string email, [FromBody] UserPatchModel value)
        {
            var user = ctx.Users.FirstOrDefault(x => x.Email == email);
            if (user != null)
            {
                if (value.UserName != null)
                {
                    user.UserName = value.UserName;
                    ctx.SaveChanges();
                }
                if (value.Password != null)
                {
                    encryption.CreatePasswordHash(value.Password, out byte[] hash, out byte[] salt);
                    user.Password_salt = salt;
                    user.Password_hash = hash;
                    ctx.SaveChanges();
                }
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        public IHttpActionResult Delete(int id)
        {
            try
            {
                var deleteUser = ctx.Users.FirstOrDefault(x => x.User_id == id);
                if (deleteUser != null)
                {
                    var userLists = ctx.UserLists.Where(x => x.user_id == id).ToList();
                    var userMovies = ctx.UserMovies.Where(x => x.User_id == id).ToList();
                    var comments = ctx.Comments.Where(x => x.User_id == id).ToList();
                    var userGenres = ctx.UserGenres.Where(x => x.User_id == id).ToList();
                    var ratings = ctx.Ratings.Where(x => x.User_id == id).ToList();

                    ctx.UserLists.RemoveRange(userLists);
                    ctx.UserMovies.RemoveRange(userMovies);
                    ctx.Comments.RemoveRange(comments);
                    ctx.UserGenres.RemoveRange(userGenres);
                    ctx.Ratings.RemoveRange(ratings);
                    ctx.Users.Remove(deleteUser);

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
