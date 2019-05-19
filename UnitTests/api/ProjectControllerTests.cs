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
    public class ProjectsControllerTests
    {
        readonly ProjectsController projectsController;
        Mock<IManageProject> manageProject;
        public ProjectsControllerTests()
        {            
            manageProject = new Mock<IManageProject>();
            projectsController = new ProjectsController(manageProject.Object);
        }
        [Fact]
        public async Task TestGetAllPojectsReturnsExpectedResults()
        {
            //Arrange
            manageProject.Setup(project => project.GetAllProjects()).Returns(Task.FromResult<IEnumerable<Project>>(TestData.GetProjects()));

            //Act
            var result = await projectsController.GetAllProjects();

            //Assert
            Assert.NotNull(result);

            Assert.Equal(typeof(OkObjectResult), result.GetType());
            Assert.NotNull((result as OkObjectResult).Value);
            Assert.Equal(((result as OkObjectResult).Value as List<Project>).Count, TestData.GetProjects().Count);
        }

        [Fact]
        public async Task TestGetPojectByIdReturnsExpectedResults()
        {

            //Arrange
            var project = (TestData.GetProjects() as List<Project>).Find(x => x.ProjectId == 112);

            manageProject.Setup(x => x.GetProject(112)).Returns(Task.FromResult<Project>(project));
            
            //Act
            var result = await projectsController.GetProject(112);

            //Assert
            Assert.NotNull(result);

            Assert.Equal(typeof(OkObjectResult), result.GetType());
            Assert.NotNull((result as OkObjectResult).Value);
            Assert.Equal(112, ((result as OkObjectResult).Value as Project).ProjectId);
            Assert.Equal(project.ProjectName, ((result as OkObjectResult).Value as Project).ProjectName);
        }
        [Fact]
        public async Task TestPostMethodReturnsExpectedResults()
        {

            //Arrange
            var project = (TestData.GetProjects() as List<Project>).Find(x => x.ProjectId == 112);

            manageProject.Setup(x => x.InsertProject(project)).Returns(Task.FromResult<int>(project.ProjectId));

            //Act
            var result = await projectsController.Post(project);

            //Assert
            Assert.NotNull(result);

            Assert.Equal(typeof(OkObjectResult), result.GetType());
            Assert.NotNull((result as OkObjectResult).Value);
            Assert.Equal(project.ProjectId, (result as OkObjectResult).Value);           
        }

        [Fact]
        public async Task TestPutMethodReturnsExpectedResults()
        {

            //Arrange
            var project = (TestData.GetProjects() as List<Project>).Find(x => x.ProjectId == 115);

            manageProject.Setup(x => x.EditProject(project)).Returns(Task.FromResult<int>(project.ProjectId));

            //Act
            var result = await projectsController.Put(project.ProjectId,project);

            //Assert
            Assert.NotNull(result);

            Assert.Equal(typeof(OkObjectResult), result.GetType());
            Assert.NotNull((result as OkObjectResult).Value);
            Assert.Equal(project.ProjectId, (result as OkObjectResult).Value);           
        }

        [Fact]
        public async Task TestDeleteMethodReturnsExpectedResult()
        {

            //Arrange
            var project = (TestData.GetProjects() as List<Project>).Find(x => x.ProjectId == 115);

            manageProject.Setup(x => x.GetProject(project.ProjectId)).Returns(Task.FromResult<Project>(project));

            //Act
            var result = await projectsController.Delete(project.ProjectId);

            //Assert
            Assert.NotNull(result);

            Assert.Equal(typeof(OkObjectResult), result.GetType());
            Assert.NotNull((result as OkObjectResult).Value);
            Assert.Equal(project.ProjectId, (result as OkObjectResult).Value);
        }
    }
}
