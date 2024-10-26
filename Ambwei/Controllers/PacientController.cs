using Ambwei.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;

namespace Ambwei.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PacientController : ControllerBase
    {
        private readonly ILogger<PacientController> _logger;

        private readonly AppDbContext _context;
        public PacientController(ILogger<PacientController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        // GET: api/pacients
        [HttpGet(Name = "GetPacients")]
        public async Task<IEnumerable<Pacient>> Get()
        {
            return await _context.Pacients.ToListAsync();
        }

        [HttpGet("{id}", Name = "GetPacient")]
        public async Task<ActionResult<Pacient>> GetPacient(int id)
        {
            var pacient = await _context.Pacients.FindAsync(id);

            if (pacient == null)
            {
                return NotFound(); // Retorna 404 se não encontrado
            }

            return Ok(pacient); // Retorna o usuário encontrado com 200 OK
        }

        [HttpPost]
        public async Task<ActionResult<Pacient>> PostPacient(Pacient pacient)
        {
            _context.Pacients.Add(pacient);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = pacient.pacient_id }, pacient);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var pacient = await _context.Pacients.FindAsync(id);

            if (pacient == null)
            {
                return NotFound(); // Retorna 404 se não encontrado
            }
            _context.Pacients.Remove(pacient);

            await _context.SaveChangesAsync();

            return NoContent(); // Retorna 204 No Content
        }
    }
}
