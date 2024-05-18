using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.IO;
using System.Linq;

namespace VCLICApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BetaBlockerController : ControllerBase
    {
        [HttpPost("upload")]
        public IActionResult UploadBetaBlockerValues(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("Please upload a valid CSV file.");
            }

            var betaBlockerValueSets = new List<BetaBlockerValueSet>();

            using (var reader = new StreamReader(file.OpenReadStream()))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
            }))
            {
                betaBlockerValueSets = csv.GetRecords<BetaBlockerValueSet>().ToList();
            }

            return Ok(betaBlockerValueSets);
        }
    }
}
