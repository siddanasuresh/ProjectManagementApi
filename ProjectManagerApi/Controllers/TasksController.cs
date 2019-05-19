using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Entities;
using System.Threading.Tasks;

namespace ProjectManagerApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Tasks")]
    public class TasksController : Controller
    {
        IManageTask _manageTask;
        public TasksController(IManageTask manageTask)
        {
            _manageTask = manageTask;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTasks()
        {
            return Ok(await _manageTask.GetAllTasks());
        }

        [HttpGet("{id}", Name = "GetTask")]
        public async Task<IActionResult> GetTask(int id)
        {
            return Ok(await _manageTask.GetTask(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]TaskDetail taskDetail)
        {

            if (!ModelState.IsValid || taskDetail == null)
            {
                return BadRequest("Invalid task details.");
            }

            await _manageTask.InsertTask(taskDetail);

            return Ok(taskDetail.Id);
        }
      
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]TaskDetail taskDetail)
        {

            if (!ModelState.IsValid || taskDetail == null || id != taskDetail.Id)
            {
                return BadRequest("Invalid task details.");
            }

            await _manageTask.EditTask(taskDetail);

            return Ok(taskDetail.Id);
        }
      
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var task = await _manageTask.GetTask(id);

            if(task==null)
            {
                return NotFound("No Task found with given taskId:"+ id);
            }

            await _manageTask.DeleteTask(task);

            return Ok(task.Id);      
        }
    }
}