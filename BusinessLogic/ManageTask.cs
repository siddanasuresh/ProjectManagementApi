using BusinessLogic.Interfaces;
using DataAccess.Interfaces;
using Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace BusinessLogic
{
    public class ManageTask : IManageTask
    {
        private readonly ITask _task;

        public ManageTask(ITask task)
        {
            _task = task;
        }
        public async Task<IEnumerable<TaskDetail>> GetAllTasks()
        {
            return await _task.GetAllTasks();
        }
        public async Task<TaskDetail> GetTask(int taskId)
        {
            return await _task.GetTask(taskId);
        }      
        public async Task<int> InsertTask(TaskDetail taskDetail)
        {
            return await _task.InsertTask(taskDetail);
        }
        public async Task<int> EditTask(TaskDetail taskDetail)
        {
            return await _task.EditTask(taskDetail);
        }
        public async Task<int> DeleteTask(TaskDetail taskDetail)
        {
            return await _task.DeleteTask(taskDetail);
        }
    }
}
