using Microsoft.AspNetCore.Mvc;
using RecipeShare.Models;
using RecipeShare.Repository;

namespace RecipeShare.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecipesController : ControllerBase
    {
        private readonly IRecipeService _recipeService;

        public RecipesController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetRecipes([FromQuery] string? dietaryTag)
        {
            var recipes = await _recipeService.GetAllRecipesAsync(dietaryTag);
            return Ok(recipes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _recipeService.GetRecipeByIdAsync(id);
            return result != null ? Ok(result) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRecipe([FromBody] Recipe recipe)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _recipeService.AddRecipeAsync(recipe);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Recipe recipe)
        {
            var result = await _recipeService.UpdateRecipeAsync(id, recipe);
            return result.IsSuccess
                ? Ok(result.Data)
                : NotFound(result.ErrorMessage);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _recipeService.DeleteRecipeAsync(id);
            return result.IsSuccess
                ? NoContent()
                : NotFound(result.ErrorMessage);
        }
    }
}
