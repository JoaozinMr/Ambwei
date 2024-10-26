using Ambwei.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace Ambwei.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;

        private readonly AppDbContext _context;

        public LoginController(ILogger<LoginController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<User>> Login(LoginUser user)
        {
            var userLogin = await _context.Users.Where(u => u.user_name == user.user_name && u.user_passwd == user.user_passwd).FirstOrDefaultAsync();

            if (userLogin == null)
            {
                return NotFound(); // Retorna 404 se não encontrado
            }

            return Ok(userLogin); // Retorna o usuário encontrado com 200 OK
        }
    }
}
