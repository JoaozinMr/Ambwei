using Ambwei.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace Ambwei.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocationController : ControllerBase
    {
        private readonly ILogger<LocationController> _logger;

        private readonly AppDbContext _context;

        public LocationController(ILogger<LocationController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        // GET: api/users
        [HttpGet(Name = "GetLocations")]
        public async Task<IEnumerable<Location>> Get()
        {
            return await _context.Locations.ToListAsync();
        }

        [HttpGet("{id}", Name = "GetLocation")]
        public async Task<ActionResult<Location>> GetLocation(int id)
        {
            var location = await _context.Locations.FindAsync(id);

            if (location == null)
            {
                return NotFound(); // Retorna 404 se não encontrado
            }

            return Ok(location); // Retorna o usuário encontrado com 200 OK
        }

        [HttpPost]
        public async Task<ActionResult<Location>> PostLocation(Location location)
        {
            _context.Locations.Add(location);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = location.location_id }, location);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var location = await _context.Locations.FindAsync(id);

            if (location == null)
            {
                return NotFound(); // Retorna 404 se não encontrado
            }

            _context.Locations.Remove(location);
            await _context.SaveChangesAsync();

            return NoContent(); // Retorna 204 No Content para indicar que a operação foi bem-sucedida
        }
    }
}
