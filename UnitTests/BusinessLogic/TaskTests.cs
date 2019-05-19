using BusinessLogic;
using BusinessLogic.Interfaces;
using DataAccess.Interfaces;
using Moq;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.BusinessLogic
{
    public class TaskTests
    {
        readonly Mock<ITask> _taskRepository;
        readonly IManageTask _manageTask;
        public TaskTests()
        {
            _taskRepository = new Mock<ITask>();
            _manageTask = new ManageTask(_taskRepository.Object);
        }
        [Fact]
        public async Task VerifyGetTasksFunction()
        {           
            var taskObj = new ManageTask(_taskRepository.Object);

            await _manageTask.GetAllTasks();

            _taskRepository.Verify(r => r.GetAllTasks(), Times.Once);
        }
        [Fact]
        public async Task VerifyGetTaskFunction()
        {           
            var taskObj = new ManageTask(_taskRepository.Object);

             await _manageTask.GetTask(114);

            _taskRepository.Verify(r => r.GetTask(114), Times.Once);
        }
        [Fact]
        public async Task VerifyInsertFunction()
        {        
            var taskDetail = TestData.GetTasks().Where(task=>task.Id==113).FirstOrDefault();

             await _manageTask.InsertTask(taskDetail);

            _taskRepository.Verify(r => r.InsertTask(taskDetail), Times.Once);
        }
        [Fact]
        public async Task VerifyEditFuntion()
        {
           var taskDetail = TestData.GetTasks().Where(task => task.Id == 115).FirstOrDefault();

            await _manageTask.EditTask(taskDetail);

            _taskRepository.Verify(r => r.EditTask(taskDetail), Times.Once);
        }
        [Fact]
        public async Task VerifyDeleteFuntion()
        {
            var taskDetail = TestData.GetTasks().Where(task => task.Id == 114).FirstOrDefault();

             await _manageTask.DeleteTask(taskDetail);

            _taskRepository.Verify(r => r.DeleteTask(taskDetail), Times.Once);
        }
    }
}
