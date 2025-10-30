using Microsoft.AspNetCore.Mvc;
using Task_Management.Persistence.Data;
using Persistence.Repositories.admin;


namespace WebUI.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly UpdateUserRoleRepository _updateUserRoleRepo;
        public AdminController(DbHelper db)
        {
            _updateUserRoleRepo = new UpdateUserRoleRepository(db);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateUserRole(int userId, [FromBody] string newRole)
        {
            if (string.IsNullOrWhiteSpace(newRole))
            return BadRequest("New role is required.");
            {
                try

                {
                 _updateUserRoleRepo.UpdateUserRole(userId, newRole);
                  return Ok(new { message = "User role updated successfully." });
                }
                catch (Exception ex)
                {
                  return StatusCode(500, new { message = "Error updating user role.", error = ex.Message });  
                }
            }


        }
    
    }
    
    
}