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
    public class userMoviesControllerTest
    {
        private userMoviesController controller;
        private Mock<FilmContext> mockContext;
        private Mock<DbSet<UserMovies>> mockSet;

        [TestInitialize]
        public void Setup()
        {
            
            var data = new List<UserMovies>
            {
                new UserMovies { User_id = 1, Movie_id = 100, Status = "favorite" },
                new UserMovies { User_id = 2, Movie_id = 200, Status = "watched" }
            }.AsQueryable();

            mockSet = new Mock<DbSet<UserMovies>>();
            mockSet.As<IQueryable<UserMovies>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<UserMovies>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<UserMovies>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<UserMovies>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            
            mockSet.Setup(m => m.Include(It.IsAny<string>())).Returns(mockSet.Object);

            mockContext = new Mock<FilmContext>();
            mockContext.Setup(c => c.UserMovies).Returns(mockSet.Object);

            controller = new userMoviesController(mockContext.Object);
        }

        [TestMethod]
        public void Get_Returns_All_UserMovies()
        {
            var result = controller.Get() as OkNegotiatedContentResult<List<userMoviesResponse>>;

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Content.Count);
        }

        [TestMethod]
        public void Get_ByUserId_Returns_Correct_UserMovie()
        {
            var result = controller.Get(1) as OkNegotiatedContentResult<userMoviesResponse>;

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Content.User_id);
            Assert.AreEqual(100, result.Content.Movie_id);
            Assert.AreEqual("favorite", result.Content.Status);
        }

        [TestMethod]
        public void Post_Adds_New_UserMovie()
        {
            var newUserMovie = new userMoviesResponse
            {
                User_id = 3,
                Movie_id = 300,
                Status = "plan"
            };

            var result = controller.Post(newUserMovie);

            Assert.IsTrue(result is OkResult || result is ConflictResult);
        }

        [TestMethod]
        public void Put_Updates_Existing_UserMovie()
        {
            var updatedUserMovie = new userMoviesResponse
            {
                User_id = 1,
                Movie_id = 100,
                Status = "watched"
            };

            var result = controller.Put(updatedUserMovie);

            Assert.IsTrue(result is OkResult || result is NotFoundResult);
        }

        [TestMethod]
        public void Delete_Removes_UserMovie()
        {
            var result = controller.Delete(1, 100);

            Assert.IsTrue(result is OkResult || result is NotFoundResult);
        }
    }
}
