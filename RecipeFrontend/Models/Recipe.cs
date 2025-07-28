using System.ComponentModel.DataAnnotations;

namespace RecipeFrontend.Models
{
    public class Recipe
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        public string Ingredients { get; set; }

        [Required]
        public string Steps { get; set; }

        [Range(1, 1000)]
        public int CookingTime { get; set; }
        public string DietaryTags { get; set; }
    }
}
