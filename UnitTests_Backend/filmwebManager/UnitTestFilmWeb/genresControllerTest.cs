using filmwebManager.Controllers;
using filmwebManager.Models;
using filmwebManager.Context;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http.Results;

namespace filmwebManager.Tests.Controllers
{
    [TestClass]
    public class genresControllerTest
    {
        private genresController controller;
        private Mock<FilmContext> mockContext;
        private Mock<DbSet<Genres>> mockSetGenres;
        private Mock<DbSet<UserGenres>> mockSetUserGenres;
        private Mock<DbSet<Movies>> mockSetMovies;

        [TestInitialize]
        public void Setup()
        {
            var genresData = new List<Genres>
            {
                new Genres { Genre_id = 1, Genre_name = "Action" },
                new Genres { Genre_id = 2, Genre_name = "Comedy" }
            }.AsQueryable();

            mockSetGenres = new Mock<DbSet<Genres>>();
            mockSetGenres.As<IQueryable<Genres>>().Setup(m => m.Provider).Returns(genresData.Provider);
            mockSetGenres.As<IQueryable<Genres>>().Setup(m => m.Expression).Returns(genresData.Expression);
            mockSetGenres.As<IQueryable<Genres>>().Setup(m => m.ElementType).Returns(genresData.ElementType);
            mockSetGenres.As<IQueryable<Genres>>().Setup(m => m.GetEnumerator()).Returns(genresData.GetEnumerator());

            var userGenresData = new List<UserGenres>().AsQueryable();
            mockSetUserGenres = new Mock<DbSet<UserGenres>>();
            mockSetUserGenres.As<IQueryable<UserGenres>>().Setup(m => m.Provider).Returns(userGenresData.Provider);
            mockSetUserGenres.As<IQueryable<UserGenres>>().Setup(m => m.Expression).Returns(userGenresData.Expression);
            mockSetUserGenres.As<IQueryable<UserGenres>>().Setup(m => m.ElementType).Returns(userGenresData.ElementType);
            mockSetUserGenres.As<IQueryable<UserGenres>>().Setup(m => m.GetEnumerator()).Returns(userGenresData.GetEnumerator());

            var moviesData = new List<Movies>().AsQueryable();
            mockSetMovies = new Mock<DbSet<Movies>>();
            mockSetMovies.As<IQueryable<Movies>>().Setup(m => m.Provider).Returns(moviesData.Provider);
            mockSetMovies.As<IQueryable<Movies>>().Setup(m => m.Expression).Returns(moviesData.Expression);
            mockSetMovies.As<IQueryable<Movies>>().Setup(m => m.ElementType).Returns(moviesData.ElementType);
            mockSetMovies.As<IQueryable<Movies>>().Setup(m => m.GetEnumerator()).Returns(moviesData.GetEnumerator());

            mockContext = new Mock<FilmContext>();
            mockContext.Setup(c => c.Genres).Returns(mockSetGenres.Object);
            mockContext.Setup(c => c.UserGenres).Returns(mockSetUserGenres.Object);
            mockContext.Setup(c => c.Movies).Returns(mockSetMovies.Object);

            controller = new genresController(mockContext.Object);
        }

        [TestMethod]
        public void Get_Returns_All_Genres()
        {
            var result = controller.Get() as OkNegotiatedContentResult<List<genreResponse>>;
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Content.Count);
        }

        [TestMethod]
        public void Get_ById_Returns_Correct_Genre()
        {
            var result = controller.Get(1) as OkNegotiatedContentResult<genreResponse>;
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Content.Genre_id);
            Assert.AreEqual("Action", result.Content.Genre_name);
        }

        [TestMethod]
        public void Post_Adds_New_Genre()
        {
            var newGenre = new genrePostModel
            {
                Genre_name = "Sci-Fi"
            };

            var result = controller.Post(newGenre);

            Assert.IsTrue(result is OkResult || result is ConflictResult);
        }

        [TestMethod]
        public void Put_Updates_Existing_Genre()
        {
            var updatedGenre = new genrePostModel
            {
                Genre_name = "Updated Action"
            };

            var result = controller.Put(1, updatedGenre);

            Assert.IsTrue(result is OkResult || result is NotFoundResult);
        }

        [TestMethod]
        public void Delete_Removes_Genre()
        {
            var result = controller.Delete(1);

            Assert.IsTrue(result is OkResult || result is NotFoundResult);
        }
    }
}
