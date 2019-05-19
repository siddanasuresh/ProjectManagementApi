using BusinessLogic.Interfaces;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ProjectManagerApi.Controllers;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
namespace UnitTests.api
{
    public class UsersControllerTests
    {
        readonly UsersController usersController;
        Mock<IManageUser> manageUser;
        public UsersControllerTests()
        {
            manageUser = new Mock<IManageUser>();
            usersController = new UsersController(manageUser.Object);
        }
        [Fact]
        public async Task TestGetAllUsersReturnsExpectedResults()
        {
            //Arrange
            manageUser.Setup(user => user.GetAllUsers()).Returns(Task.FromResult<IEnumerable<User>>(TestData.GetUsers()));

            //Act
            var result = await usersController.GetAllUsers();

            //Assert
            Assert.NotNull(result);

            Assert.Equal(typeof(OkObjectResult), result.GetType());
            Assert.NotNull((result as OkObjectResult).Value);
            Assert.Equal(((result as OkObjectResult).Value as List<User>).Count, TestData.GetUsers().Count);
        }

        [Fact]
        public async Task TestGetUserByIdReturnsExpectedResults()
        {

            //Arrange
            var user = (TestData.GetUsers() as List<User>).Find(x => x.UserId == 33);

            manageUser.Setup(x => x.GetUser(33)).Returns(Task.FromResult<User>(user));
            
            //Act
            var result = await usersController.GetUser(33);

            //Assert
            Assert.NotNull(result);

            Assert.Equal(typeof(OkObjectResult), result.GetType());
            Assert.NotNull((result as OkObjectResult).Value);
            Assert.Equal(33, ((result as OkObjectResult).Value as User).UserId);
            Assert.Equal(user.FirstName, ((result as OkObjectResult).Value as User).FirstName);
            Assert.Equal(user.EmployeeId, ((result as OkObjectResult).Value as User).EmployeeId);
        }
        [Fact]
        public async Task TestPostMethodReturnsExpectedResults()
        {

            //Arrange
            var user = (TestData.GetUsers() as List<User>).Find(x => x.UserId == 33);

            manageUser.Setup(x => x.InsertUser(user)).Returns(Task.FromResult<int>(user.UserId));

            //Act
            var result = await usersController.Post(user);

            //Assert
            Assert.NotNull(result);

            Assert.Equal(typeof(OkObjectResult), result.GetType());
            Assert.NotNull((result as OkObjectResult).Value);
            Assert.Equal(user.UserId, (result as OkObjectResult).Value);           
        }

        [Fact]
        public async Task TestPutMethodReturnsExpectedResults()
        {

            //Arrange
            var user = (TestData.GetUsers() as List<User>).Find(x => x.UserId == 33);

            manageUser.Setup(x => x.EditUser(user)).Returns(Task.FromResult<int>(user.UserId));

            //Act
            var result = await usersController.Put(user.UserId,user);

            //Assert
            Assert.NotNull(result);
            
            Assert.Equal(typeof(OkObjectResult), result.GetType());
            Assert.NotNull((result as OkObjectResult).Value);
            Assert.Equal(user.UserId, (result as OkObjectResult).Value);           
        }

        [Fact]
        public async Task TestDeleteMethodReturnsExpectedResult()
        {

            //Arrange
            var user = (TestData.GetUsers() as List<User>).Find(x => x.UserId == 33);

            manageUser.Setup(x => x.GetUser(user.UserId)).Returns(Task.FromResult<User>(user));

            //Act
            var result = await usersController.Delete(user.UserId);

            //Assert
            Assert.NotNull(result);

            Assert.Equal(typeof(OkObjectResult), result.GetType());
            Assert.NotNull((result as OkObjectResult).Value);
            Assert.Equal(user.UserId, (result as OkObjectResult).Value);
        }
    }
}
