using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.IO;
using System.Linq;
using VCLICApi.Model;
using VCLICApi.Data.Mappings;
using VCLICApi.Data;
namespace VCLICApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BetaBlockerController : ControllerBase
    {
        private readonly MyDbContext _context;

        public BetaBlockerController(MyDbContext context)
        {
            _context = context;
        }
        [HttpPost("upload-beta-blocker-values")]
        public async Task<IActionResult> ReadBetaBlockerValues(IFormFile file) 
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            var betaBlockerValueSets = new List<BetaBlockerValueSet>();

            using (var reader = new StreamReader(file.OpenReadStream()))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
            }))
            {
                csv.Context.RegisterClassMap<BetaBlockerValueSetMap>();
                betaBlockerValueSets = csv.GetRecords<BetaBlockerValueSet>().ToList();
            }

            // Save to database
            _context.BetaBlockerValueSets.AddRange(betaBlockerValueSets);
            await _context.SaveChangesAsync();

            return Ok(betaBlockerValueSets);
        }

    }


}
