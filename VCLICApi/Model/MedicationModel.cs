namespace VCLICApi.Model
{
    public class Medication
    {
        public int MedicationId { get; set; }
        public string? MedName { get; set; }
        public string? SimpleGenericName { get; set; }
        public string? Route { get; set; }
        public int Outpatients { get; set; }
        public int Inpatients { get; set; }
        public int Patients { get; set; }
    }
}
