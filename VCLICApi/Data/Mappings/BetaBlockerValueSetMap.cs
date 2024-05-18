using CsvHelper.Configuration;
using VCLICApi.Model;

namespace VCLICApi.Data.Mappings
{
    public class BetaBlockerValueSetMap : ClassMap<BetaBlockerValueSet>
    {
        public BetaBlockerValueSetMap()
        {
            Map(m => m.ValueSetId).Name("value_set_id");
            Map(m => m.ValueSetName).Name("value_set_name");
            Map(m => m.Medications).Name("medications");
        }
    }
}
