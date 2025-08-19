using System.Collections.Generic;
using System.Linq;
using RestaurantOps.Legacy.Models;
using RestaurantOps.Legacy.Interfaces;

namespace RestaurantOps.Legacy.Data
{
	public class TableRepository : ITableRepository
	{
		private readonly RestaurantOpsContext _db;

		public TableRepository(RestaurantOpsContext db)
		{
			_db = db;
		}

		public IEnumerable<RestaurantTable> GetAll()
		{
			return _db.RestaurantTables
				.OrderBy(t => t.TableId)
				.ToList();
		}

		public void UpdateOccupied(int tableId, bool occupied)
		{
			var table = _db.RestaurantTables.First(t => t.TableId == tableId);
			table.IsOccupied = occupied;
			_db.SaveChanges();
		}
	}
} 