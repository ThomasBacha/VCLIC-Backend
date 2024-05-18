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
        [HttpGet("read")]  // Changed to a GET method as it now reads from a fixed source
        public async Task<IActionResult> ReadMedications() // Change the return type to Task<IActionResult>

        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "requirements", "medications.csv");  // Assuming the file is in the 'requirements' directory under the root of the project

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound($"The file was not found: {filePath}");
            }

            var medications = new List<Medication>();

            using (var reader = new StreamReader(filePath))
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
