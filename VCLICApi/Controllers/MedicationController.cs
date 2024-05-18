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
    public class MedicationController : ControllerBase
    {
        [HttpPost("upload")]
        public IActionResult UploadMedications(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("Please upload a valid CSV file.");
            }

            var medications = new List<Medication>();

            using (var reader = new StreamReader(file.OpenReadStream()))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
            }))
            {
                medications = csv.GetRecords<Medication>().ToList();
            }

            // Here you could add code to save these records to a database
            return Ok(medications);
        }
    }
}
