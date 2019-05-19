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
    public class TaskRepositoryTests
    {
        DbContextOptions<ProjectManagerApiDbContext> contextOptions;

       public TaskRepositoryTests()
        {
            contextOptions = new DbContextOptions<ProjectManagerApiDbContext>();
        }

        [Fact]
        public async Task ToVerifyTaskDetailsCount()
        {
         
            var mockContext = new Mock<ProjectManagerApiDbContext>(contextOptions);
           
            var taskRepository = new TaskRepository(mockContext.Object);

            IQueryable<TaskDetail> taskDetailsList =TestData.GetTasks().AsQueryable();

            var mockSet = new Mock<DbSet<TaskDetail>>();

            mockSet.As<IAsyncEnumerable<TaskDetail>>()
        .Setup(m => m.GetEnumerator())
        .Returns(new TestAsyncEnumerator<TaskDetail>(taskDetailsList.GetEnumerator()));

            mockSet.As<IQueryable<TaskDetail>>()
                .Setup(m => m.Provider)
                .Returns(new TestAsyncQueryProvider<TaskDetail>(taskDetailsList.Provider));

            mockSet.As<IQueryable<TaskDetail>>().Setup(m => m.Expression).Returns(taskDetailsList.Expression);
            mockSet.As<IQueryable<TaskDetail>>().Setup(m => m.ElementType).Returns(taskDetailsList.ElementType);
            mockSet.As<IQueryable<TaskDetail>>().Setup(m => m.GetEnumerator()).Returns(() => taskDetailsList.GetEnumerator());

            mockContext.Setup(m => m.Tasks).Returns(mockSet.Object);          

            var taskDetails = await taskRepository.GetAllTasks();

            Assert.Equal(4, taskDetails.Count());
        }

        [Fact]
        public async Task ReturnsNullWhenNoTaskAvailableWithGivenTaskId()
        {
            
            var mockContext = new Mock<ProjectManagerApiDbContext>(contextOptions);

            var taskRepository = new TaskRepository(mockContext.Object);

            IQueryable<TaskDetail> taskDetails = TestData.GetTasks().AsQueryable();

            var mockSet = new Mock<DbSet<TaskDetail>>();

            mockSet.As<IAsyncEnumerable<TaskDetail>>()
        .Setup(m => m.GetEnumerator())
        .Returns(new TestAsyncEnumerator<TaskDetail>(taskDetails.GetEnumerator()));

            mockSet.As<IQueryable<TaskDetail>>()
                .Setup(m => m.Provider)
                .Returns(new TestAsyncQueryProvider<TaskDetail>(taskDetails.Provider));

            mockSet.As<IQueryable<TaskDetail>>().Setup(m => m.Expression).Returns(taskDetails.Expression);
            mockSet.As<IQueryable<TaskDetail>>().Setup(m => m.ElementType).Returns(taskDetails.ElementType);
            mockSet.As<IQueryable<TaskDetail>>().Setup(m => m.GetEnumerator()).Returns(() => taskDetails.GetEnumerator());

            mockContext.Setup(m => m.Tasks).Returns(mockSet.Object);

            var taskDetail = await taskRepository.GetTask(1131);

            Assert.Null(taskDetail);          
        }

        [Fact]
        public async Task ToVerifyTaskValues()
        {
                     
            var mockContext = new Mock<ProjectManagerApiDbContext>(contextOptions);

            var taskRepository = new TaskRepository(mockContext.Object);

            IQueryable<TaskDetail> taskDetails = TestData.GetTasks().AsQueryable();

            var mockSet = new Mock<DbSet<TaskDetail>>();

            mockSet.As<IAsyncEnumerable<TaskDetail>>()
        .Setup(m => m.GetEnumerator())
        .Returns(new TestAsyncEnumerator<TaskDetail>(taskDetails.GetEnumerator()));

            mockSet.As<IQueryable<TaskDetail>>()
                .Setup(m => m.Provider)
                .Returns(new TestAsyncQueryProvider<TaskDetail>(taskDetails.Provider));

            mockSet.As<IQueryable<TaskDetail>>().Setup(m => m.Expression).Returns(taskDetails.Expression);
            mockSet.As<IQueryable<TaskDetail>>().Setup(m => m.ElementType).Returns(taskDetails.ElementType);
            mockSet.As<IQueryable<TaskDetail>>().Setup(m => m.GetEnumerator()).Returns(() => taskDetails.GetEnumerator());

            mockContext.Setup(m => m.Tasks).Returns(mockSet.Object);         

            var taskDetail = await taskRepository.GetTask(113);

            Assert.Equal(113, taskDetail.Id);
            Assert.Equal("NUNIT TESTS", taskDetail.Name);
            Assert.Null(taskDetail.ParentId);
        }

        [Fact]
        public async Task InsertTestAndCheckWhetherSavedInDatabaseOrNot()
        {
         
            var mockContext = new Mock<ProjectManagerApiDbContext>(contextOptions);

            var taskRepository = new TaskRepository(mockContext.Object);

            var taskDetail = TestData.GetTasks().Where(task => task.Id == 112).FirstOrDefault();

            var mockSet = new Mock<DbSet<TaskDetail>>();

            mockContext.Setup(m => m.Tasks).Returns(mockSet.Object);
            var result = await taskRepository.InsertTask(taskDetail);

            mockSet.Verify(m => m.Add(taskDetail), Times.Once);
            mockContext.Verify(m => m. SaveChangesAsync(System.Threading.CancellationToken.None), Times.Once);           
        }

        [Fact]
        public async Task EditTaskAndCheckWhetherSavedInDatabaseOrNot()
        {
           var mockContext = new Mock<ProjectManagerApiDbContext>(contextOptions);

            var taskRepository = new TaskRepository(mockContext.Object);

            var taskDetail = TestData.GetTasks().Where(task=>task.Id==115).FirstOrDefault();

            var mockSet = new Mock<DbSet<TaskDetail>>();

            mockContext.Setup(m => m.Tasks).Returns(mockSet.Object);
            var result = await taskRepository.EditTask(taskDetail);

            mockSet.Verify(m => m.Update(taskDetail), Times.Once);
            mockContext.Verify(m => m.SaveChangesAsync(System.Threading.CancellationToken.None), Times.Once);
        }

        [Fact]
        public async Task DeleteTaskAndCheckWhetherSavedInDatabaseOrNot()
        {
          
            var mockContext = new Mock<ProjectManagerApiDbContext>(contextOptions);

            var taskRepository = new TaskRepository(mockContext.Object);

            var taskDetail = TestData.GetTasks().Where(task => task.Id == 113).FirstOrDefault();

            var mockSet = new Mock<DbSet<TaskDetail>>();

            mockContext.Setup(m => m.Tasks).Returns(mockSet.Object);
            var result = await taskRepository.DeleteTask(taskDetail);

            mockSet.Verify(m => m.Remove(taskDetail), Times.Once);
            mockContext.Verify(m => m.SaveChangesAsync(System.Threading.CancellationToken.None), Times.Once);
        }
    }
}
