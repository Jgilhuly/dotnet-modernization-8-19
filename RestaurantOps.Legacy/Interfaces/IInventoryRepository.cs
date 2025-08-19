using RestaurantOps.Legacy.Models;

namespace RestaurantOps.Legacy.Interfaces;

/// <summary>
/// Repository interface for inventory-related data operations
/// </summary>
public interface IInventoryRepository
{
    /// <summary>
    /// Adjusts the stock level for an ingredient
    /// </summary>
    /// <param name="ingredientId">The ingredient ID</param>
    /// <param name="quantityChange">The quantity change (positive or negative)</param>
    /// <param name="notes">Optional notes for the transaction</param>
    void AdjustStock(int ingredientId, decimal quantityChange, string? notes);

    /// <summary>
    /// Gets all inventory transactions for a specific ingredient
    /// </summary>
    /// <param name="ingredientId">The ingredient ID</param>
    /// <returns>Collection of inventory transactions</returns>
    IEnumerable<InventoryTx> GetByIngredient(int ingredientId);
}
