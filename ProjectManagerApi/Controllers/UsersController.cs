using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Entities;
using System.Threading.Tasks;

namespace ProjectManagerApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Users")]
    public class UsersController : Controller
    {
        IManageUser _manageUser;
        public UsersController(IManageUser manageUser)
        {
            _manageUser = manageUser;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            return Ok(await _manageUser.GetAllUsers());
        }

        [HttpGet("{id}", Name = "GetUser")]
        public async Task<IActionResult> GetUser(int id)
        {
            if (id < 0)
            {
                return BadRequest("User Id is invalid:" + id);
            }
            return Ok(await _manageUser.GetUser(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]User user)
        {

            if (!ModelState.IsValid || user == null)
            {
                return BadRequest("Invalid user details.");
            }

            await _manageUser.InsertUser(user);

            return Ok(user.UserId);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]User user)
        {
            if (!ModelState.IsValid || user == null || id != user.UserId)
            {
                return BadRequest("Invalid user details.");
            }

            await _manageUser.EditUser(user);
            return Ok(user.UserId);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userDetails = await _manageUser.GetUser(id);
            if (userDetails == null)
            {
                return BadRequest("User Details not found with id:" + id);
            }

            await _manageUser.DeleteUser(userDetails);

            return Ok(userDetails.UserId);
        }
    }
}