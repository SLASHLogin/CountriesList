using Microsoft.AspNetCore.Components;

namespace CountriesList.Pages;

public partial class Index : ComponentBase
{
    private Dictionary<string, Continent> _continents = new Dictionary<string, Continent>();

    private string? _selectedContinent;

    private IEnumerable<Country> _selectedCountries = new List<Country>();
    private IEnumerable<string> _selectedCountryNames = new List<string>();

    private int _numberOfCountries = 5;

    private async Task ChangeNumberOfCountries(ChangeEventArgs args)
    {
        await Task.Run(async () =>
        {
            // Clamp to min and max
            _numberOfCountries = Math.Clamp(int.Parse(args.Value?.ToString() ?? "5"), 2, 10);

            if (_selectedContinent is not null)
            {
                await SelectRandomCountries();
            }

            return _numberOfCountries;
        });
    }

    private async Task SelectContinent(ChangeEventArgs args)
    {
        await Task.Run(async () =>
        {
            _selectedContinent = args.Value?.ToString();

            await SelectRandomCountries();

            return _selectedContinent;
        });
    }

    private async Task SelectRandomCountries()
    {
        _selectedCountries = new List<Country>();
        _selectedCountryNames = new List<string>();

        foreach (var continent in _continents)
        {
            if (continent.Key == _selectedContinent)
            {
                foreach (var country in continent.Value.Countries)
                {
                    _selectedCountryNames = _selectedCountryNames.Append(country.Name);
                }
            }
        }

        // Select random country codes  
        while (_selectedCountries.Count() < _numberOfCountries)
        {
            var random = new Random();
            var randomNames = _selectedCountryNames.OrderBy(_ => random.Next()).Take(_numberOfCountries);

            // Get country objects from country codes
            foreach (var name in randomNames)
            {
                var country = await Country.GetCountry(name);

                _selectedCountries = _selectedCountries.Append(country);
            }
        }

        _selectedCountries = _selectedCountries.OrderBy(c => c.name?.official);

        StateHasChanged();
    }
}
