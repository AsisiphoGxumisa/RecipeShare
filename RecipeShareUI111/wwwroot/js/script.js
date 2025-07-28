document.addEventListener('DOMContentLoaded', function () {
    const recipeGridBody = document.getElementById('recipeGridBody');
    const recipeForm = document.getElementById('recipeForm');
    const apiBaseUrl = 'http://localhost:5000/api/recipes'; // Adjust to your API URL

    // Load recipes when page loads
    loadRecipes();

    // Form submission handler
    recipeForm.addEventListener('submit', function (e) {
        e.preventDefault();

        const recipe = {
            title: document.getElementById('title').value,
            ingredients: document.getElementById('ingredients').value.split(',').map(item => item.trim()),
            steps: document.getElementById('steps').value.split('\n').filter(step => step.trim() !== ''),
            cookingTime: parseInt(document.getElementById('cookingTime').value),
            dietaryTags: document.getElementById('dietaryTags').value.split(',').map(tag => tag.trim())
        };

        addRecipe(recipe);
    });

    // Function to load recipes from API
    function loadRecipes() {
        fetch(apiBaseUrl)
            .then(response => response.json())
            .then(recipes => {
                recipeGridBody.innerHTML = '';
                recipes.forEach(recipe => {
                    addRecipeToGrid(recipe);
                });
            })
            .catch(error => console.error('Error loading recipes:', error));
    }

    // Function to add a recipe to the grid
    function addRecipeToGrid(recipe) {
        const row = document.createElement('tr');

        row.innerHTML = `
            <td>${recipe.title}</td>
            <td>${recipe.ingredients.join(', ')}</td>
            <td>${recipe.steps.join('<br>')}</td>
            <td>${recipe.cookingTime} mins</td>
            <td>${recipe.dietaryTags ? recipe.dietaryTags.join(', ') : ''}</td>
            <td>
                <button class="action-btn delete-btn" data-id="${recipe.id}">Delete</button>
            </td>
        `;

        recipeGridBody.appendChild(row);

        // Add event listener to delete button
        row.querySelector('.delete-btn').addEventListener('click', function () {
            deleteRecipe(recipe.id);
        });
    }

    // Function to add a new recipe via API
    function addRecipe(recipe) {
        fetch(apiBaseUrl, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(recipe)
        })
            .then(response => response.json())
            .then(newRecipe => {
                addRecipeToGrid(newRecipe);
                recipeForm.reset();
            })
            .catch(error => console.error('Error adding recipe:', error));
    }

    // Function to delete a recipe via API
    function deleteRecipe(recipeId) {
        if (confirm('Are you sure you want to delete this recipe?')) {
            fetch(`${apiBaseUrl}/${recipeId}`, {
                method: 'DELETE'
            })
                .then(() => {
                    // Remove the row from the grid
                    const row = document.querySelector(`[data-id="${recipeId}"]`).closest('tr');
                    row.remove();
                })
                .catch(error => console.error('Error deleting recipe:', error));
        }
    }
});