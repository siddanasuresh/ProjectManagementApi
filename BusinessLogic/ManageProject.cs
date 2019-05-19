using BusinessLogic.Interfaces;
using DataAccess.Interfaces;
using Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace BusinessLogic
{
    public class ManageProject : IManageProject
    {
        IProject _project;

        public ManageProject()
        {
            
        }
        public ManageProject(IProject project)
        {
            _project = project;          
        }
        public async Task<IEnumerable<Project>> GetAllProjects()
        {
            return await _project.GetAllProjects();
        }
        public async Task<Project> GetProject(int projectId)
        {
            return await _project.GetProject(projectId);
        }
        public async Task<int> InsertProject(Project project)
        {
            return await _project.InsertProject(project);
        }
        public async Task<int> EditProject(Project project)
        {
            return await _project.EditProject(project);
        }      
        public async Task<int> DeleteProject(Project project)
        {
           return await _project.DeleteProject(project);
        }
    }
}
