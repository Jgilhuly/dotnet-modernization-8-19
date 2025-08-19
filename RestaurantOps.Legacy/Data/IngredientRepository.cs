using System.Collections.Generic;
using System.Linq;
using RestaurantOps.Legacy.Models;
using RestaurantOps.Legacy.Interfaces;

namespace RestaurantOps.Legacy.Data
{
	public class IngredientRepository : IIngredientRepository
	{
		private readonly RestaurantOpsContext _db;

		public IngredientRepository(RestaurantOpsContext db)
		{
			_db = db;
		}

		public IEnumerable<Ingredient> GetAll()
		{
			return _db.Ingredients
				.OrderBy(i => i.Name)
				.ToList();
		}

		public Ingredient? GetById(int id)
		{
			return _db.Ingredients.FirstOrDefault(i => i.IngredientId == id);
		}

		public void Add(Ingredient ing)
		{
			_db.Ingredients.Add(ing);
			_db.SaveChanges();
		}

		public void Update(Ingredient ing)
		{
			_db.Ingredients.Update(ing);
			_db.SaveChanges();
		}
	}
} 