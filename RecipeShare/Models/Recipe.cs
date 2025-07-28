using System.ComponentModel.DataAnnotations;
using System.Net;

namespace RecipeShare.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Ingredients are required.")]
        public string Ingredients { get; set; }

        [Required(ErrorMessage = "Steps are required.")]
        public string Steps { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Cooking time must be greater than zero.")]
        public int CookingTime { get; set; }

        [Required(ErrorMessage = "Dietary tags are required.")]
        public string DietaryTags { get; set; }
    }
}
