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
    public class ParentTaskDetailsControllerTests
    {
        readonly ParentTaskDetailsController parentTaskDetailsController;
        Mock<IManageParentTaskDetails> manageTask;
        public ParentTaskDetailsControllerTests()
        {            
            manageTask = new Mock<IManageParentTaskDetails>();
            parentTaskDetailsController = new ParentTaskDetailsController(manageTask.Object);
        }
        [Fact]
        public async Task TestGetAllTasksReturnsExpectedResults()
        {
            //Arrange
            manageTask.Setup(task => task.GetAll()).Returns(Task.FromResult<IEnumerable<ParentTaskDetails>>(TestData.GetParentTaskDetails()));

            //Act
            var result = await parentTaskDetailsController.GetAll();

            //Assert
            Assert.NotNull(result);
                
            Assert.Equal(typeof(OkObjectResult), result.GetType());
            Assert.NotNull((result as OkObjectResult).Value);
            Assert.Equal(((result as OkObjectResult).Value as List<ParentTaskDetails>).Count, TestData.GetParentTaskDetails().Count);
        }

        [Fact]
        public async Task TestGetReturnsExpectedResults()
        {

            //Arrange
            var parentTaskDetail = ((TestData.GetParentTaskDetails()) as List<ParentTaskDetails>).Find(x => x.ParentId == 112);


            manageTask.Setup(x => x.Get(112)).Returns(Task.FromResult<ParentTaskDetails>(parentTaskDetail));
            
            //Act
            var result = await parentTaskDetailsController.Get(112);

            //Assert
            Assert.NotNull(result);

            Assert.Equal(typeof(OkObjectResult), result.GetType());
            Assert.NotNull((result as OkObjectResult).Value);
            Assert.Equal(112, ((result as OkObjectResult).Value as ParentTaskDetails).ParentId);
            Assert.Equal(parentTaskDetail.ParentTask, ((result as OkObjectResult).Value as ParentTaskDetails).ParentTask);
        }        

        [Fact]
        public async Task TestDeleteMethodReturnsExpectedResult()
        {

            //Arrange
            var parentTaskDetail = (TestData.GetParentTaskDetails() as List<ParentTaskDetails>).Find(x => x.ParentId == 112);

            manageTask.Setup(x => x.Get(parentTaskDetail.ParentId)).Returns(Task.FromResult<ParentTaskDetails>(parentTaskDetail));

            //Act
            var result = await parentTaskDetailsController.Delete(parentTaskDetail.ParentId);

            //Assert
            Assert.NotNull(result);

            Assert.Equal(typeof(OkObjectResult), result.GetType());
            Assert.NotNull((result as OkObjectResult).Value);
            Assert.Equal(parentTaskDetail.ParentId, (result as OkObjectResult).Value);
        }
    }
}
