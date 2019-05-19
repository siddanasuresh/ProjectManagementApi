using Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IManageTask
    {
        Task<IEnumerable<TaskDetail>> GetAllTasks();
        Task<TaskDetail> GetTask(int taskId);
        Task<int> InsertTask(TaskDetail taskDetail);
        Task<int> EditTask(TaskDetail taskDetail);
        Task<int> DeleteTask(TaskDetail taskDetail);
    }
}
