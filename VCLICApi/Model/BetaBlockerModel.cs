namespace VCLICApi.Model
{
    public class BetaBlockerValueSet
    {
        public string ValueSetId { get; set; } = string.Empty; // Primary Key
        public string ValueSetName { get; set; } = string.Empty;
        public string Medications { get; set; } = string.Empty; // Pipe-separated medication IDs
    }
}
