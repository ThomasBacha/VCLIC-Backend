using Microsoft.AspNetCore.Mvc;

namespace VCLICApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ValueSetController : ControllerBase
    {
        private static readonly List<dynamic> ValueSets = new List<dynamic>
        {
            new { Id = 100165, Name = "ERX GENERAL BETA BLOCKERS PQRS MEASURE 7,8", Medications = "716|717|718" },
            new { Id = 100166, Name = "ERX EXTENDED BETA BLOCKERS PQRS MEASURE 9,10", Medications = "1037|1048|1916" },
            new { Id = 100166, Name = "ERX EXTENDED BETA BLOCKERS PQRS MEASURE 9,10", Medications = "1037|1048|1916" },
            new { Id = 100166, Name = "ERX EXTENDED BETA BLOCKERS PQRS MEASURE 9,10", Medications = "1037|1048|1916" }
        };

        [HttpGet]
        public IActionResult GetValueSets()
        {
            return Ok(ValueSets);
        }
    }
}