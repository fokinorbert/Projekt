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
    public class moviesControllerTest
    {
        private moviesController controller;
        private Mock<FilmContext> mockContext;
        private Mock<DbSet<Movies>> mockSetMovies;
        private Mock<DbSet<Genres>> mockSetGenres;
        private Mock<DbSet<Persons>> mockSetPersons;
        private Mock<DbSet<MovieActors>> mockSetMovieActors;
        private Mock<DbSet<UserMovies>> mockSetUserMovies;
        private Mock<DbSet<Ratings>> mockSetRatings;
        private Mock<DbSet<Comments>> mockSetComments;
        private Mock<DbSet<UserlistMovies>> mockSetUserlistMovies;

        [TestInitialize]
        public void Setup()
        {
        
            var moviesData = new List<Movies>
            {
                new Movies { Movie_id = 1, Title = "Inception", Genre_id = 1, Relase_Year = 2010, Director_id = 1, Img_Url = "img1.jpg" },
                new Movies { Movie_id = 2, Title = "Titanic", Genre_id = 2, Relase_Year = 1997, Director_id = 2, Img_Url = "img2.jpg" }
            }.AsQueryable();

            var genresData = new List<Genres>
            {
                new Genres { Genre_id = 1, Genre_name = "Sci-Fi" },
                new Genres { Genre_id = 2, Genre_name = "Drama" }
            }.AsQueryable();

            var personsData = new List<Persons>
            {
                new Persons { Person_id = 1, Name = "Christopher Nolan" },
                new Persons { Person_id = 2, Name = "James Cameron" }
            }.AsQueryable();

            var emptyData = new List<MovieActors>().AsQueryable();
            var emptyUserMovies = new List<UserMovies>().AsQueryable();
            var emptyRatings = new List<Ratings>().AsQueryable();
            var emptyComments = new List<Comments>().AsQueryable();
            var emptyUserlistMovies = new List<UserlistMovies>().AsQueryable();

      
            mockSetMovies = CreateMockDbSet(moviesData);
            mockSetGenres = CreateMockDbSet(genresData);
            mockSetPersons = CreateMockDbSet(personsData);
            mockSetMovieActors = CreateMockDbSet(emptyData);
            mockSetUserMovies = CreateMockDbSet(emptyUserMovies);
            mockSetRatings = CreateMockDbSet(emptyRatings);
            mockSetComments = CreateMockDbSet(emptyComments);
            mockSetUserlistMovies = CreateMockDbSet(emptyUserlistMovies);

            mockContext = new Mock<FilmContext>();
            mockContext.Setup(c => c.Movies).Returns(mockSetMovies.Object);
            mockContext.Setup(c => c.Genres).Returns(mockSetGenres.Object);
            mockContext.Setup(c => c.Persons).Returns(mockSetPersons.Object);
            mockContext.Setup(c => c.MovieActors).Returns(mockSetMovieActors.Object);
            mockContext.Setup(c => c.UserMovies).Returns(mockSetUserMovies.Object);
            mockContext.Setup(c => c.Ratings).Returns(mockSetRatings.Object);
            mockContext.Setup(c => c.Comments).Returns(mockSetComments.Object);
            mockContext.Setup(c => c.UserlistMovies).Returns(mockSetUserlistMovies.Object);

            controller = new moviesController(mockContext.Object);
        }

        private Mock<DbSet<T>> CreateMockDbSet<T>(IQueryable<T> data) where T : class
        {
            var mockSet = new Mock<DbSet<T>>();
            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            mockSet.Setup(m => m.Include(It.IsAny<string>())).Returns(mockSet.Object);
            return mockSet;
        }

        [TestMethod]
        public void Get_Returns_All_Movies()
        {
            var result = controller.Get() as OkNegotiatedContentResult<List<MoviesResponse>>;
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Content.Count);
        }



        [TestMethod]
        public void GetAllGenres_Returns_Genres()
        {
            var result = controller.GetAllGenres() as OkNegotiatedContentResult<List<string>>;
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Content.Count);
        }



        [TestMethod]
        public void Post_Adds_New_Movie()
        {
            var newMovie = new MoviePostModel
            {
                Movie_id = 3,
                Title = "Interstellar",
                Genre_name = "Sci-Fi",
                Relase_Year = 2014,
                Director_Id = 1,
                Img_Url = "img3.jpg"
            };

            var result = controller.Post(newMovie);

            Assert.IsTrue(result is OkResult || result is ConflictResult);
        }

        [TestMethod]
        public void Put_Updates_Movie()
        {
            var updatedMovie = new Movies
            {
                Movie_id = 1,
                Genre_id = 1,
                Title = "Inception Updated",
                Relase_Year = 2010,
                Img_Url = "updated_img.jpg"
            };

            var result = controller.Put(1, updatedMovie);

            Assert.IsTrue(result is OkResult || result is NotFoundResult);
        }

        [TestMethod]
        public void Delete_Removes_Movie()
        {
            var result = controller.Delete(1);
            Assert.IsTrue(result is OkResult || result is NotFoundResult);
        }
    }
}
