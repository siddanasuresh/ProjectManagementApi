using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Entities;
using System.Threading.Tasks;

namespace ProjectManagerApi.Controllers
{
    [Produces("application/json")]
    [Route("api/ParentTaskDetail")]
    public class ParentTaskDetailsController : Controller
    {
        IManageParentTaskDetails _manageParentTaskDetails;

        public ParentTaskDetailsController(IManageParentTaskDetails manageParentTaskDetails)
        {
            _manageParentTaskDetails = manageParentTaskDetails;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _manageParentTaskDetails.GetAll());
        }

        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _manageParentTaskDetails.Get(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]ParentTaskDetails parentTask)
        {
            if (!ModelState.IsValid || parentTask == null)
            {
                return BadRequest("Provided Parent Task Details are not valid.");
            }

            await _manageParentTaskDetails.Insert(parentTask);

            return Ok(parentTask.ParentId);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]ParentTaskDetails parentTask)
        {
            if (!ModelState.IsValid || parentTask == null || id != parentTask.ParentId)
            {
                return BadRequest("Provided Parent Task Details are not valid.");
            }

            await _manageParentTaskDetails.Edit(parentTask);

            return Ok(parentTask.ParentId);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid || id <= 0)
            {
                return BadRequest("Parent Task id is not valid.");
            }

            var parentTask = await _manageParentTaskDetails.Get(id);
            if (parentTask == null)
                return BadRequest("Parent Task details not found with given id:" + id);

            await _manageParentTaskDetails.Delete(parentTask);

            return Ok(parentTask.ParentId);
        }
    }
}
