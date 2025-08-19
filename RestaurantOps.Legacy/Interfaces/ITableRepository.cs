using RestaurantOps.Legacy.Models;

namespace RestaurantOps.Legacy.Interfaces;

/// <summary>
/// Repository interface for restaurant table-related data operations
/// </summary>
public interface ITableRepository
{
    /// <summary>
    /// Gets all restaurant tables
    /// </summary>
    /// <returns>Collection of restaurant tables</returns>
    IEnumerable<RestaurantTable> GetAll();

    /// <summary>
    /// Updates the occupied status of a table
    /// </summary>
    /// <param name="tableId">The table ID</param>
    /// <param name="occupied">Whether the table is occupied</param>
    void UpdateOccupied(int tableId, bool occupied);
}
