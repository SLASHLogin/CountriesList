using System.Text.Json;

namespace CountriesList.Pages;

public class Country
{
    public Name? name { get; set; }
    public List<string> capital { get; set; } = new List<string>();
    public int? population { get; set; }
    public Dictionary<string, Currency> currencies { get; set; } = new Dictionary<string, Currency>();
    public string? subregion { get; set; }
    public Dictionary<string, string> languages { get; set; } = new Dictionary<string, string>();

    public static async Task<Country> GetCountry(string code)
    {
        // Normalize code
        code = code.Trim().ToUpper();

        // Create country object from country code by calling the API
        var client = new HttpClient();
        client.BaseAddress = new Uri("https://restcountries.com/v3.1/name/"); // TODO: move to config

        var response = await client.GetAsync(code);
        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            var countries = JsonSerializer.Deserialize<List<Country>>(json);

            return countries.First();
        }
        else
        {
            throw new Exception($"Error: {response.StatusCode}");
        }
    }

    public class Name
    {
        public string official { get; set; }
    }

    public class Currency
    {
        public string? name { get; set; }
        public string? symbol { get; set; }
    }

}
