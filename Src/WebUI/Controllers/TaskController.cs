using Microsoft.AspNetCore.Mvc;
 
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks; 
using Task_Management.Persistence.Repositories;
using Task_Management.Domain.Entities;
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


[HttpGet("{id}")]
public IActionResult GetTask(int id)
{
    var task = _taskRepo.GetTaskById(id);
    if (task == null)
        return NotFound("Task not found.");

    return Ok(task);
}

        [HttpPut("{id}")]
public IActionResult UpdateTask(int id, [FromBody] TaskModel task)
{
    if (task == null || id != task.TaskID)
    {
        return BadRequest("Invalid task data.");
    }

    _taskRepo.UpdateTask(task);
    return Ok("Task updated successfully.");
}


        [HttpDelete("{id}")]
        public IActionResult DeleteTask(int id)
        {
            _taskRepo.DeleteTask(id);
            return Ok("Task deleted successfully");
        }
    }
}