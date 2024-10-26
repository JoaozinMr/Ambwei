using Ambwei.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace Ambwei.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly ILogger<RoleController> _logger;

        private readonly AppDbContext _context;

        public RoleController(ILogger<RoleController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        // GET: api/roles
        [HttpGet(Name = "GetRoles")]
        public async Task<IEnumerable<Role>> Get()
        {
            return await _context.Roles.ToListAsync();
        }

        [HttpGet("{id}", Name = "GetRole")]
        public async Task<ActionResult<Role>> GetRole(int id)
        {
            var role = await _context.Roles.FindAsync(id);

            if (role == null)
            {
                return NotFound(); // Retorna 404 se não encontrado
            }

            return Ok(role); // Retorna o usuário encontrado com 200 OK
        }

        [HttpPost]
        public async Task<ActionResult<Role>> PostRole(Role role)
        {
            _context.Roles.Add(role);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = role.role_id }, role);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var role = await _context.Roles.FindAsync(id);

            if (role == null)
            {
                return NotFound(); // Retorna 404 se não encontrado
            }

            _context.Roles.Remove(role);

            await _context.SaveChangesAsync();

            return NoContent(); // Retorna 204 No Content
        }
    }
}
