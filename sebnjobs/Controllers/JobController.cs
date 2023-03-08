using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sebnjobs.Models;

namespace sebnjobs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly SebnjobsContext _context;

        public JobController(SebnjobsContext context)
        {
            _context = context;
        }

        [HttpGet("get_job/id")]
        public async Task<IActionResult> GetJob(int JobId)
        {
            var job = await _context.Jobs.FindAsync(JobId);
            if (job == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(job);
            }
        }
        [HttpPost("add_job")]
        public async Task<IActionResult> AddJob([FromBody] Job jobObj)
        {
            if (jobObj == null)
            {
                return NotFound();
            }
            else
            {
                _context.Jobs.Add(jobObj);
                _context.SaveChanges();
                return Ok(new
                {
                    StatusCode = 200,
                    Message = "Job Added Successfully!"
                });
            }
        }

        [HttpPut("update_Job")]
        public async Task<IActionResult> UpdateJob([FromBody] Job jobObj)
        {
            if (jobObj == null)
            {
                return NotFound();
            }
            var job = await _context.Jobs.AsNoTracking().FirstOrDefaultAsync(x => x.JobId == jobObj.JobId);
            if (job == null)
            {
                return NotFound();
            }
            else
            {
                _context.Jobs.Entry(jobObj).State = EntityState.Modified;
                _context.SaveChanges();
                return Ok();
            }
        }
        [HttpDelete("delete_Job/{id}")]
        public async Task<IActionResult> DeleteJob(string id)
        {
            var job = await _context.Jobs.FindAsync(id);
            if (job == null)
            {
                return NotFound();
            }
            else
            {
                _context.Jobs.Remove(job);

                _context.SaveChanges();

                return Ok();
            }
        }
    }
}
