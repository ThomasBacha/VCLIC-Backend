using Microsoft.AspNetCore.Mvc;
using VCLICApi.Data;
using Microsoft.EntityFrameworkCore;
namespace VCLICApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ValueSetController : ControllerBase
    {
        private readonly MyDbContext _context;

        public ValueSetController(MyDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetValueSets()
        {
            var valueSets = await _context.ValueSets.ToListAsync();
            return Ok(valueSets);
        }
    }
}