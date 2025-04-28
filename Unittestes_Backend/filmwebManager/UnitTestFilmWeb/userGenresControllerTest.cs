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
    public class userGenresControllerTest
    {
        private userGenresController controller;
        private Mock<FilmContext> mockContext;
        private Mock<DbSet<UserGenres>> mockSet;

        [TestInitialize]
        public void Setup()
        {
            
            var data = new List<UserGenres>
    {
        new UserGenres { User_id = 1, Genre_id = 10 },
        new UserGenres { User_id = 2, Genre_id = 20 }
    }.AsQueryable();

            mockSet = new Mock<DbSet<UserGenres>>();

            mockSet.As<IQueryable<UserGenres>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<UserGenres>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<UserGenres>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<UserGenres>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            
            mockSet.Setup(m => m.Include(It.IsAny<string>())).Returns(mockSet.Object);

            mockContext = new Mock<FilmContext>();
            mockContext.Setup(c => c.UserGenres).Returns(mockSet.Object);

            controller = new userGenresController(mockContext.Object);
        }


        [TestMethod]
        public void Get_Returns_All_UserGenres()
        {
            var result = controller.Get() as OkNegotiatedContentResult<List<userGenreResponse>>;

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Content.Count);
            Assert.AreEqual(1, result.Content[0].User_id);
            Assert.AreEqual(10, result.Content[0].Genre_id);
        }

        [TestMethod]
        public void Get_ById_Returns_Correct_UserGenre()
        {
            var result = controller.Get(1) as OkNegotiatedContentResult<List<userGenreResponse>>;

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Content.First().User_id);
            Assert.AreEqual(10, result.Content.First().Genre_id);
        }

        [TestMethod]
        public void Post_Adds_New_UserGenre()
        {
            var newUserGenre = new userGenreResponse
            {
                User_id = 3,
                Genre_id = 30
            };

            var result = controller.Post(newUserGenre);

            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        [TestMethod]
        public void Put_Updates_Existing_UserGenre()
        {
            var updatedUserGenre = new userGenreResponse
            {
                User_id = 1,
                Genre_id = 99
            };

            var result = controller.Put(1, updatedUserGenre);

            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        [TestMethod]
        public void Delete_Removes_UserGenre()
        {
            var result = controller.Delete(1);

            Assert.IsInstanceOfType(result, typeof(OkResult));
        }
    }
}
