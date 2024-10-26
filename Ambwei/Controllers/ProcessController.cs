using Ambwei.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace Ambwei.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProcessController : ControllerBase
    {
        private readonly ILogger<ProcessController> _logger;

        private readonly AppDbContext _context;

        public ProcessController(ILogger<ProcessController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        // GET: api/roles
        [HttpGet(Name = "GetProcesses")]
        public async Task<IEnumerable<Process>> Get()
        {
            return await _context.Processes.ToListAsync();
        }

        [HttpGet("{id}", Name = "GetProcess")]
        public async Task<ActionResult<Process>> GetProcess(int id)
        {
            var process = await _context.Processes.FindAsync(id);

            if (process == null)
            {
                return NotFound(); // Retorna 404 se não encontrado
            }

            return Ok(process); // Retorna o usuário encontrado com 200 OK
        }

        // POST: api/Processos
        // Cria um novo processo de consulta vinculando o paciente ao médico (usuário)
        [HttpPost]
        public async Task<ActionResult<Process>> PostProcess(int pacientId, int userId)
        {
            // Verifica se o paciente existe
            var paciente = await _context.Pacients.FindAsync(pacientId);
            if (paciente == null)
            {
                return NotFound($"Paciente com ID {pacientId} não encontrado.");
            }

            // Verifica se o médico (usuário) existe
            var usuario = await _context.Users.FindAsync(userId);
            if (usuario == null)
            {
                return NotFound($"Médico com ID {userId} não encontrado.");
            }

            // Cria um novo processo vinculando o paciente ao médico
            var process = new Process
            {
                created_at = DateTime.UtcNow,
                pacient_id = pacientId,  // Vincula o paciente
                user_id = userId         // Vincula o médico (usuário)
            };

            _context.Processes.Add(process);
            await _context.SaveChangesAsync();

            // Retorna o processo criado com os dados do paciente e médico vinculados
            return CreatedAtAction(nameof(Get), new { id = process.process_id }, process);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var process = await _context.Processes.FindAsync(id);

            if (process == null)
            {
                return NotFound(); // Retorna 404 se não encontrado
            }

            _context.Processes.Remove(process);

            await _context.SaveChangesAsync();

            return NoContent(); // Retorna 204 No Content
        }

        [HttpPut]
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
    }
}
