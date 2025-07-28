
namespace RecipeShare.Repository
{
    using Microsoft.EntityFrameworkCore;
    using RecipeShare.Data;
    using RecipeShare.Models;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class RecipeRepository : IRecipeRepository
    {
        private readonly RecipeContext _context;

        public RecipeRepository(RecipeContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Recipe>> GetAllAsync() => await _context.Recipes.ToListAsync();

        public async Task<Recipe> GetByIdAsync(int id) => await _context.Recipes.FindAsync(id);

        public async Task<Recipe> AddAsync(Recipe recipe)
        {
            _context.Recipes.Add(recipe);
            await _context.SaveChangesAsync();
            return recipe;
        }

        public async Task<Recipe> UpdateAsync(int id, Recipe recipe)
        {
            var existing = await _context.Recipes.FindAsync(id);
            if (existing == null) return null;

            existing.Ingredients = recipe.Ingredients;
            existing.Steps = recipe.Steps;
            existing.CookingTime = recipe.CookingTime;
            existing.DietaryTags = recipe.DietaryTags;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null) return false;

            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}
