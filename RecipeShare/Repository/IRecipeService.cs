using RecipeShare.Models;
using RecipeShare.Services;

namespace RecipeShare.Repository
{
    public interface IRecipeService
    {
        Task<IEnumerable<Recipe>> GetAllRecipesAsync(string? dietaryTags);
        Task<Recipe?> GetRecipeByIdAsync(int id);
        Task<ServiceResult<Recipe>> AddRecipeAsync(Recipe recipe);
        Task<ServiceResult<Recipe>> UpdateRecipeAsync(int id, Recipe recipe);
        Task<ServiceResult<bool>> DeleteRecipeAsync(int id);
    }

}
