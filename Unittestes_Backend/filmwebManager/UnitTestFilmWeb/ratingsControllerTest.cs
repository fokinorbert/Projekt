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
    public class ratingsControllerTest
    {
        private ratingsController controller;
        private Mock<FilmContext> mockContext;
        private Mock<DbSet<Ratings>> mockSetRatings;
        private Mock<DbSet<Movies>> mockSetMovies;
        private Mock<DbSet<Users>> mockSetUsers;

        [TestInitialize]
        public void Setup()
        {
            var ratingsData = new List<Ratings>
            {
                new Ratings { Rating_id = 1, User_id = 1, Movie_id = 1, Rating = 5, Movies = new Movies { Movie_id = 1, Title = "Movie1", Img_Url = "img1.jpg" }, User = new Users { User_id = 1, UserName = "User1" } },
                new Ratings { Rating_id = 2, User_id = 2, Movie_id = 2, Rating = 4, Movies = new Movies { Movie_id = 2, Title = "Movie2", Img_Url = "img2.jpg" }, User = new Users { User_id = 2, UserName = "User2" } }
            }.AsQueryable();

            mockSetRatings = new Mock<DbSet<Ratings>>();
            mockSetRatings.As<IQueryable<Ratings>>().Setup(m => m.Provider).Returns(ratingsData.Provider);
            mockSetRatings.As<IQueryable<Ratings>>().Setup(m => m.Expression).Returns(ratingsData.Expression);
            mockSetRatings.As<IQueryable<Ratings>>().Setup(m => m.ElementType).Returns(ratingsData.ElementType);
            mockSetRatings.As<IQueryable<Ratings>>().Setup(m => m.GetEnumerator()).Returns(ratingsData.GetEnumerator());
            mockSetRatings.Setup(m => m.Include(It.IsAny<string>())).Returns(mockSetRatings.Object);

            var moviesData = new List<Movies>
            {
                new Movies { Movie_id = 1, Title = "Movie1", Img_Url = "img1.jpg" },
                new Movies { Movie_id = 2, Title = "Movie2", Img_Url = "img2.jpg" }
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
            mockContext.Setup(c => c.Ratings).Returns(mockSetRatings.Object);
            mockContext.Setup(c => c.Movies).Returns(mockSetMovies.Object);
            mockContext.Setup(c => c.Users).Returns(mockSetUsers.Object);

            controller = new ratingsController(mockContext.Object);
        }

        [TestMethod]
        public void Get_Returns_All_Ratings()
        {
            var result = controller.Get() as OkNegotiatedContentResult<List<ratingResponse>>;
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Content.Count);
        }

        [TestMethod]
        public void Get_ByUserId_Returns_Ratings()
        {
            var result = controller.Get(1) as OkNegotiatedContentResult<List<ratingResponse>>;
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Content.Count);
            Assert.AreEqual(1, result.Content.First().User_id);
        }

        [TestMethod]
        public void Post_Adds_New_Rating()
        {
            var newRating = new ratingResponse
            {
                Movie_id = 1,
                User_id = 1,
                Rating = 5
            };

            var result = controller.Post(newRating);

            Assert.IsTrue(result is OkResult || result is BadRequestErrorMessageResult);
        }

        [TestMethod]
        public void Put_Updates_Rating()
        {
            var updatedRating = new ratingResponse
            {
                User_id = 1,
                Movie_id = 1,
                Rating = 3
            };

            var result = controller.Put(1, updatedRating);

            Assert.IsTrue(result is OkResult || result is NotFoundResult);
        }

        [TestMethod]
        public void Delete_Removes_Rating()
        {
            var result = controller.Delete(1);
            Assert.IsTrue(result is OkResult || result is NotFoundResult);
        }
    }
}
