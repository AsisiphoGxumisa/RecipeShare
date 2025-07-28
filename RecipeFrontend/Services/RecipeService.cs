using System.Net.Http.Json;
using RecipeFrontend.Models;

public class RecipeService
{
    private readonly HttpClient _http;

    public RecipeService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<Recipe>> GetRecipesAsync()
    {
        try
        {
            return await _http.GetFromJsonAsync<List<Recipe>>("https://localhost:7055/api/recipes");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
        
    }
}
