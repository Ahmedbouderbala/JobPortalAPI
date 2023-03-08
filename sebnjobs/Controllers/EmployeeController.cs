using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sebnjobs.Models;

namespace sebnjobs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly SebnjobsContext _context;

        public EmployeeController(SebnjobsContext context)
        {
            _context = context;
        }

        [HttpGet("get_emp/id")]
        public async Task<IActionResult> GetEmp(int EmpId)
        {
            var emp = await _context.Employees.FindAsync(EmpId);
            if (emp == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(emp);
            }
        }
        [HttpPost("add_emp")]
        public async Task<IActionResult> AddEmp([FromBody] Employee empObj)
        {
            if (empObj == null)
            {
                return NotFound();
            }
            else
            {
                _context.Employees.Add(empObj);
                _context.SaveChanges();
                return Ok(new
                {
                    StatusCode = 200,
                    Message = "Emp Added Successfully!"
                });
            }
        }

        [HttpPut("update_Empb")]
        public async Task<IActionResult> UpdateEmp([FromBody] Employee empObj)
        {
            if (empObj == null)
            {
                return NotFound();
            }
            var emp = await _context.Employees.AsNoTracking().FirstOrDefaultAsync(x => x.EmployeeId == empObj.EmployeeId);
            if (emp == null)
            {
                return NotFound();
            }
            else
            {
                _context.Employees.Entry(empObj).State = EntityState.Modified;
                _context.SaveChanges();
                return Ok();
            }
        }
        [HttpDelete("delete_Emp/{id}")]
        public async Task<IActionResult> DeleteEmp(string id)
        {
            var emp = await _context.Employees.FindAsync(id);
            if (emp == null)
            {
                return NotFound();
            }
            else
            {
                _context.Employees.Remove(emp);

                _context.SaveChanges();

                return Ok();
            }
        }
    }
}
