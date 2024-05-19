using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using VCLICApi.Model;
using VCLICApi.Data.Mappings;
using VCLICApi.Data;

namespace VCLICApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MedicationController : ControllerBase
    {
        private readonly MyDbContext _context;

        public MedicationController(MyDbContext context)
        {
            _context = context;
        }

        [HttpPost("upload-medications")]
        public async Task<IActionResult> ReadMedications(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            var medications = new List<Medication>();

            using (var reader = new StreamReader(file.OpenReadStream()))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
            }))
            {
                csv.Context.RegisterClassMap<MedicationMap>();
                medications = csv.GetRecords<Medication>().ToList();
            }

            // Save to database
            _context.Medications.AddRange(medications);
            await _context.SaveChangesAsync();

            return Ok(medications);
        }
    }
}
