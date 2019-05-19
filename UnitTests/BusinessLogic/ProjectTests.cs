using BusinessLogic;
using BusinessLogic.Interfaces;
using DataAccess.Interfaces;
using Moq;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.BusinessLogic
{
    public class ProjectTests 
    {
        readonly Mock<IProject> _projectRepository;
        readonly IManageProject _manageProject;
        public ProjectTests()
        {
            _projectRepository = new Mock<IProject>();
            _manageProject = new ManageProject(_projectRepository.Object);
        }
        [Fact]
        public async Task VerifyGetAllProjectsFunction()
        {       
            await _manageProject.GetAllProjects();
            _projectRepository.Verify(r => r.GetAllProjects(), Times.Once);
        }

        [Fact]
        public async Task VerifyGetProjectFunction()
        {
            await _manageProject.GetProject(114);

            _projectRepository.Verify(r => r.GetProject(114), Times.Once);
        }
        [Fact]
        public async Task VerifyInsertFunction()
        {
            var project = TestData.GetProjects().FirstOrDefault();

            await _manageProject.InsertProject(project);

            _projectRepository.Verify(r => r.InsertProject(project), Times.Once);
        }
        [Fact]
        public async Task VerifyEditFunction()
        {
            var project = TestData.GetProjects().FirstOrDefault();

            await _manageProject.EditProject(project);

            _projectRepository.Verify(r => r.EditProject(project), Times.Once);
        }       
        [Fact]
        public async Task VerifyDeleteProjectFunction()
        {
            var project = TestData.GetProjects().LastOrDefault();

            await _manageProject.DeleteProject(project);

            _projectRepository.Verify(r => r.DeleteProject(project), Times.Once);
        }       
    }
}
