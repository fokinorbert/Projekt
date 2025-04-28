using filmwebManager.Controllers;
using filmwebManager.Models;
using filmwebManager.Context;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web.Http.Results;

namespace filmwebManager.Tests.Controllers
{
    [TestClass]
    public class usersControllerTest
    {
        private usersController controller;
        private Mock<FilmContext> mockContext;

        private Mock<DbSet<Users>> mockSetUsers;
        private Mock<DbSet<UserGenres>> mockSetUserGenres;
        private Mock<DbSet<UserPersons>> mockSetUserPersons;
        private Mock<DbSet<UserMovies>> mockSetUserMovies;
        private Mock<DbSet<UserLists>> mockSetUserLists;
        private Mock<DbSet<Comments>> mockSetComments;
        private Mock<DbSet<Ratings>> mockSetRatings;
        private Mock<DbSet<Movies>> mockSetMovies;
        private Mock<DbSet<Genres>> mockSetGenres;
        private Mock<DbSet<Persons>> mockSetPersons;
        private Mock<DbSet<MovieActors>> mockSetMovieActors;

        [TestInitialize]
        public void Setup()
        {
            
            var usersData = new List<Users>
            {
                new Users { User_id = 1, UserName = "TestUser", Email = "test@example.com", Password_hash = new byte[0], Password_salt = new byte[0] }
            }.AsQueryable();

            var moviesData = new List<Movies>
            {
                new Movies { Movie_id = 1, Title = "TestMovie", Genre_id = 1, Relase_Year = 2020, Director_id = 1 }
            }.AsQueryable();

            var genresData = new List<Genres>
            {
                new Genres { Genre_id = 1, Genre_name = "Action" }
            }.AsQueryable();

            var personsData = new List<Persons>
            {
                new Persons { Person_id = 1, Name = "Test Actor", Role = "Actor" }
            }.AsQueryable();

            mockSetUsers = CreateMockDbSet(usersData);
            mockSetMovies = CreateMockDbSet(moviesData);
            mockSetGenres = CreateMockDbSet(genresData);
            mockSetPersons = CreateMockDbSet(personsData);
            mockSetUserGenres = CreateMockDbSet(new List<UserGenres>().AsQueryable());
            mockSetUserPersons = CreateMockDbSet(new List<UserPersons>().AsQueryable());
            mockSetUserMovies = CreateMockDbSet(new List<UserMovies>().AsQueryable());
            mockSetUserLists = CreateMockDbSet(new List<UserLists>().AsQueryable());
            mockSetComments = CreateMockDbSet(new List<Comments>().AsQueryable());
            mockSetRatings = CreateMockDbSet(new List<Ratings>().AsQueryable());
            mockSetMovieActors = CreateMockDbSet(new List<MovieActors>().AsQueryable());

            mockContext = new Mock<FilmContext>();
            mockContext.Setup(c => c.Users).Returns(mockSetUsers.Object);
            mockContext.Setup(c => c.Movies).Returns(mockSetMovies.Object);
            mockContext.Setup(c => c.Genres).Returns(mockSetGenres.Object);
            mockContext.Setup(c => c.Persons).Returns(mockSetPersons.Object);
            mockContext.Setup(c => c.UserGenres).Returns(mockSetUserGenres.Object);
            mockContext.Setup(c => c.UserPersons).Returns(mockSetUserPersons.Object);
            mockContext.Setup(c => c.UserMovies).Returns(mockSetUserMovies.Object);
            mockContext.Setup(c => c.UserLists).Returns(mockSetUserLists.Object);
            mockContext.Setup(c => c.Comments).Returns(mockSetComments.Object);
            mockContext.Setup(c => c.Ratings).Returns(mockSetRatings.Object);
            mockContext.Setup(c => c.MovieActors).Returns(mockSetMovieActors.Object);

            controller = new usersController(mockContext.Object);
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
        public void AddStatus_Adds_Status()
        {
            var req = new FavoriteRequest
            {
                UserId = 1,
                MovieId = 1,
                Status = "favorite"
            };

            var result = controller.AddStatus(req);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void RemoveStatus_Removes_Status()
        {
            var result = controller.RemoveStatus(1, 1, "favorite");

            Assert.IsNotNull(result);
        }


        [TestMethod]
        public void Post_Register_New_User()
        {
            var register = new UsersPost
            {
                UserName = "NewUser",
                Password = "Password123",
                Email = "newuser@example.com"
            };

            var result = controller.Post(register);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Login_User_Success()
        {
            var login = new UsersPost
            {
                Email = "test@example.com",
                Password = "Password123"
            };

            var result = controller.Login(login);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Put_Updates_User()
        {
            var update = new UsersPost
            {
                UserName = "UpdatedUser",
                Password = "NewPassword",
                Email = "updated@example.com"
            };

            var result = controller.Put(1, update);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Patch_Updates_UserName_Or_Password()
        {
            var patch = new UserPatchModel
            {
                UserName = "PatchUser",
                Password = "PatchPassword"
            };

            var result = controller.Patch("test@example.com", patch);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Delete_Removes_User()
        {
            var result = controller.Delete(1);

            Assert.IsNotNull(result);
        }
    }
}
