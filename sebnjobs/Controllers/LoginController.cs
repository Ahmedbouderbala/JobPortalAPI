using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sebnjobs.Models;

namespace sebnjobs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly SebnjobsContext _context;

        public LoginController(SebnjobsContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] Login item)
        {
            if (ModelState.IsValid)
            {
                _context.Database.EnsureCreated();

                var pwd = await _context.Employees.SingleOrDefaultAsync(p => p.Password == item.Password);
                var email = await _context.Employees.SingleOrDefaultAsync(e => e.Email == item.Email);
                if ((pwd != null) && (email != null))
                {

                    return Ok(new Response { Status = "Sucess", Message = "User Logged Successfully", currentUser = pwd.EmployeeId });
                }
            }

            return NotFound();
        }
    }
}
