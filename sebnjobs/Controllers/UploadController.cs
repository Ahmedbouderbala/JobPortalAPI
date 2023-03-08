using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sebnjobs.Models;
using System.Net.Http.Headers;

namespace sebnjobs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly SebnjobsContext _context;
        public UploadController(SebnjobsContext context)
        {
            _context = context;
        }
        [HttpPost, DisableRequestSizeLimit]
        public async Task<ActionResult> Upload()
        {

          
                var formCollection = await Request.ReadFormAsync();
                var file = formCollection.Files.First();
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
                   

                    return Ok(new { fileName });
                }
                else
                {
                    return BadRequest();
                }
          
        }
    }
}
