using RestaurantOps.Legacy.Models;

namespace RestaurantOps.Legacy.Interfaces;

/// <summary>
/// Repository interface for ingredient-related data operations
/// </summary>
public interface IIngredientRepository
{
    /// <summary>
    /// Gets all ingredients
    /// </summary>
    /// <returns>Collection of ingredients</returns>
    IEnumerable<Ingredient> GetAll();

    /// <summary>
    /// Gets an ingredient by its ID
    /// </summary>
    /// <param name="id">The ingredient ID</param>
    /// <returns>The ingredient or null if not found</returns>
    Ingredient? GetById(int id);

    /// <summary>
    /// Adds a new ingredient
    /// </summary>
    /// <param name="ingredient">The ingredient to add</param>
    void Add(Ingredient ingredient);

    /// <summary>
    /// Updates an existing ingredient
    /// </summary>
    /// <param name="ingredient">The ingredient to update</param>
    void Update(Ingredient ingredient);
}
