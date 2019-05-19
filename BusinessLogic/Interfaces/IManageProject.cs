using Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IManageProject
    {
        Task<IEnumerable<Project>> GetAllProjects();
        Task<Project> GetProject(int projectId);
        Task<int> InsertProject(Project project);
        Task<int> EditProject(Project project);      
        Task<int> DeleteProject(Project project);
    }
}
