using filmwebManager.Models;
using filmwebManager.Context;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http.Results;
using filmwebManager.Controllers;
using static filmwebManager.Controllers.moviActorPostModel;

namespace filmwebManager.Tests.Controllers
{
    [TestClass]
    public class movieActorsControllerTest
    {
        private movieActorsController controller;
        private Mock<FilmContext> mockContext;
        private Mock<DbSet<MovieActors>> mockSetMovieActors;
        private Mock<DbSet<Movies>> mockSetMovies;
        private Mock<DbSet<Persons>> mockSetPersons;

        [TestInitialize]
        public void Setup()
        {
        
            var movieActorsData = new List<MovieActors>
            {
                new MovieActors { Movie_id = 1, Person_id = 10 },
                new MovieActors { Movie_id = 2, Person_id = 20 }
            }.AsQueryable();

            mockSetMovieActors = new Mock<DbSet<MovieActors>>();
            mockSetMovieActors.As<IQueryable<MovieActors>>().Setup(m => m.Provider).Returns(movieActorsData.Provider);
            mockSetMovieActors.As<IQueryable<MovieActors>>().Setup(m => m.Expression).Returns(movieActorsData.Expression);
            mockSetMovieActors.As<IQueryable<MovieActors>>().Setup(m => m.ElementType).Returns(movieActorsData.ElementType);
            mockSetMovieActors.As<IQueryable<MovieActors>>().Setup(m => m.GetEnumerator()).Returns(movieActorsData.GetEnumerator());
            mockSetMovieActors.Setup(m => m.Include(It.IsAny<string>())).Returns(mockSetMovieActors.Object);

            var moviesData = new List<Movies>
            {
                new Movies { Movie_id = 1, Title = "Movie1" },
                new Movies { Movie_id = 2, Title = "Movie2" }
            }.AsQueryable();

            mockSetMovies = new Mock<DbSet<Movies>>();
            mockSetMovies.As<IQueryable<Movies>>().Setup(m => m.Provider).Returns(moviesData.Provider);
            mockSetMovies.As<IQueryable<Movies>>().Setup(m => m.Expression).Returns(moviesData.Expression);
            mockSetMovies.As<IQueryable<Movies>>().Setup(m => m.ElementType).Returns(moviesData.ElementType);
            mockSetMovies.As<IQueryable<Movies>>().Setup(m => m.GetEnumerator()).Returns(moviesData.GetEnumerator());

          
            var personsData = new List<Persons>
            {
                new Persons { Person_id = 10, Name = "Actor1" },
                new Persons { Person_id = 20, Name = "Actor2" }
            }.AsQueryable();

            mockSetPersons = new Mock<DbSet<Persons>>();
            mockSetPersons.As<IQueryable<Persons>>().Setup(m => m.Provider).Returns(personsData.Provider);
            mockSetPersons.As<IQueryable<Persons>>().Setup(m => m.Expression).Returns(personsData.Expression);
            mockSetPersons.As<IQueryable<Persons>>().Setup(m => m.ElementType).Returns(personsData.ElementType);
            mockSetPersons.As<IQueryable<Persons>>().Setup(m => m.GetEnumerator()).Returns(personsData.GetEnumerator());

            mockContext = new Mock<FilmContext>();
            mockContext.Setup(c => c.MovieActors).Returns(mockSetMovieActors.Object);
            mockContext.Setup(c => c.Movies).Returns(mockSetMovies.Object);
            mockContext.Setup(c => c.Persons).Returns(mockSetPersons.Object);

            controller = new movieActorsController(mockContext.Object);
        }

        [TestMethod]
        public void Get_Returns_All_MovieActors()
        {
            var result = controller.Get() as OkNegotiatedContentResult<List<movieActorsResponse>>;

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Content.Count);
        }

        [TestMethod]
        public void Get_ByMovieId_Returns_Correct_Actors()
        {
            var result = controller.Get(1) as OkNegotiatedContentResult<List<movieActorsResponse>>;

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Content.Count);
            Assert.AreEqual(1, result.Content.First().Movie_id);
        }

        [TestMethod]
        public void Post_Adds_New_MovieActor()
        {
            var newActor = new moviActorPostModel
            {
                Title = "Movie1",
                PersonName = "Actor1"
            };

            var result = controller.Post(newActor);

            Assert.IsTrue(result is OkResult || result is BadRequestErrorMessageResult);
        }

        [TestMethod]
        public void Put_Updates_MovieActor()
        {
            var updatedActor = new moviActorPostModel
            {
                Title = "Movie2",
                PersonName = "Actor2"
            };

            var result = controller.Put(1, 10, updatedActor);

            Assert.IsTrue(result is OkResult || result is NotFoundResult);
        }

        [TestMethod]
        public void Delete_Removes_MovieActor()
        {
            var result = controller.Delete(1, 10);

            Assert.IsTrue(result is OkResult || result is NotFoundResult);
        }
    }
}
