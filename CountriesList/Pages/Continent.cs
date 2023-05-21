namespace CountriesList.Pages;

public partial class Index
{
    public class Continent
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public List<PartialCountry> Countries { get; set; }

        public Continent(string code, string name, List<PartialCountry> countries)
        {
            Code = code;
            Name = name;
            Countries = countries;
        }
    }
}
