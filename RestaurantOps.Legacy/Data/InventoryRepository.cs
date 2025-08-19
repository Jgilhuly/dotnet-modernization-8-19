using System;
using System.Collections.Generic;
using System.Linq;
using RestaurantOps.Legacy.Models;
using RestaurantOps.Legacy.Interfaces;

namespace RestaurantOps.Legacy.Data
{
	public class InventoryRepository : IInventoryRepository
	{
		private readonly RestaurantOpsContext _db;

		public InventoryRepository(RestaurantOpsContext db)
		{
			_db = db;
		}

		public void AdjustStock(int ingredientId, decimal quantityChange, string? notes)
		{
			var tx = new InventoryTx
			{
				IngredientId = ingredientId,
				QuantityChange = quantityChange,
				Notes = notes,
				TxDate = DateTime.UtcNow
			};
			_db.InventoryTx.Add(tx);

			var ing = _db.Ingredients.First(i => i.IngredientId == ingredientId);
			ing.QuantityOnHand += quantityChange;

			_db.SaveChanges();
		}

		public IEnumerable<InventoryTx> GetByIngredient(int ingredientId)
		{
			var query = from tx in _db.InventoryTx
						join i in _db.Ingredients on tx.IngredientId equals i.IngredientId
						where tx.IngredientId == ingredientId
						orderby tx.TxDate descending
						select new InventoryTx
						{
							TxId = tx.TxId,
							IngredientId = tx.IngredientId,
							TxDate = tx.TxDate,
							QuantityChange = tx.QuantityChange,
							Notes = tx.Notes,
							IngredientName = i.Name
						};
			return query.ToList();
		}
	}
} 