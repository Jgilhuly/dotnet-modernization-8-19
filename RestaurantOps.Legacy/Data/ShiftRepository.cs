using System;
using System.Collections.Generic;
using System.Linq;
using RestaurantOps.Legacy.Models;
using RestaurantOps.Legacy.Interfaces;

namespace RestaurantOps.Legacy.Data
{
	public class ShiftRepository : IShiftRepository
	{
		private readonly RestaurantOpsContext _db;

		public ShiftRepository(RestaurantOpsContext db)
		{
			_db = db;
		}

		public IEnumerable<Shift> GetByDateRange(DateTime start, DateTime end)
		{
			var query = from s in _db.Shifts
						join e in _db.Employees on s.EmployeeId equals e.EmployeeId
						where s.ShiftDate >= start.Date && s.ShiftDate <= end.Date
						orderby s.ShiftDate, s.StartTime
						select new Shift
						{
							ShiftId = s.ShiftId,
							EmployeeId = s.EmployeeId,
							EmployeeName = e.FirstName + " " + e.LastName,
							ShiftDate = s.ShiftDate,
							StartTime = s.StartTime,
							EndTime = s.EndTime,
							Role = s.Role
						};
			return query.ToList();
		}

		public void Add(Shift shift)
		{
			_db.Shifts.Add(shift);
			_db.SaveChanges();
		}

		public void Delete(int shiftId)
		{
			var s = _db.Shifts.First(x => x.ShiftId == shiftId);
			_db.Shifts.Remove(s);
			_db.SaveChanges();
		}

		public IEnumerable<TimeOff> GetPendingTimeOff()
		{
			var query = from t in _db.TimeOff
						join e in _db.Employees on t.EmployeeId equals e.EmployeeId
						where t.Status == "Pending"
						select new TimeOff
						{
							TimeOffId = t.TimeOffId,
							EmployeeId = t.EmployeeId,
							EmployeeName = e.FirstName + " " + e.LastName,
							StartDate = t.StartDate,
							EndDate = t.EndDate,
							Status = t.Status
						};
			return query.ToList();
		}

		public void SetTimeOffStatus(int timeOffId, string status)
		{
			var t = _db.TimeOff.First(x => x.TimeOffId == timeOffId);
			t.Status = status;
			_db.SaveChanges();
		}

		public bool HasOverlap(int employeeId, DateTime date, TimeSpan start, TimeSpan end)
		{
			return _db.Shifts.Any(s =>
				s.EmployeeId == employeeId && s.ShiftDate == date.Date && start < s.EndTime && end > s.StartTime);
		}

		public bool IsDuringApprovedTimeOff(int employeeId, DateTime date)
		{
			return _db.TimeOff.Any(t => t.EmployeeId == employeeId && t.Status == "Approved" && date.Date >= t.StartDate && date.Date <= t.EndDate);
		}
	}
} 