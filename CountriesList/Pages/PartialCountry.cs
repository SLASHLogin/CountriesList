namespace CountriesList.Pages;

public partial class Index
{
    public class PartialCountry
    {
        public string Name { get; set; }
        public string Code { get; set; }

        public PartialCountry(string code, string name)
        {
            Name = name;
            Code = code;
        }
    }
}
