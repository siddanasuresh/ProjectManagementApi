using DataAccess.Interfaces;
using Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace DataAccess
{
    public class ProjectRepository : IProject
    {
        private readonly ProjectManagerApiDbContext _dbContext;
        public ProjectRepository()
        {
        }
        public ProjectRepository(ProjectManagerApiDbContext dbContext)
        {
            _dbContext = dbContext;          
        }
        public async Task<IEnumerable<Project>> GetAllProjects()
        {
            return await _dbContext.Projects.AsNoTracking().ToListAsync();
        }
        public async Task<Project> GetProject(int projectId)
        {
            return await _dbContext.Projects.
                Include(p => p.TaskDetails).Include(p => p.UserDetail).FirstOrDefaultAsync(t => t.ProjectId == projectId);
        }
        public async Task<int> InsertProject(Project project)
        {
            project.UserDetail = null;
            _dbContext.Projects.Add(project);
            return await _dbContext.SaveChangesAsync();
        }
        public async Task<int> EditProject(Project project)
        {
            _dbContext.Projects.Update(project);
            return await _dbContext.SaveChangesAsync();
        }
        public async Task<int> DeleteProject(Project project)
        {
            _dbContext.Projects.Remove(project);
            return await _dbContext.SaveChangesAsync();
        }
    }
}
