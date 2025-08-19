namespace RestaurantOps.Legacy.Interfaces;

/// <summary>
/// Unit of Work pattern interface for managing transactions across multiple repositories
/// Ensures data consistency and provides a single point for transaction management
/// </summary>
public interface IUnitOfWork : IDisposable
{
    /// <summary>
    /// Repository for order-related operations
    /// </summary>
    IOrderRepository Orders { get; }

    /// <summary>
    /// Repository for menu-related operations
    /// </summary>
    IMenuRepository Menu { get; }

    /// <summary>
    /// Repository for table-related operations
    /// </summary>
    ITableRepository Tables { get; }

    /// <summary>
    /// Repository for inventory-related operations
    /// </summary>
    IInventoryRepository Inventory { get; }

    /// <summary>
    /// Repository for employee-related operations
    /// </summary>
    IEmployeeRepository Employees { get; }

    /// <summary>
    /// Repository for ingredient-related operations
    /// </summary>
    IIngredientRepository Ingredients { get; }

    /// <summary>
    /// Repository for shift and scheduling-related operations
    /// </summary>
    IShiftRepository Shifts { get; }

    /// <summary>
    /// Commits all pending changes within the current transaction
    /// </summary>
    /// <returns>The number of objects written to the underlying database</returns>
    Task<int> SaveChangesAsync();

    /// <summary>
    /// Commits all pending changes within the current transaction (synchronous)
    /// </summary>
    /// <returns>The number of objects written to the underlying database</returns>
    int SaveChanges();

    /// <summary>
    /// Begins a new database transaction
    /// </summary>
    void BeginTransaction();

    /// <summary>
    /// Commits the current transaction
    /// </summary>
    void CommitTransaction();

    /// <summary>
    /// Rolls back the current transaction
    /// </summary>
    void RollbackTransaction();
}
