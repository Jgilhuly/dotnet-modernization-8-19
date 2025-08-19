namespace RestaurantOps.Legacy.Interfaces;

/// <summary>
/// Base interface for business logic services
/// Provides a contract for service layer implementations
/// </summary>
public interface IService
{
    // Marker interface for service layer identification
    // Additional common service operations can be added here as needed
}

/// <summary>
/// Service interface for order-related business logic
/// </summary>
public interface IOrderService : IService
{
    // Future implementation will contain business logic for order processing
    // Examples: ValidateOrder, CalculateTotal, ProcessPayment, etc.
}

/// <summary>
/// Service interface for menu-related business logic
/// </summary>
public interface IMenuService : IService
{
    // Future implementation will contain business logic for menu management
    // Examples: ValidateMenuItem, CheckAvailability, etc.
}

/// <summary>
/// Service interface for table-related business logic
/// </summary>
public interface ITableService : IService
{
    // Future implementation will contain business logic for table management
    // Examples: FindAvailableTable, AssignTable, etc.
}

/// <summary>
/// Service interface for inventory-related business logic
/// </summary>
public interface IInventoryService : IService
{
    // Future implementation will contain business logic for inventory management
    // Examples: CheckReorderLevel, ValidateStockAdjustment, etc.
}

/// <summary>
/// Service interface for employee-related business logic
/// </summary>
public interface IEmployeeService : IService
{
    // Future implementation will contain business logic for employee management
    // Examples: ValidateEmployee, CheckPermissions, etc.
}

/// <summary>
/// Service interface for shift and scheduling-related business logic
/// </summary>
public interface IScheduleService : IService
{
    // Future implementation will contain business logic for scheduling
    // Examples: ValidateShift, CheckAvailability, ApproveTimeOff, etc.
}
