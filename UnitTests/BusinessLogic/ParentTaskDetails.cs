using BusinessLogic;
using BusinessLogic.Interfaces;
using DataAccess.Interfaces;
using Moq;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.BusinessLogic
{
    public class ParentTaskDetails
    {
        readonly Mock<IParentTaskDetails> _parentTaskRepository;
        readonly IManageParentTaskDetails _manageParentTaskDetails;
        public ParentTaskDetails()
        {
            _parentTaskRepository = new Mock<IParentTaskDetails>();
            _manageParentTaskDetails = new ManageParentTaskDetails(_parentTaskRepository.Object);
        }
        [Fact]
        public async Task VerifyGetAllFunction()
        {
            await _manageParentTaskDetails.GetAll();
            _parentTaskRepository.Verify(r => r.GetAll(), Times.Once);
        }

        [Fact]
        public async Task VerifyGetProjectFunction()
        {
            await _manageParentTaskDetails.Get(114);

            _parentTaskRepository.Verify(r => r.Get(114), Times.Once);
        }
        [Fact]
        public async Task VerifyInsertFunction()
        {
            var project = TestData.GetParentTaskDetails().FirstOrDefault();

            await _manageParentTaskDetails.Insert(project);

            _parentTaskRepository.Verify(r => r.Insert(project), Times.Once);
        }
        [Fact]
        public async Task VerifyEditFunction()
        {
            var project = TestData.GetParentTaskDetails().FirstOrDefault();

            await _manageParentTaskDetails.Edit(project);

            _parentTaskRepository.Verify(r => r.Edit(project), Times.Once);
        }
        [Fact]
        public async Task VerifyDeleteProjectFunction()
        {
            var project = TestData.GetParentTaskDetails().LastOrDefault();

            await _manageParentTaskDetails.Delete(project);

            _parentTaskRepository.Verify(r => r.Delete(project), Times.Once);
        }
    }
}
