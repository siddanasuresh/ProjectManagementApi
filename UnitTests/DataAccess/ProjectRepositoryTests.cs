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
    public class ProjectRepositoryTests
    {
         DbContextOptions<ProjectManagerApiDbContext> contextOptions;     
        
       public ProjectRepositoryTests()
        {
            contextOptions = new DbContextOptions<ProjectManagerApiDbContext>();
        }

        [Fact]       
        public async Task TestProjectsDataInDatabase()
        {           
            var mockContext = new Mock<ProjectManagerApiDbContext>(contextOptions);

            var projectRepository = new ProjectRepository(mockContext.Object);

            IQueryable<Project> projectList = TestData.GetProjects().AsQueryable();

            var mockSet = new Mock<DbSet<Project>>();

            mockSet.As<IAsyncEnumerable<Project>>()
        .Setup(m => m.GetEnumerator())
        .Returns(new TestAsyncEnumerator<Project>(projectList.GetEnumerator()));

            mockSet.As<IQueryable<Project>>()
                .Setup(m => m.Provider)
                .Returns(new TestAsyncQueryProvider<Project>(projectList.Provider));

            mockSet.As<IQueryable<Project>>().Setup(m => m.Expression).Returns(projectList.Expression);
            mockSet.As<IQueryable<Project>>().Setup(m => m.ElementType).Returns(projectList.ElementType);
            mockSet.As<IQueryable<Project>>().Setup(m => m.GetEnumerator()).Returns(() => projectList.GetEnumerator());

            mockContext.Setup(m => m.Projects).Returns(mockSet.Object);
           
            var Projects = await projectRepository.GetAllProjects();

            Assert.Equal(6, Projects.Count());
        }

        [Fact]
        public async Task ToCheckProjectReturnedWhenNotAvailableInDatabase()
        {
            var mockContext = new Mock<ProjectManagerApiDbContext>(contextOptions);

            var projectRepository = new ProjectRepository(mockContext.Object);

            IQueryable<Project> ProjectsList = TestData.GetProjects().AsQueryable();

            var mockSet = new Mock<DbSet<Project>>();

            mockSet.As<IAsyncEnumerable<Project>>()
        .Setup(m => m.GetEnumerator())
        .Returns(new TestAsyncEnumerator<Project>(ProjectsList.GetEnumerator()));

            mockSet.As<IQueryable<Project>>()
                .Setup(m => m.Provider)
                .Returns(new TestAsyncQueryProvider<Project>(ProjectsList.Provider));

            mockSet.As<IQueryable<Project>>().Setup(m => m.Expression).Returns(ProjectsList.Expression);
            mockSet.As<IQueryable<Project>>().Setup(m => m.ElementType).Returns(ProjectsList.ElementType);
            mockSet.As<IQueryable<Project>>().Setup(m => m.GetEnumerator()).Returns(() => ProjectsList.GetEnumerator());

            mockContext.Setup(m => m.Projects).Returns(mockSet.Object);

            var project = await projectRepository.GetProject(113334);

            Assert.Null(project);          
        }

        [Fact]
        public async Task ToCheckProjectName()
        {         
            var mockContext = new Mock<ProjectManagerApiDbContext>(contextOptions);

            var projectRepository = new ProjectRepository(mockContext.Object);

            IQueryable<Project> ProjectsList =TestData.GetProjects().AsQueryable();

            var mockSet = new Mock<DbSet<Project>>();

            mockSet.As<IAsyncEnumerable<Project>>()
        .Setup(m => m.GetEnumerator())
        .Returns(new TestAsyncEnumerator<Project>(ProjectsList.GetEnumerator()));

            mockSet.As<IQueryable<Project>>()
                .Setup(m => m.Provider)
                .Returns(new TestAsyncQueryProvider<Project>(ProjectsList.Provider));

            mockSet.As<IQueryable<Project>>().Setup(m => m.Expression).Returns(ProjectsList.Expression);
            mockSet.As<IQueryable<Project>>().Setup(m => m.ElementType).Returns(ProjectsList.ElementType);
            mockSet.As<IQueryable<Project>>().Setup(m => m.GetEnumerator()).Returns(() => ProjectsList.GetEnumerator());

            mockContext.Setup(m => m.Projects).Returns(mockSet.Object);           

            var project = await projectRepository.GetProject(113);

            Assert.NotNull(project);
            Assert.Equal("Project Management2", project.ProjectName);
            Assert.True(project.ActiveStatus);
            Assert.Equal(3, project.Priority);
        }

        [Fact]
        public async Task InsertProjectAndCheckWhetherSavedInDatabaseOrNot()
        {          
            var mockContext = new Mock<ProjectManagerApiDbContext>(contextOptions);

            var projectRepository = new ProjectRepository(mockContext.Object);

            var project = TestData.GetProjects().Where(pro=> pro.ProjectId== 115).FirstOrDefault();

            var mockSet = new Mock<DbSet<Project>>();

            mockContext.Setup(m => m.Projects).Returns(mockSet.Object);
            var result = await projectRepository.InsertProject(project);

            mockSet.Verify(m => m.Add(project), Times.Once);
            mockContext.Verify(m => m.SaveChangesAsync(System.Threading.CancellationToken.None), Times.Once);
        }

        [Fact]
        public async Task EditProjectAndCheckWhetherSavedInDatabaseOrNot()
        {         
            var mockContext = new Mock<ProjectManagerApiDbContext>(contextOptions);

            var projectRepository = new ProjectRepository(mockContext.Object);

            var project = TestData.GetProjects().Where(pro => pro.ProjectId == 1116).FirstOrDefault();

            var mockSet = new Mock<DbSet<Project>>();

            mockContext.Setup(m => m.Projects).Returns(mockSet.Object);
            var result = await projectRepository.EditProject(project);

            mockSet.Verify(m => m.Update(project), Times.Once);
            mockContext.Verify(m => m.SaveChangesAsync(System.Threading.CancellationToken.None), Times.Once);
        }

        [Fact]
        public async Task DeleteProjectAndCheckWhetherSavedInDatabaseOrNot()
        {
            var contextOptions = new DbContextOptions<ProjectManagerApiDbContext>();
            var mockContext = new Mock<ProjectManagerApiDbContext>(contextOptions);

            var projectRepository = new ProjectRepository(mockContext.Object);

            var project = TestData.GetProjects().Where(pro => pro.ProjectId == 114).FirstOrDefault();

            var mockSet = new Mock<DbSet<Project>>();

            mockContext.Setup(m => m.Projects).Returns(mockSet.Object);
            var result = await projectRepository.DeleteProject(project);

            mockSet.Verify(m => m.Remove(project), Times.Once);
            mockContext.Verify(m => m.SaveChangesAsync(System.Threading.CancellationToken.None), Times.Once);
        }

    }
}
