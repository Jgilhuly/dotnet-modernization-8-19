using System.Collections.Generic;
using System.Linq;
using RestaurantOps.Legacy.Models;
using RestaurantOps.Legacy.Interfaces;

namespace RestaurantOps.Legacy.Data
{
	public class MenuRepository : IMenuRepository
	{
		private readonly RestaurantOpsContext _db;

		public MenuRepository(RestaurantOpsContext db)
		{
			_db = db;
		}

		public IEnumerable<MenuItem> GetAll()
		{
			var query = from mi in _db.MenuItems
						join c in _db.Categories on mi.CategoryId equals c.CategoryId
						orderby c.Name, mi.Name
						select new MenuItem
						{
							MenuItemId = mi.MenuItemId,
							CategoryId = mi.CategoryId,
							Name = mi.Name,
							Description = mi.Description,
							Price = mi.Price,
							IsAvailable = mi.IsAvailable,
							CategoryName = c.Name
						};
			return query
				.GroupBy(m => m.Name)
				.Select(g => g.First())
				.ToList();
		}

		public void Add(MenuItem item)
		{
			_db.MenuItems.Add(item);
			_db.SaveChanges();
		}
	}
} 