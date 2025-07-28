const apiUrl = 'https://localhost:7055/api/recipes';

document.addEventListener('DOMContentLoaded', () => {
    loadRecipes();

    document.getElementById('recipe-form').addEventListener('submit', async (e) => {
        e.preventDefault();
        clearErrors();

        const recipe = {
            title: document.getElementById('title').value,
            ingredients: document.getElementById('ingredients').value,
            steps: document.getElementById('steps').value,
            cookingTime: parseInt(document.getElementById('cookingTime').value),
            dietaryTags: document.getElementById('dietaryTags').value
        };

        const id = document.getElementById('id').value;
        let response;

        try {
            if (id) {
                response = await fetch(`${apiUrl}/${id}`, {
                    method: 'PUT',
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify(recipe)
                });
            } else {
                response = await fetch(apiUrl, {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify(recipe)
                });
            }

            if (response.ok) {
                resetForm();
                loadRecipes();
            } else {
                const errorData = await response.json();
                displayErrors(errorData.errors || errorData);
            }
        } catch (err) {
            console.error("Unexpected error:", err);
            alert("Something went wrong. Please try again.");
        }
    });
});

async function loadRecipes() {
    const response = await fetch(apiUrl);
    const recipes = await response.json();

    const tbody = document.querySelector('#recipes-table tbody');
    tbody.innerHTML = '';

    recipes.forEach(r => {
        const row = document.createElement('tr');
        row.innerHTML = `
            <td>${r.title}</td>
            <td>${r.ingredients}</td>
            <td>${r.steps}</td>
            <td>${r.cookingTime}</td>
            <td>${r.dietaryTags}</td>
            <td>
                <button onclick="editRecipe(${r.id})">Edit</button>
                <button onclick="deleteRecipe(${r.id})">Delete</button>
            </td>
        `;
        tbody.appendChild(row);
    });
}

async function editRecipe(id) {
    try {
        const response = await fetch(`${apiUrl}/${id}`);
        if (!response.ok) throw new Error(`Failed to fetch recipe. Status: ${response.status}`);
        const recipe = await response.json();

        document.getElementById('id').value = recipe.id;
        document.getElementById('title').value = recipe.title;
        document.getElementById('ingredients').value = recipe.ingredients;
        document.getElementById('steps').value = recipe.steps;
        document.getElementById('cookingTime').value = recipe.cookingTime;
        document.getElementById('dietaryTags').value = recipe.dietaryTags;
    } catch (error) {
        console.error("Error loading recipe:", error);
        alert("Failed to load recipe. Please try again.");
    }
}

async function deleteRecipe(id) {
    if (confirm('Are you sure you want to delete this recipe?')) {
        await fetch(`${apiUrl}/${id}`, { method: 'DELETE' });
        loadRecipes();
    }
}

function resetForm() {
    document.getElementById('recipe-form').reset();
    document.getElementById('id').value = '';
    clearErrors();
}

function clearErrors() {
    document.getElementById('error-messages').innerHTML = '';
}

function displayErrors(errors) {
    const errorContainer = document.getElementById('error-messages');
    errorContainer.innerHTML = '';

    for (const key in errors) {
        const errorMessages = errors[key];
        errorMessages.forEach(msg => {
            const div = document.createElement('div');
            div.className = 'error';
            div.innerText = `${key}: ${msg}`;
            errorContainer.appendChild(div);
        });
    }
}
