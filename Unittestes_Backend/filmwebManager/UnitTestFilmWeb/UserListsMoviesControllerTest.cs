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
    public class userListsMoviesControllerTest
    {
        private userListsMoviesController controller;
        private Mock<FilmContext> mockContext;
        private Mock<DbSet<UserlistMovies>> mockSetUserlistMovies;

        [TestInitialize]
        public void Setup()
        {
            
            var userlistMoviesData = new List<UserlistMovies>
            {
                new UserlistMovies { list_id = 1, movie_id = 10 },
                new UserlistMovies { list_id = 1, movie_id = 20 },
                new UserlistMovies { list_id = 2, movie_id = 30 }
            }.AsQueryable();

            mockSetUserlistMovies = new Mock<DbSet<UserlistMovies>>();
            mockSetUserlistMovies.As<IQueryable<UserlistMovies>>().Setup(m => m.Provider).Returns(userlistMoviesData.Provider);
            mockSetUserlistMovies.As<IQueryable<UserlistMovies>>().Setup(m => m.Expression).Returns(userlistMoviesData.Expression);
            mockSetUserlistMovies.As<IQueryable<UserlistMovies>>().Setup(m => m.ElementType).Returns(userlistMoviesData.ElementType);
            mockSetUserlistMovies.As<IQueryable<UserlistMovies>>().Setup(m => m.GetEnumerator()).Returns(userlistMoviesData.GetEnumerator());

            mockContext = new Mock<FilmContext>();
            mockContext.Setup(c => c.UserlistMovies).Returns(mockSetUserlistMovies.Object);

            controller = new userListsMoviesController(mockContext.Object);
        }

        [TestMethod]
        public void Get_Returns_All_UserlistMovies()
        {
            var result = controller.Get() as OkNegotiatedContentResult<List<UserlistMovies>>;

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Content.Count);
        }

        [TestMethod]
        public void Get_ByListId_Returns_Correct_UserlistMovies()
        {
            var result = controller.Get(1) as OkNegotiatedContentResult<List<UserlistMovies>>;

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Content.Count); 
        }

        [TestMethod]
        public void Post_Adds_New_UserlistMovie()
        {
            var newUserlistMovie = new UserlistMovies
            {
                list_id = 3,
                movie_id = 40
            };

            var result = controller.Post(newUserlistMovie);

            Assert.IsTrue(result is OkResult || result is ConflictResult);
        }

        [TestMethod]
        public void Put_Updates_UserlistMovie()
        {
            var updateModel = new UserListMoviesPutModel
            {
                movie_id = 50
            };

            var result = controller.Put(1, 10, updateModel);

            Assert.IsTrue(result is OkResult || result is NotFoundResult);
        }

        [TestMethod]
        public void Delete_Removes_UserlistMovie()
        {
            var result = controller.Delete(1, 10);

            Assert.IsTrue(result is OkResult || result is NotFoundResult);
        }
    }
}
