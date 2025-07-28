namespace RecipeShare.Repository
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using RecipeShare.Models;

    public interface IRecipeRepository
    {
        Task<IEnumerable<Recipe>> GetAllAsync();
        Task<Recipe> GetByIdAsync(int id);
        Task<Recipe> AddAsync(Recipe recipe);
        Task<Recipe> UpdateAsync(int id, Recipe recipe);
        Task<bool> DeleteAsync(int id);
    }

}
