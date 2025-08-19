using RestaurantOps.Legacy.Models;

namespace RestaurantOps.Legacy.Interfaces;

/// <summary>
/// Repository interface for shift and time-off related data operations
/// </summary>
public interface IShiftRepository
{
    /// <summary>
    /// Gets shifts within a date range
    /// </summary>
    /// <param name="start">Start date</param>
    /// <param name="end">End date</param>
    /// <returns>Collection of shifts</returns>
    IEnumerable<Shift> GetByDateRange(DateTime start, DateTime end);

    /// <summary>
    /// Adds a new shift
    /// </summary>
    /// <param name="shift">The shift to add</param>
    void Add(Shift shift);

    /// <summary>
    /// Deletes a shift
    /// </summary>
    /// <param name="shiftId">The shift ID</param>
    void Delete(int shiftId);

    /// <summary>
    /// Gets all pending time-off requests
    /// </summary>
    /// <returns>Collection of pending time-off requests</returns>
    IEnumerable<TimeOff> GetPendingTimeOff();

    /// <summary>
    /// Sets the status of a time-off request
    /// </summary>
    /// <param name="timeOffId">The time-off request ID</param>
    /// <param name="status">The new status</param>
    void SetTimeOffStatus(int timeOffId, string status);

    /// <summary>
    /// Checks if an employee has overlapping shifts
    /// </summary>
    /// <param name="employeeId">The employee ID</param>
    /// <param name="date">The shift date</param>
    /// <param name="start">The start time</param>
    /// <param name="end">The end time</param>
    /// <returns>True if there's an overlap</returns>
    bool HasOverlap(int employeeId, DateTime date, TimeSpan start, TimeSpan end);

    /// <summary>
    /// Checks if an employee is on approved time-off for a given date
    /// </summary>
    /// <param name="employeeId">The employee ID</param>
    /// <param name="date">The date to check</param>
    /// <returns>True if the employee is on approved time-off</returns>
    bool IsDuringApprovedTimeOff(int employeeId, DateTime date);
}
