using Microsoft.AspNetCore.Mvc;
using Task_Management.Persistence.Repositories;
using Task_Management.Domain.Entities;
using Task_Management.Persistence.Data;

namespace Task_Management.WebUI.Controllers
{
    [ApiController]
    [Route("api")]
    public class AuthController : ControllerBase
    {
        private readonly UserRepository _userRepo;

        public AuthController(DbHelper dbHelper)
        {
            _userRepo = new UserRepository(dbHelper);
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserModel loginRequest)
        {
            var user = _userRepo.ValidateUserLogin(loginRequest.Email, loginRequest.PasswordHash);

            if (user == null)
                return Unauthorized(new { message = "Invalid email or password" });

            // ✅ Return role and user info
            return Ok(new
            {
                message = "Login successful",
                userId = user.UserID,
                userName = user.UserName,
                role = user.Role
            });
        }
    }
}
