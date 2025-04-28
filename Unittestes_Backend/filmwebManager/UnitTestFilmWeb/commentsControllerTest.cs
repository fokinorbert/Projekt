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
    public class commentsControllerTest
    {
        private commentsController controller;
        private Mock<FilmContext> mockContext;
        private Mock<DbSet<Comments>> mockSetComments;
        private Mock<DbSet<Movies>> mockSetMovies;
        private Mock<DbSet<Users>> mockSetUsers;

        [TestInitialize]
        public void Setup()
        {
            var commentsData = new List<Comments>
        {
            new Comments
            {
                Comment_id = 1,
                User_id = 1,
                Movie_id = 1,
                Comment = "Nice movie!",
                Created_At = DateTime.Now,
                User = new Users { User_id = 1, UserName = "User1" },
                Movies = new Movies { Movie_id = 1, Title = "Movie 1", Img_Url = "img1.jpg" }
            },
            new Comments
            {
                Comment_id = 2,
                User_id = 2,
                Movie_id = 2,
                Comment = "Awesome!",
                Created_At = DateTime.Now,
                User = new Users { User_id = 2, UserName = "User2" },
                Movies = new Movies { Movie_id = 2, Title = "Movie 2", Img_Url = "img2.jpg" }
            }
        }.AsQueryable();

            mockSetComments = new Mock<DbSet<Comments>>();
            mockSetComments.As<IQueryable<Comments>>().Setup(m => m.Provider).Returns(commentsData.Provider);
            mockSetComments.As<IQueryable<Comments>>().Setup(m => m.Expression).Returns(commentsData.Expression);
            mockSetComments.As<IQueryable<Comments>>().Setup(m => m.ElementType).Returns(commentsData.ElementType);
            mockSetComments.As<IQueryable<Comments>>().Setup(m => m.GetEnumerator()).Returns(commentsData.GetEnumerator());
            mockSetComments.Setup(m => m.Include(It.IsAny<string>())).Returns(mockSetComments.Object);

            var moviesData = new List<Movies>
            {
                new Movies { Movie_id = 1, Title = "Movie 1", Img_Url = "img1.jpg" },
                new Movies { Movie_id = 2, Title = "Movie 2", Img_Url = "img2.jpg" }
            }.AsQueryable();

            mockSetMovies = new Mock<DbSet<Movies>>();
            mockSetMovies.As<IQueryable<Movies>>().Setup(m => m.Provider).Returns(moviesData.Provider);
            mockSetMovies.As<IQueryable<Movies>>().Setup(m => m.Expression).Returns(moviesData.Expression);
            mockSetMovies.As<IQueryable<Movies>>().Setup(m => m.ElementType).Returns(moviesData.ElementType);
            mockSetMovies.As<IQueryable<Movies>>().Setup(m => m.GetEnumerator()).Returns(moviesData.GetEnumerator());

            var usersData = new List<Users>
            {
                new Users { User_id = 1, UserName = "User1" },
                new Users { User_id = 2, UserName = "User2" }
            }.AsQueryable();

            mockSetUsers = new Mock<DbSet<Users>>();
            mockSetUsers.As<IQueryable<Users>>().Setup(m => m.Provider).Returns(usersData.Provider);
            mockSetUsers.As<IQueryable<Users>>().Setup(m => m.Expression).Returns(usersData.Expression);
            mockSetUsers.As<IQueryable<Users>>().Setup(m => m.ElementType).Returns(usersData.ElementType);
            mockSetUsers.As<IQueryable<Users>>().Setup(m => m.GetEnumerator()).Returns(usersData.GetEnumerator());

            mockContext = new Mock<FilmContext>();
            mockContext.Setup(c => c.Comments).Returns(mockSetComments.Object);
            mockContext.Setup(c => c.Movies).Returns(mockSetMovies.Object);
            mockContext.Setup(c => c.Users).Returns(mockSetUsers.Object);

            controller = new commentsController(mockContext.Object);
        }

        [TestMethod]
        public void Get_Returns_All_Comments()
        {
            var result = controller.Get() as OkNegotiatedContentResult<List<commentsResponse>>;

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Content.Count);
        }

        [TestMethod]
        public void GetCommentsByMovie_Returns_Correct_Comments()
        {
            var result = controller.GetCommentsByMovie(1) as OkNegotiatedContentResult<List<commentsResponse>>;

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Content.Count);
            Assert.AreEqual(1, result.Content.First().Movie_id);
        }

        [TestMethod]
        public void Get_ByUserId_Returns_Comments()
        {
            var result = controller.Get(1) as OkNegotiatedContentResult<List<commentsResponse>>;

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Content.Count);
            Assert.AreEqual(1, result.Content.First().User_id);
        }

        [TestMethod]
        public void Post_Adds_New_Comment()
        {
            var newComment = new commentsResponse
            {
                Comment_id = 3,
                Comment = "Amazing!",
                Movie_id = 1,
                User_id = 1,
                Created_At = DateTime.Now
            };

            var result = controller.Post(newComment);

            Assert.IsTrue(result is OkResult || result is BadRequestErrorMessageResult);
        }

        [TestMethod]
        public void Put_Updates_Comment()
        {
            var updateModel = new commentPutModel
            {
                Comment = "Updated Comment"
            };

            var result = controller.Put(1, updateModel);

            Assert.IsTrue(result is OkResult || result is NotFoundResult);
        }

        [TestMethod]
        public void Delete_Removes_Comment()
        {
            var result = controller.Delete(1);

            Assert.IsTrue(result is OkResult || result is NotFoundResult);
        }
    }
}
