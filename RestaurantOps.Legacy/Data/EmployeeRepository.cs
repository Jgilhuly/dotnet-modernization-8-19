using System.Collections.Generic;
using System.Linq;
using RestaurantOps.Legacy.Models;
using RestaurantOps.Legacy.Interfaces;

namespace RestaurantOps.Legacy.Data
{
	public class EmployeeRepository : IEmployeeRepository
	{
		private readonly RestaurantOpsContext _db;

		public EmployeeRepository(RestaurantOpsContext db)
		{
			_db = db;
		}

		public IEnumerable<Employee> GetAll(bool includeInactive = false)
		{
			var query = _db.Employees.AsQueryable();
			if (!includeInactive)
			{
				query = query.Where(e => e.IsActive);
			}
			return query
				.OrderBy(e => e.LastName)
				.ThenBy(e => e.FirstName)
				.ToList();
		}

		public Employee? GetById(int id)
		{
			return _db.Employees.FirstOrDefault(e => e.EmployeeId == id);
		}

		public void Add(Employee emp)
		{
			_db.Employees.Add(emp);
			_db.SaveChanges();
		}

		public void Update(Employee emp)
		{
			_db.Employees.Update(emp);
			_db.SaveChanges();
		}
	}
} 