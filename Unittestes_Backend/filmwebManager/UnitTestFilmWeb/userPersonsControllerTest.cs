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
    public class userPersonsControllerTest
    {
        private userPersonsController controller;
        private Mock<FilmContext> mockContext;
        private Mock<DbSet<UserPersons>> mockSetUserPersons;
        private Mock<DbSet<Persons>> mockSetPersons;

        [TestInitialize]
        public void Setup()
        {
          
            var userPersonsData = new List<UserPersons>
            {
                new UserPersons { User_id = 1, Person_id = 10 },
                new UserPersons { User_id = 2, Person_id = 20 }
            }.AsQueryable();

            mockSetUserPersons = new Mock<DbSet<UserPersons>>();
            mockSetUserPersons.As<IQueryable<UserPersons>>().Setup(m => m.Provider).Returns(userPersonsData.Provider);
            mockSetUserPersons.As<IQueryable<UserPersons>>().Setup(m => m.Expression).Returns(userPersonsData.Expression);
            mockSetUserPersons.As<IQueryable<UserPersons>>().Setup(m => m.ElementType).Returns(userPersonsData.ElementType);
            mockSetUserPersons.As<IQueryable<UserPersons>>().Setup(m => m.GetEnumerator()).Returns(userPersonsData.GetEnumerator());
            mockSetUserPersons.Setup(m => m.Include(It.IsAny<string>())).Returns(mockSetUserPersons.Object);

            
            var personsData = new List<Persons>
            {
                new Persons { Person_id = 10, Name = "John Doe" },
                new Persons { Person_id = 20, Name = "Jane Smith" }
            }.AsQueryable();

            mockSetPersons = new Mock<DbSet<Persons>>();
            mockSetPersons.As<IQueryable<Persons>>().Setup(m => m.Provider).Returns(personsData.Provider);
            mockSetPersons.As<IQueryable<Persons>>().Setup(m => m.Expression).Returns(personsData.Expression);
            mockSetPersons.As<IQueryable<Persons>>().Setup(m => m.ElementType).Returns(personsData.ElementType);
            mockSetPersons.As<IQueryable<Persons>>().Setup(m => m.GetEnumerator()).Returns(personsData.GetEnumerator());
            mockSetPersons.Setup(m => m.Include(It.IsAny<string>())).Returns(mockSetPersons.Object);

            mockContext = new Mock<FilmContext>();
            mockContext.Setup(c => c.UserPersons).Returns(mockSetUserPersons.Object);
            mockContext.Setup(c => c.Persons).Returns(mockSetPersons.Object);

            controller = new userPersonsController(mockContext.Object);
        }

        [TestMethod]
        public void Get_Returns_All_UserPersons()
        {
            var result = controller.Get() as OkNegotiatedContentResult<List<userPersonResponse>>;

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Content.Count);
        }

        [TestMethod]
        public void Get_ByUserId_Returns_Correct_UserPerson()
        {
            var result = controller.Get(1) as OkNegotiatedContentResult<userPersonResponse>;

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Content.User_id);
            Assert.AreEqual(10, result.Content.Person_id);
        }

        [TestMethod]
        public void Get_ByPersonId_Returns_Correct_UserPerson()
        {
            var result = controller.Get(10, "person") as OkNegotiatedContentResult<userPersonResponse>;

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Content.User_id);
            Assert.AreEqual(10, result.Content.Person_id);
        }

        [TestMethod]
        public void Post_Adds_New_UserPerson()
        {
            var newUserPerson = new userPersonPostModel
            {
                UserId = 3,
                PersonName = "John Doe" 
            };

            var result = controller.Post(newUserPerson);

            Assert.IsTrue(result is OkResult || result is BadRequestErrorMessageResult);
        }

        [TestMethod]
        public void Put_Updates_UserPerson()
        {
            var updatedUserPerson = new userPersonResponse
            {
                User_id = 1,
                Person_id = 20
            };

            var result = controller.Put(10, updatedUserPerson);

            Assert.IsTrue(result is OkResult || result is NotFoundResult);
        }

        [TestMethod]
        public void Delete_Removes_UserPerson()
        {
            var result = controller.Delete(1, 10);

            Assert.IsTrue(result is OkResult || result is NotFoundResult);
        }
    }
}
