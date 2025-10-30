
using Microsoft.AspNetCore.Mvc;
using Task_Management.Persistence.Repositories;
using Task_Management.Domain.Entities;

namespace Task_Management.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserRepository _userRepo;

        public UserController(UserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        [HttpPost]
        public IActionResult AddUser(UserModel user)
        {
            _userRepo.AddUser(user);
            return Ok("User added successfully");
        }

        // ✅ FIXED GetAllUsers method
        [HttpGet]
        public ActionResult<IEnumerable<UserModel>> GetAllUsers()
        {
            var users = _userRepo.GetAllUsers(); // ✅ call repository
            return Ok(users); // ✅ return result
        }

[HttpGet("{id}")]
public IActionResult GetUser(int id)
{
    var user = _userRepo.GetUserById(id);
    if (user == null)
        return NotFound("User not found.");

    return Ok(user);
}



 // ✅ Update user
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] UserModel updatedUser)
        {
            if (updatedUser == null)
                return BadRequest("Invalid user data.");

            _userRepo.UpdateUser(id, updatedUser);

            return Ok($"User with ID {id} updated successfully.");
        }

        // ✅ Delete user
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid user ID.");

            _userRepo.DeleteUser(id);

            return Ok($"User with ID {id} deleted successfully.");
        }
    }
}



