
using Task_Management.Repositories;
using Task_Management.Models;
using Microsoft.AspNetCore.Mvc;
namespace Task_Management.Controllers
{
    //  Attribute that tells ASP.NET Core this class is a Web API Controller.
 [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly TaskRepository _taskRepo;

        public TaskController(TaskRepository taskRepo)
        {
            _taskRepo = taskRepo;
        }

        [HttpPost]
        public IActionResult AddTask(TaskModel task)
        {
            _taskRepo.AddTask(task);
            return Ok("Task added successfully");
        }

        [HttpGet]
        public IActionResult GetTasks()
        {
            var tasks = _taskRepo.GetTasks();
            return Ok(tasks);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTaskStatus(int id, [FromBody] string status)
        {
            _taskRepo.UpdateTaskStatus(id, status);
            return Ok("Task updated successfully");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTask(int id)
        {
            _taskRepo.DeleteTask(id);
            return Ok("Task deleted successfully");
        }
    }
}