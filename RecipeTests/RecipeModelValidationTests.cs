using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using RecipeShare.Models;

namespace RecipeShareTests
{
    public class RecipeModelValidationTests
    {
        private List<ValidationResult> ValidateModel(object model)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, context, results, true);
            return results;
        }

        [Fact]
        public void Valid_Recipe_Should_Pass_Validation()
        {
            var recipe = new Recipe
            {
                Title = "Making Pedi Eggs",
                Ingredients = "Eggs, Flour",
                Steps = "Mix and cook",
                CookingTime = 15,
                DietaryTags = "Vegetarian"
            };

            var results = ValidateModel(recipe);

            results.Should().BeEmpty();
        }

        [Fact]
        public void Recipe_Missing_Ingredients_Should_Fail()
        {
            var recipe = new Recipe
            {
                Title = null,
                Ingredients = null,
                Steps = "Boil water",
                CookingTime = 5,
                DietaryTags = "Vegan"
            };

            var results = ValidateModel(recipe);

            results.Should().ContainSingle(r => r.ErrorMessage.Contains("required"));
        }

        [Fact]
        public void Recipe_With_Zero_CookingTime_Should_Fail()
        {
            var recipe = new Recipe
            {
                Title = "Heating water ",
                Ingredients = "Water",
                Steps = "Boil",
                CookingTime = 0,
                DietaryTags = "None"
            };

            var results = ValidateModel(recipe);

            results.Should().Contain(r => r.ErrorMessage.Contains("greater than zero"));
        }
    }
}
