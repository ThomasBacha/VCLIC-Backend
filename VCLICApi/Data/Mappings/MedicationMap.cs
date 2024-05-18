using CsvHelper.Configuration;
using VCLICApi.Model;

namespace VCLICApi.Data.Mappings
{
    public class MedicationMap : ClassMap<Medication>
    {
        public MedicationMap()
        {
            Map(m => m.MedicationId).Name("medication_id");
            Map(m => m.MedName).Name("medname");
            Map(m => m.SimpleGenericName).Name("simple_generic_name");
            Map(m => m.Route).Name("route");
            Map(m => m.Outpatients).Name("outpatients");
            Map(m => m.Inpatients).Name("inpatients");
            Map(m => m.Patients).Name("patients");
        }
    }
}
