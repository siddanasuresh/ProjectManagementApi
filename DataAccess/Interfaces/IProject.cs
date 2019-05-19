using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;

namespace DataAccess.Interfaces
{
    public interface IProject
    {
        Task<IEnumerable<Project>> GetAllProjects();
        Task<Project> GetProject(int projectId);
        Task<int> InsertProject(Project project);
        Task<int> EditProject(Project project);
        Task<int> DeleteProject(Project project);
    }
}
