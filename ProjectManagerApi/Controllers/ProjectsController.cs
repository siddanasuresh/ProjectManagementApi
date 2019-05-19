using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Entities;
using System.Threading.Tasks;

namespace ProjectManagerApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Projects")]
    public class ProjectsController : Controller
    {
        IManageProject _manageProject;

        public ProjectsController(IManageProject manageProject)
        {

            _manageProject = manageProject;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProjects()
        {
            return Ok(await _manageProject.GetAllProjects());
        }

        [HttpGet("{id}", Name = "GetProject")]
        public async Task<IActionResult> GetProject(int id)
        {
            return Ok(await _manageProject.GetProject(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Project project)
        {
            if (!ModelState.IsValid || project == null)
            {
                return BadRequest("Provided project details are not valid.");
            }

            await _manageProject.InsertProject(project);

            return Ok(project.ProjectId);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]Project project)
        {
            if (!ModelState.IsValid || project == null || id != project.ProjectId)
            {
                return BadRequest("Provided project details are not valid.");
            }

            await _manageProject.EditProject(project);

            return Ok(project.ProjectId);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid || id <= 0)
            {
                return BadRequest("Project id is not valid.");
            }

            var project = await _manageProject.GetProject(id);
            if (project == null)
                return BadRequest("Project details not found with given id:" + id);
            await _manageProject.DeleteProject(project);

            return Ok(project.ProjectId);
        }
    }
}
