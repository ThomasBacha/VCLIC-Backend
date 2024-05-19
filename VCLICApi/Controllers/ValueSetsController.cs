using Microsoft.AspNetCore.Mvc;
using VCLICApi.Data;
using Microsoft.EntityFrameworkCore;
using VCLICApi.Model;

[ApiController]
[Route("[controller]")]
public class ValueSetsController : ControllerBase
{
    private readonly MyDbContext _context;

    public ValueSetsController(MyDbContext context)
    {
        _context = context;
    }
    [HttpGet]
    public async Task<IActionResult> GetAllValueSets()
    {
        var valueSets = await _context.BetaBlockerValueSets.ToListAsync();

        var valueSetDtos = valueSets.Select(vs => new ValueSetDto
        {
            ValueSetId = vs.ValueSetId,
            ValueSetName = vs.ValueSetName,
            Medications = vs.Medications?.Split('|').Select(int.Parse).ToList() ?? new List<int>()
        }).ToList();

        return Ok(valueSetDtos);
    }



    [HttpDelete("delete-all-value-sets")]
    public async Task<IActionResult> DeleteAllValueSets()
    {
        var medicationExists = await _context.Medications.AnyAsync();
        var betaBlockerValueSetsExists = await _context.BetaBlockerValueSets.AnyAsync();

        if (!medicationExists && !betaBlockerValueSetsExists)
        {
            return NotFound("No records found in both tables.");
        }

        if (medicationExists)
        {
            _context.Medications.RemoveRange(_context.Medications);
        }

        if (betaBlockerValueSetsExists)
        {
            _context.BetaBlockerValueSets.RemoveRange(_context.BetaBlockerValueSets);
        }

        await _context.SaveChangesAsync();

        return Ok("All records in both tables have been deleted.");
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetMedicationsForValueSet(string id)
    {
        var valueSet = await _context.BetaBlockerValueSets
            .FirstOrDefaultAsync(vs => vs.ValueSetId == id);

        if (valueSet == null)
        {
            return NotFound();
        }

        var medicationIds = valueSet.Medications?.Split('|').Select(int.Parse).ToList() ?? new List<int>();

        var medications = await _context.Medications
            .Where(m => medicationIds.Contains(m.MedicationId))
            .Select(m => new MedicationDto
            {
                MedicationId = m.MedicationId,
                MedName = m.MedName,
                SimpleGenericName = m.SimpleGenericName,
                Route = m.Route,
                Outpatients = m.Outpatients,
                Inpatients = m.Inpatients,
                Patients = m.Patients
            })
            .ToListAsync();

        var result = new
        {
            valueSet.ValueSetId,
            valueSet.ValueSetName,
            Medications = medications
        };

        return Ok(result);
    }

    [HttpGet("compare")]
    public async Task<IActionResult> CompareValueSets([FromQuery] string valueSetIds)
    {
        // Split the comma-separated string into an array of valueSetIds
        var valueSetIdArray = valueSetIds.Split(',');

        var valueSets = await _context.BetaBlockerValueSets
            .Where(vs => valueSetIdArray.Contains(vs.ValueSetId))
            .ToListAsync();

        var allMedicationIds = valueSets
            .SelectMany(vs => vs.Medications?.Split('|').Select(int.Parse) ?? new List<int>())
            .Distinct()
            .ToList();

        var commonMedicationIds = allMedicationIds
            .Where(medId => valueSets.All(vs => vs.Medications?.Split('|').Contains(medId.ToString()) ?? false))
            .ToList();

        var commonMedications = await _context.Medications
            .Where(m => commonMedicationIds.Contains(m.MedicationId))
            .Select(m => new MedicationDto
            {
                MedicationId = m.MedicationId,
                MedName = m.MedName,
                SimpleGenericName = m.SimpleGenericName,
                Route = m.Route,
                Outpatients = m.Outpatients,
                Inpatients = m.Inpatients,
                Patients = m.Patients
            })
            .ToListAsync();

        var uniqueMedications = new Dictionary<string, List<MedicationDto>>();

        foreach (var valueSet in valueSets)
        {
            var medicationIds = valueSet.Medications?.Split('|').Select(int.Parse) ?? new List<int>();
            var uniqueMedicationIds = medicationIds.Except(commonMedicationIds);

            var uniqueMeds = await _context.Medications
                .Where(m => uniqueMedicationIds.Contains(m.MedicationId))
                .Select(m => new MedicationDto
                {
                    MedicationId = m.MedicationId,
                    MedName = m.MedName,
                    SimpleGenericName = m.SimpleGenericName,
                    Route = m.Route,
                    Outpatients = m.Outpatients,
                    Inpatients = m.Inpatients,
                    Patients = m.Patients
                })
                .ToListAsync();

            uniqueMedications[valueSet.ValueSetId] = uniqueMeds;
        }

        return Ok(new { commonMedications, uniqueMedications });
    }


    [HttpPost]
    public async Task<IActionResult> AddValueSet([FromBody] ValueSetDto newValueSet)
    {
        var valueSet = new BetaBlockerValueSet
        {
            ValueSetId = newValueSet.ValueSetId,
            ValueSetName = newValueSet.ValueSetName,
            Medications = newValueSet.Medications != null ? string.Join("|", newValueSet.Medications) : null
        };

        _context.BetaBlockerValueSets.Add(valueSet);
        await _context.SaveChangesAsync();

        return Ok(valueSet);
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteValueSet(string id)
    {
        var valueSet = await _context.BetaBlockerValueSets.FindAsync(id);
        if (valueSet == null)
        {
            return NotFound();
        }

        _context.BetaBlockerValueSets.Remove(valueSet);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Value set deleted successfully." });
    }

    [HttpGet("full-join")]
    public async Task<IActionResult> GetFullJoin()
    {
        var valueSets = await _context.BetaBlockerValueSets.ToListAsync();
        var medications = await _context.Medications.ToListAsync();

        var result = valueSets.SelectMany(vs =>
            vs.Medications.Split('|').Select(medId => new
            {
                ValueSetId = vs.ValueSetId,
                ValueSetName = vs.ValueSetName,
                MedicationId = medId,
                Medication = medications.FirstOrDefault(m => m.MedicationId.ToString() == medId)
            })).ToList();

        return Ok(result);
    }

}
