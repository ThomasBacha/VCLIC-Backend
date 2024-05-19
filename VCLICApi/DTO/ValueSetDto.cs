public class ValueSetDto
{
    public string ValueSetId { get; set; } = string.Empty;
    public string ValueSetName { get; set; } = string.Empty;
    public List<int> Medications { get; set; } = new List<int>();
}
