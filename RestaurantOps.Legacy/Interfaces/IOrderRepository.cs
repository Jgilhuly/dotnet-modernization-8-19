using RestaurantOps.Legacy.Models;

namespace RestaurantOps.Legacy.Interfaces;

/// <summary>
/// Repository interface for order-related data operations
/// </summary>
public interface IOrderRepository
{
    /// <summary>
    /// Creates a new order for the specified table
    /// </summary>
    /// <param name="tableId">The table ID</param>
    /// <returns>The created order</returns>
    Order Create(int tableId);

    /// <summary>
    /// Gets the current open order for a table
    /// </summary>
    /// <param name="tableId">The table ID</param>
    /// <returns>The current order or null if no open order exists</returns>
    Order? GetCurrentByTable(int tableId);

    /// <summary>
    /// Gets an order by its ID
    /// </summary>
    /// <param name="orderId">The order ID</param>
    /// <returns>The order or null if not found</returns>
    Order? GetById(int orderId);

    /// <summary>
    /// Adds a line item to an existing order
    /// </summary>
    /// <param name="orderId">The order ID</param>
    /// <param name="menuItemId">The menu item ID</param>
    /// <param name="quantity">The quantity</param>
    /// <param name="price">The price per item</param>
    void AddLine(int orderId, int menuItemId, int quantity, decimal price);

    /// <summary>
    /// Closes an order and marks it as completed
    /// </summary>
    /// <param name="orderId">The order ID</param>
    void CloseOrder(int orderId);

    /// <summary>
    /// Submits an order to the kitchen
    /// </summary>
    /// <param name="orderId">The order ID</param>
    void SubmitOrder(int orderId);

    /// <summary>
    /// Gets all line items for an order
    /// </summary>
    /// <param name="orderId">The order ID</param>
    /// <returns>Collection of order line items</returns>
    IEnumerable<OrderLine> GetLines(int orderId);
}
