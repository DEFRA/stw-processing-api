namespace STW.ProcessingApi.Function.Models.Commodity;

public class TypeInfo
{
    public Type Type { get; set; }

    public IList<TaxonomicClass> TaxonomicClasses { get; set; }

    public IList<TaxonomicFamily> TaxonomicFamilies { get; set; }

    public IList<TaxonomicModel> TaxonomicModels { get; set; }

    public IList<TaxonomicSpecies> TaxonomicSpecies { get; set; }
}
