using RestaurantOps.Legacy.Models;

namespace RestaurantOps.Legacy.Interfaces;

/// <summary>
/// Repository interface for employee-related data operations
/// </summary>
public interface IEmployeeRepository
{
    /// <summary>
    /// Gets all employees, optionally including inactive ones
    /// </summary>
    /// <param name="includeInactive">Whether to include inactive employees</param>
    /// <returns>Collection of employees</returns>
    IEnumerable<Employee> GetAll(bool includeInactive = false);

    /// <summary>
    /// Gets an employee by their ID
    /// </summary>
    /// <param name="id">The employee ID</param>
    /// <returns>The employee or null if not found</returns>
    Employee? GetById(int id);

    /// <summary>
    /// Adds a new employee
    /// </summary>
    /// <param name="employee">The employee to add</param>
    void Add(Employee employee);

    /// <summary>
    /// Updates an existing employee
    /// </summary>
    /// <param name="employee">The employee to update</param>
    void Update(Employee employee);
}
