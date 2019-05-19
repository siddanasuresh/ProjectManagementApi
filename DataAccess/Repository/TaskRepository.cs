using DataAccess.Interfaces;
using Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace DataAccess
{
    public class TaskRepository : ITask
    {
        ProjectManagerApiDbContext _dbContext;

        public TaskRepository()
        {

        }

        public async Task SaveDabaseChanges()
        {
            await _dbContext.SaveChangesAsync();
        }
        public TaskRepository(ProjectManagerApiDbContext _dbContext)
        {
            this._dbContext = _dbContext;
        }
        public async Task<IEnumerable<TaskDetail>> GetAllTasks()
        {
            return await _dbContext.Tasks.AsNoTracking().ToListAsync();
        }
        public async Task<TaskDetail> GetTask(int taskId)
        {
            return await _dbContext.Tasks.Include(project => project.UserDetail)
                .Include(project => project.ProjectDetail).AsNoTracking().FirstOrDefaultAsync(t => t.Id == taskId);
        }
        public async Task<int> InsertTask(TaskDetail taskDetail)
        {
            taskDetail.UserDetail = null;
            taskDetail.ProjectDetail = null;

            _dbContext.Tasks.Add(taskDetail);

            return await _dbContext.SaveChangesAsync();
        }
        public async Task<int> EditTask(TaskDetail taskDetail)
        {
            taskDetail.ProjectDetail = null;
            taskDetail.UserDetail = null;
            _dbContext.Tasks.Update(taskDetail);
            return await _dbContext.SaveChangesAsync();
        }
        public async Task<int> DeleteTask(TaskDetail entity)
        {
            _dbContext.Tasks.Remove(entity);

            return await _dbContext.SaveChangesAsync();
        }
    }
}

