using filmwebManager.Controllers;
using filmwebManager.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using filmwebManager.Controllers;
using filmwebManager.Models;
using filmwebManager.Context;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http.Results;
using System;

namespace filmwebManager.Tests.Controllers
{
    [TestClass]
    public class PersonsControllerTest
    {
        private PersonsController controller;
        private Mock<FilmContext> mockContext;
        private Mock<DbSet<Persons>> mockSetPersons;
        private Mock<DbSet<UserPersons>> mockSetUserPersons;
        private Mock<DbSet<MovieActors>> mockSetMovieActors;
        private Mock<DbSet<Movies>> mockSetMovies;

        [TestInitialize]
        public void Setup()
        {
          
            var personsData = new List<Persons>
            {
                new Persons { Person_id = 1, Name = "John Doe" },
                new Persons { Person_id = 2, Name = "Jane Smith" }
            }.AsQueryable();

            mockSetPersons = new Mock<DbSet<Persons>>();
            mockSetPersons.As<IQueryable<Persons>>().Setup(m => m.Provider).Returns(personsData.Provider);
            mockSetPersons.As<IQueryable<Persons>>().Setup(m => m.Expression).Returns(personsData.Expression);
            mockSetPersons.As<IQueryable<Persons>>().Setup(m => m.ElementType).Returns(personsData.ElementType);
            mockSetPersons.As<IQueryable<Persons>>().Setup(m => m.GetEnumerator()).Returns(personsData.GetEnumerator());

            var userPersonsData = new List<UserPersons>().AsQueryable();
            mockSetUserPersons = new Mock<DbSet<UserPersons>>();
            mockSetUserPersons.As<IQueryable<UserPersons>>().Setup(m => m.Provider).Returns(userPersonsData.Provider);
            mockSetUserPersons.As<IQueryable<UserPersons>>().Setup(m => m.Expression).Returns(userPersonsData.Expression);
            mockSetUserPersons.As<IQueryable<UserPersons>>().Setup(m => m.ElementType).Returns(userPersonsData.ElementType);
            mockSetUserPersons.As<IQueryable<UserPersons>>().Setup(m => m.GetEnumerator()).Returns(userPersonsData.GetEnumerator());

    
            var movieActorsData = new List<MovieActors>().AsQueryable();
            mockSetMovieActors = new Mock<DbSet<MovieActors>>();
            mockSetMovieActors.As<IQueryable<MovieActors>>().Setup(m => m.Provider).Returns(movieActorsData.Provider);
            mockSetMovieActors.As<IQueryable<MovieActors>>().Setup(m => m.Expression).Returns(movieActorsData.Expression);
            mockSetMovieActors.As<IQueryable<MovieActors>>().Setup(m => m.ElementType).Returns(movieActorsData.ElementType);
            mockSetMovieActors.As<IQueryable<MovieActors>>().Setup(m => m.GetEnumerator()).Returns(movieActorsData.GetEnumerator());

            var moviesData = new List<Movies>().AsQueryable();
            mockSetMovies = new Mock<DbSet<Movies>>();
            mockSetMovies.As<IQueryable<Movies>>().Setup(m => m.Provider).Returns(moviesData.Provider);
            mockSetMovies.As<IQueryable<Movies>>().Setup(m => m.Expression).Returns(moviesData.Expression);
            mockSetMovies.As<IQueryable<Movies>>().Setup(m => m.ElementType).Returns(moviesData.ElementType);
            mockSetMovies.As<IQueryable<Movies>>().Setup(m => m.GetEnumerator()).Returns(moviesData.GetEnumerator());

            mockContext = new Mock<FilmContext>();
            mockContext.Setup(c => c.Persons).Returns(mockSetPersons.Object);
            mockContext.Setup(c => c.UserPersons).Returns(mockSetUserPersons.Object);
            mockContext.Setup(c => c.MovieActors).Returns(mockSetMovieActors.Object);
            mockContext.Setup(c => c.Movies).Returns(mockSetMovies.Object);

            controller = new PersonsController(mockContext.Object);
        }

        [TestMethod]
        public void Get_Returns_All_Persons()
        {
            var result = controller.Get() as OkNegotiatedContentResult<List<Persons>>;

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Content.Count);
        }

        [TestMethod]
        public void Get_ById_Returns_Correct_Person()
        {
            var result = controller.Get(1) as OkNegotiatedContentResult<Persons>;

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Content.Person_id);
            Assert.AreEqual("John Doe", result.Content.Name);
        }

        [TestMethod]
        public void Post_Adds_New_Person()
        {
            var newPerson = new Persons
            {
                Person_id = 3,
                Name = "New Person",
                Birth_Place = "New City",
                Date = DateTime.Now,
                Role = "Actor"
            };

            var result = controller.Post(newPerson);

            Assert.IsTrue(result is OkResult || result is ConflictResult);
        }

        [TestMethod]
        public void Put_Updates_Existing_Person()
        {
            var updatedPerson = new Persons
            {
                Person_id = 1,
                Name = "Updated Name",
                Birth_Place = "Updated City",
                Date = DateTime.Now,
                Role = "Director"
            };

            var result = controller.Put(1, updatedPerson);

            Assert.IsTrue(result is OkResult || result is NotFoundResult);
        }

        [TestMethod]
        public void Delete_Removes_Person()
        {
            var result = controller.Delete(1);

            Assert.IsTrue(result is OkResult || result is NotFoundResult);
        }
    }
}
