using RecipeShare.Models;
using RecipeShare.Repository;

namespace RecipeShare.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeRepository _repository;

        public RecipeService(IRecipeRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Recipe>> GetAllRecipesAsync(string? dietaryTag)
        {
            var recipes = await _repository.GetAllAsync();

            if (!string.IsNullOrEmpty(dietaryTag))
            {
                recipes = recipes
                    .Where(r => r.DietaryTags != null &&
                                r.DietaryTags.ToLower().Contains(dietaryTag.ToLower()));
            }

            return recipes;
        }

        public Task<Recipe?> GetRecipeByIdAsync(int id) =>
            _repository.GetByIdAsync(id);

        public async Task<ServiceResult<Recipe>> AddRecipeAsync(Recipe recipe)
        {
            if (!IsValid(recipe, out var error))
                return ServiceResult<Recipe>.Fail(error);

            var created = await _repository.AddAsync(recipe);
            return ServiceResult<Recipe>.Success(created);
        }

        public async Task<ServiceResult<Recipe>> UpdateRecipeAsync(int id, Recipe recipe)
        {
            if (!IsValid(recipe, out var error))
                return ServiceResult<Recipe>.Fail(error);

            var updated = await _repository.UpdateAsync(id, recipe);
            if (updated == null)
                return ServiceResult<Recipe>.Fail("Recipe not found");

            return ServiceResult<Recipe>.Success(updated);
        }

        public async Task<ServiceResult<bool>> DeleteRecipeAsync(int id)
        {
            var result = await _repository.DeleteAsync(id);
            return result
                ? ServiceResult<bool>.Success(true)
                : ServiceResult<bool>.Fail("Recipe not found");
        }

        private bool IsValid(Recipe recipe, out string error)
        {
            if (string.IsNullOrWhiteSpace(recipe.Ingredients) ||
                string.IsNullOrWhiteSpace(recipe.Steps) ||
                recipe.CookingTime <= 0 ||
                string.IsNullOrWhiteSpace(recipe.DietaryTags))
            {
                error = "All fields must be filled and cooking time must be greater than 0.";
                return false;
            }

            error = string.Empty;
            return true;
        }
    }

}
