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
    public class TasksControllerTests
    {
        readonly TasksController tasksController;
        Mock<IManageTask> manageTask;
        public TasksControllerTests()
        {            
            manageTask = new Mock<IManageTask>();
            tasksController = new TasksController(manageTask.Object);
        }
        [Fact]
        public async Task TestGetAllTasksReturnsExpectedResults()
        {
            //Arrange
            manageTask.Setup(task => task.GetAllTasks()).Returns(Task.FromResult<IEnumerable<TaskDetail>>(TestData.GetTasks()));

            //Act
            var result = await tasksController.GetAllTasks();

            //Assert
            Assert.NotNull(result);

            Assert.Equal(typeof(OkObjectResult), result.GetType());
            Assert.NotNull((result as OkObjectResult).Value);
            Assert.Equal(((result as OkObjectResult).Value as List<TaskDetail>).Count, TestData.GetTasks().Count);
        }

        [Fact]
        public async Task TestGetTaskByIdReturnsExpectedResults()
        {

            //Arrange
            var task = (TestData.GetTasks() as List<TaskDetail>).Find(x => x.Id == 114);

            manageTask.Setup(x => x.GetTask(114)).Returns(Task.FromResult<TaskDetail>(task));
            
            //Act
            var result = await tasksController.GetTask(114);

            //Assert
            Assert.NotNull(result);

            Assert.Equal(typeof(OkObjectResult), result.GetType());
            Assert.NotNull((result as OkObjectResult).Value);
            Assert.Equal(114, ((result as OkObjectResult).Value as TaskDetail).Id);
            Assert.Equal(task.Name, ((result as OkObjectResult).Value as TaskDetail).Name);
        }
        [Fact]
        public async Task TestPostMethodReturnsExpectedResults()
        {

            //Arrange
            var task = (TestData.GetTasks() as List<TaskDetail>).Find(x => x.Id == 114);

            manageTask.Setup(x => x.InsertTask(task)).Returns(Task.FromResult<int>(task.Id));

            //Act
            var result = await tasksController.Post(task);

            //Assert
            Assert.NotNull(result);

            Assert.Equal(typeof(OkObjectResult), result.GetType());
            Assert.NotNull((result as OkObjectResult).Value);
            Assert.Equal(task.Id, (result as OkObjectResult).Value);           
        }

        [Fact]
        public async Task TestPutMethodReturnsExpectedResults()
        {

            //Arrange
            var task = (TestData.GetTasks() as List<TaskDetail>).Find(x => x.Id == 114);

            manageTask.Setup(x => x.EditTask(task)).Returns(Task.FromResult<int>(task.Id));

            //Act
            var result = await tasksController.Put(task.Id,task);

            //Assert
            Assert.NotNull(result);

            Assert.Equal(typeof(OkObjectResult), result.GetType());
            Assert.NotNull((result as OkObjectResult).Value);
            Assert.Equal(task.Id, (result as OkObjectResult).Value);           
        }

        [Fact]
        public async Task TestDeleteMethodReturnsExpectedResult()
        {

            //Arrange
            var task = (TestData.GetTasks() as List<TaskDetail>).Find(x => x.Id == 114);

            manageTask.Setup(x => x.GetTask(task.Id)).Returns(Task.FromResult<TaskDetail>(task));

            //Act
            var result = await tasksController.Delete(task.Id);

            //Assert
            Assert.NotNull(result);

            Assert.Equal(typeof(OkObjectResult), result.GetType());
            Assert.NotNull((result as OkObjectResult).Value);
            Assert.Equal(task.Id, (result as OkObjectResult).Value);
        }
    }
}
