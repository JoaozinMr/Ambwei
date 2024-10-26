using Ambwei.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace Ambwei.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ILogger<TaskController> _logger;

        private readonly AppDbContext _context;
        public TaskController(ILogger<TaskController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        // GET: api/tasks
        [HttpGet(Name = "GetTasks")]
        public async Task<IEnumerable<Models.Task>> Get()
        {
            return await _context.Tasks.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Models.Task>> GetTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);

            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }

        [HttpPost]
        public async Task<ActionResult<Models.Task>> PostTask(Models.Task task)
        {
            _context.Tasks.Add(task);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = task.task_id }, task);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Finish(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            task.finished_at = DateTime.UtcNow; // Data e hora que a tarefa foi finalizada
            await _context.SaveChangesAsync();

            return NoContent(); // Retorna 204 No Content
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var task = await _context.Tasks.FindAsync(id);

            if (task == null)
            {
                return NotFound();
            }
            _context.Tasks.Remove(task);

            await _context.SaveChangesAsync();

            return NoContent(); // Retorna 204 No Content
        }
    }
}
