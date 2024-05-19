public class MedicationDto
{
    public int MedicationId { get; set; }
    public string MedName { get; set; } = string.Empty;
    public string SimpleGenericName { get; set; } = string.Empty;
    public string Route { get; set; } = string.Empty;
    public int Outpatients { get; set; }
    public int Inpatients { get; set; }
    public int Patients { get; set; }
}
