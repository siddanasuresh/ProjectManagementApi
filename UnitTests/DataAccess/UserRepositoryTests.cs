using DataAccess;
using Entities;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnitTests.DataAccess.Helpers;
using Xunit;

namespace UnitTests.DataAccess
{
    public class UserRepositoryTests
    {
        DbContextOptions<ProjectManagerApiDbContext> contextOptions;

      public  UserRepositoryTests()
        {
            contextOptions = new DbContextOptions<ProjectManagerApiDbContext>();
        }

        [Fact]
        public async Task VerifyUserDetailsCount()
        {
            var mockContext = new Mock<ProjectManagerApiDbContext>(contextOptions);

            var userRepository = new UserRepository(mockContext.Object);

            IQueryable<User> userList = TestData.GetUsers().AsQueryable();

            var mockSet = new Mock<DbSet<User>>();

            mockSet.As<IAsyncEnumerable<User>>()
        .Setup(m => m.GetEnumerator())
        .Returns(new TestAsyncEnumerator<User>(userList.GetEnumerator()));

            mockSet.As<IQueryable<User>>()
                .Setup(m => m.Provider)
                .Returns(new TestAsyncQueryProvider<User>(userList.Provider));

            mockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(userList.Expression);
            mockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(userList.ElementType);
            mockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(() => userList.GetEnumerator());

            mockContext.Setup(m => m.Users).Returns(mockSet.Object);

            var users = await userRepository.GetAllUsers();

            Assert.True(users.Count() > 0);
            Assert.Equal(6, users.Count());
        }

        [Fact(DisplayName = "Verifys User Details for given User Id When Not Available")]
        public async Task ToVerifyNotUserDetailsForGivenIdWhenNotAvailable()
        {

            var mockContext = new Mock<ProjectManagerApiDbContext>(contextOptions);

            var userRepository = new UserRepository(mockContext.Object);

            IQueryable<User> userList = TestData.GetUsers().AsQueryable();

            var mockSet = new Mock<DbSet<User>>();

            mockSet.As<IAsyncEnumerable<User>>()
        .Setup(m => m.GetEnumerator())
        .Returns(new TestAsyncEnumerator<User>(userList.GetEnumerator()));

            mockSet.As<IQueryable<User>>()
                .Setup(m => m.Provider)
                .Returns(new TestAsyncQueryProvider<User>(userList.Provider));

            mockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(userList.Expression);
            mockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(userList.ElementType);
            mockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(() => userList.GetEnumerator());

            mockContext.Setup(m => m.Users).Returns(mockSet.Object);

            var user = await userRepository.GetUser(289999);

            Assert.Null(user);
        }

        [Fact(DisplayName = "Verifys User Details for given User Id")]
        public async Task ToVerifyUserDetailsForGivenId()
        {

            var mockContext = new Mock<ProjectManagerApiDbContext>(contextOptions);

            var userRepository = new UserRepository(mockContext.Object);

            IQueryable<User> userList = TestData.GetUsers().AsQueryable();

            var mockSet = new Mock<DbSet<User>>();

            mockSet.As<IAsyncEnumerable<User>>()
        .Setup(m => m.GetEnumerator())
        .Returns(new TestAsyncEnumerator<User>(userList.GetEnumerator()));

            mockSet.As<IQueryable<User>>()
                .Setup(m => m.Provider)
                .Returns(new TestAsyncQueryProvider<User>(userList.Provider));

            mockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(userList.Expression);
            mockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(userList.ElementType);
            mockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(() => userList.GetEnumerator());

            mockContext.Setup(m => m.Users).Returns(mockSet.Object);

            var user = await userRepository.GetUser(33);

            Assert.NotNull(user);
            Assert.Equal("Naresh44", user.FirstName);
            Assert.Equal("Gr4", user.LastName);
            Assert.Equal(66, user.EmployeeId);
        }

        [Fact]
        public async Task TestInsertUserSavingInDatabase()
        {
            var mockContext = new Mock<ProjectManagerApiDbContext>(contextOptions);

            var userRepository = new UserRepository(mockContext.Object);

            var userDetail = TestData.GetUsers().Where(x => x.UserId == 12).FirstOrDefault();

            var mockSet = new Mock<DbSet<User>>();

            mockContext.Setup(m => m.Users).Returns(mockSet.Object);
            var result = await userRepository.InsertUser(userDetail);

            mockSet.Verify(m => m.Add(userDetail), Times.Once);
            mockContext.Verify(m => m.SaveChangesAsync(System.Threading.CancellationToken.None), Times.Once);
        }

        [Fact]
        public async Task TestEditUserWorksFine()
        {
            var mockContext = new Mock<ProjectManagerApiDbContext>(contextOptions);

            var userRepository = new UserRepository(mockContext.Object);

            var userDetail = TestData.GetUsers().Where(x => x.UserId == 8990).FirstOrDefault();

            var mockSet = new Mock<DbSet<User>>();

            mockContext.Setup(m => m.Users).Returns(mockSet.Object);
            var result = await userRepository.EditUser(userDetail);

            mockSet.Verify(m => m.Update(userDetail), Times.Once);
            mockContext.Verify(m => m.SaveChangesAsync(System.Threading.CancellationToken.None), Times.Once);
        }

        [Fact]
        public async Task ToTestDeleteUserWorksFine()
        {
            var mockContext = new Mock<ProjectManagerApiDbContext>(contextOptions);

            var userRepository = new UserRepository(mockContext.Object);

            var userDetail = TestData.GetUsers().Where(x => x.UserId == 34456).FirstOrDefault();

            var mockSet = new Mock<DbSet<User>>();

            mockContext.Setup(m => m.Users).Returns(mockSet.Object);
            var result = await userRepository.DeleteUser(userDetail);

            mockSet.Verify(m => m.Remove(userDetail), Times.Once);
            mockContext.Verify(m => m.SaveChangesAsync(System.Threading.CancellationToken.None), Times.Once);
        }

    }
}

