using RestaurantOps.Legacy.Models;

namespace RestaurantOps.Legacy.Interfaces;

/// <summary>
/// Repository interface for menu-related data operations
/// </summary>
public interface IMenuRepository
{
    /// <summary>
    /// Gets all menu items with their categories
    /// </summary>
    /// <returns>Collection of menu items</returns>
    IEnumerable<MenuItem> GetAll();

    /// <summary>
    /// Adds a new menu item
    /// </summary>
    /// <param name="item">The menu item to add</param>
    void Add(MenuItem item);
}
