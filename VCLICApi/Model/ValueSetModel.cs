
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class ValueSet
{
    [Key] // Marks the property as the primary key
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Automatically generate the value when a new record is inserted
    public int ValueSetId { get; set; } // The ID number for the value set

    [Required] // Makes the field required
    [StringLength(255)] // Sets the maximum length of the string
    public string ValueSetName { get; set; } // The name of the value set

    // Stores medication IDs as a string; you may want to handle this differently, e.g., a related table
    [Required]
    public string Medications { get; set; } // The ID numbers of the medications contained in the value set, separated by a pipe character
}
