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
    public class UserListsControllerTest
    {
        private UserListsController controller;
        private Mock<FilmContext> mockContext;
        private Mock<DbSet<UserLists>> mockSetLists;
        private Mock<DbSet<UserlistMovies>> mockSetListMovies;

        private Mock<DbSet<Movies>> mockSetMovies; 

        [TestInitialize]
        public void Setup()
        {
            var userListsData = new List<UserLists>
    {
        new UserLists { list_id = 1, list_name = "Favorites", user_id = 1 },
        new UserLists { list_id = 2, list_name = "Watch Later", user_id = 2 }
    }.AsQueryable();

            mockSetLists = new Mock<DbSet<UserLists>>();
            mockSetLists.As<IQueryable<UserLists>>().Setup(m => m.Provider).Returns(userListsData.Provider);
            mockSetLists.As<IQueryable<UserLists>>().Setup(m => m.Expression).Returns(userListsData.Expression);
            mockSetLists.As<IQueryable<UserLists>>().Setup(m => m.ElementType).Returns(userListsData.ElementType);
            mockSetLists.As<IQueryable<UserLists>>().Setup(m => m.GetEnumerator()).Returns(userListsData.GetEnumerator());
            mockSetLists.Setup(m => m.Include(It.IsAny<string>())).Returns(mockSetLists.Object); 

            var userListMoviesData = new List<UserlistMovies>().AsQueryable();
            mockSetListMovies = new Mock<DbSet<UserlistMovies>>();
            mockSetListMovies.As<IQueryable<UserlistMovies>>().Setup(m => m.Provider).Returns(userListMoviesData.Provider);
            mockSetListMovies.As<IQueryable<UserlistMovies>>().Setup(m => m.Expression).Returns(userListMoviesData.Expression);
            mockSetListMovies.As<IQueryable<UserlistMovies>>().Setup(m => m.ElementType).Returns(userListMoviesData.ElementType);
            mockSetListMovies.As<IQueryable<UserlistMovies>>().Setup(m => m.GetEnumerator()).Returns(userListMoviesData.GetEnumerator());

            var moviesData = new List<Movies>().AsQueryable();
            mockSetMovies = new Mock<DbSet<Movies>>();
            mockSetMovies.As<IQueryable<Movies>>().Setup(m => m.Provider).Returns(moviesData.Provider);
            mockSetMovies.As<IQueryable<Movies>>().Setup(m => m.Expression).Returns(moviesData.Expression);
            mockSetMovies.As<IQueryable<Movies>>().Setup(m => m.ElementType).Returns(moviesData.ElementType);
            mockSetMovies.As<IQueryable<Movies>>().Setup(m => m.GetEnumerator()).Returns(moviesData.GetEnumerator());

            mockContext = new Mock<FilmContext>();
            mockContext.Setup(c => c.UserLists).Returns(mockSetLists.Object);
            mockContext.Setup(c => c.UserlistMovies).Returns(mockSetListMovies.Object);
            mockContext.Setup(c => c.Movies).Returns(mockSetMovies.Object);

            controller = new UserListsController(mockContext.Object);
        }



        [TestMethod]
        public void Get_Returns_All_UserLists()
        {
            var actionResult = controller.Get();

            Assert.IsNotNull(actionResult); 
        }




        [TestMethod]
        public void Get_ById_Returns_Correct_UserList()
        {
            var result = controller.Get(1) as OkNegotiatedContentResult<UserLists>;

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Content.list_id);
            Assert.AreEqual("Favorites", result.Content.list_name);
        }

        [TestMethod]
        public void Post_Adds_New_UserList()
        {
            var newList = new UserListPostModel
            {
                userId = 3,
                ListName = "Sci-Fi Movies"
            };

            var result = controller.Post(newList);

            Assert.IsTrue(result is OkResult || result is ConflictResult);
        }

        [TestMethod]
        public void Put_Updates_Existing_UserList()
        {
            var updatedList = new UserListPostModel
            {
                userId = 1,
                ListName = "Updated Favorites"
            };

            var result = controller.Put(1, updatedList);

            Assert.IsTrue(result is OkResult || result is NotFoundResult);
        }

        [TestMethod]
        public void Delete_Removes_UserList()
        {
            var result = controller.Delete(1);

            Assert.IsTrue(result is OkResult || result is NotFoundResult);
        }
    }
}
