const API_BASE = 'https://your-api-url.com/api/recipes'; // Replace with your actual API URL

document.addEventListener('DOMContentLoaded', () => {
    loadRecipes();
    document.getElementById('form').addEventListener('submit', handleFormSubmit);
});

async function loadRecipes() {
    try {
        const res = await fetch(API_BASE);
        const data = await res.json();
        renderRecipes(data);
    } catch (err) {
        showError('Failed to load recipes');
    }
}

function renderRecipes(recipes) {
    const list = document.getElementById('recipes');
    list.innerHTML = '';

    recipes.forEach(recipe => {
        const li = document.createElement('li');
        li.innerHTML = `
      <strong>${recipe.name}</strong> - ${recipe.description}
      <button onclick="editRecipe(${recipe.id})">Edit</button>
      <button onclick="deleteRecipe(${recipe.id})">Delete</button>
    `;
        list.appendChild(li);
    });
}

async function handleFormSubmit(e) {
    e.preventDefault();
    clearError();

    const id = document.getElementById('recipe-id').value;
    const name = document.getElementById('name').value.trim();
    const description = document.getElementById('description').value.trim();

    if (!name || !description) {
        showError('All fields are required.');
        return;
    }

    const recipe = { name, description };

    try {
        const res = await fetch(id ? `${API_BASE}/${id}` : API_BASE, {
            method: id ? 'PUT' : 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(recipe),
        });

        if (!res.ok) {
            const errorData = await res.json();
            showError(errorData.message || 'Validation failed');
            return;
        }

        resetForm();
        loadRecipes();
    } catch (err) {
        showError('Something went wrong');
    }
}

function editRecipe(id) {
    fetch(`${API_BASE}/${id}`)
        .then(res => res.json())
        .then(recipe => {
            document.getElementById('form-title').textContent = 'Edit Recipe';
            document.getElementById('recipe-id').value = recipe.id;
            document.getElementById('name').value = recipe.name;
            document.getElementById('description').value = recipe.description;
        })
        .catch(() => showError('Failed to load recipe details'));
}

async function deleteRecipe(id) {
    if (!confirm('Are you sure you want to delete this recipe?')) return;

    try {
        await fetch(`${API_BASE}/${id}`, { method: 'DELETE' });
        loadRecipes();
    } catch {
        showError('Failed to delete recipe');
    }
}

function resetForm() {
    document.getElementById('form-title').textContent = 'Add New Recipe';
    document.getElementById('form').reset();
    document.getElementById('recipe-id').value = '';
    clearError();
}

function showError(msg) {
    document.getElementById('error').textContent = msg;
}

function clearError() {
    document.getElementById('error').textContent = '';
}
