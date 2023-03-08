using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sebnjobs.Models;
using System.Net.Http.Headers;

namespace sebnjobs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobEmployeeController : ControllerBase
    {
        private readonly SebnjobsContext _context;
        public JobEmployeeController(SebnjobsContext context)
        {
            _context = context;
        }
        [HttpGet("{employeeId}")]
        public async Task<ActionResult<List<Job1>>> GetEmployeeJob(int employeeId, int jobId)
        {
            var employeejob = await _context.Jobs1.Where(e => e.EmployeeId == employeeId && e.JobId == jobId)
                .ToListAsync();

            if (employeejob != null)
            {
                return employeejob;
            }
            return NotFound();
        }
        [HttpGet]
        public async Task<ActionResult<List<Job1>>> GetAllEmployeeJob()
        {
            var employeejob = await _context.Jobs1.ToListAsync();
            if (employeejob != null)
            {
                return employeejob;
            }
            return NotFound();
        }


        [HttpPost]
        public async Task<ActionResult> AddEmployeeJob([FromForm] Job1 jobobj, IFormFile File)
        {
            try
            {
                // Get the uploaded file
                IFormFile file = File;
               
                var folderName = Path.Combine("Resources", "Cvs");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    jobobj.FilePdfName = fileName;
                    _context.Jobs1.Add(jobobj);
                    _context.SaveChanges();
                    return Ok(new { jobobj.FilePdfName });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

       
        
          

           
        

        [HttpDelete("{empId}/EmpJob/{jobId}")]
        public async Task<IActionResult> DeleteEmpJob(int empId, int jobId)
        {
            var empjob = await _context.Jobs1.SingleOrDefaultAsync(cp => cp.JobId == jobId && cp.EmployeeId == empId);
            if (empjob == null)
            {
                return NotFound();
            }

            _context.Jobs1.Remove(empjob);
            await _context.SaveChangesAsync();

            return NoContent();
        }

       






    }

}
