using System;
using System.Collections.Generic;
using System.Linq;
using RestaurantOps.Legacy.Models;
using RestaurantOps.Legacy.Interfaces;

namespace RestaurantOps.Legacy.Data
{
	public class OrderRepository : IOrderRepository
	{
		private readonly RestaurantOpsContext _db;

		public OrderRepository(RestaurantOpsContext db)
		{
			_db = db;
		}

		public Order Create(int tableId)
		{
			var order = new Order { TableId = tableId, CreatedAt = DateTime.UtcNow, Status = "Open" };
			_db.Orders.Add(order);
			_db.SaveChanges();
			return order;
		}

		public Order? GetCurrentByTable(int tableId)
		{
			var order = _db.Orders
				.Where(o => o.TableId == tableId && o.Status == "Open")
				.OrderByDescending(o => o.CreatedAt)
				.FirstOrDefault();
			if (order == null) return null;
			order.Lines = GetLines(order.OrderId).ToList();
			return order;
		}

		public Order? GetById(int orderId)
		{
			var order = _db.Orders.FirstOrDefault(o => o.OrderId == orderId);
			if (order == null) return null;
			order.Lines = GetLines(order.OrderId).ToList();
			return order;
		}

		public void AddLine(int orderId, int menuItemId, int qty, decimal price)
		{
			var line = new OrderLine
			{
				OrderId = orderId,
				MenuItemId = menuItemId,
				Quantity = qty,
				PriceEach = price
			};
			_db.OrderLines.Add(line);
			_db.SaveChanges();
		}

		public void CloseOrder(int orderId)
		{
			var order = _db.Orders.First(o => o.OrderId == orderId);
			order.Status = "Closed";
			order.ClosedAt = DateTime.UtcNow;
			_db.SaveChanges();
		}

		public void SubmitOrder(int orderId)
		{
			var order = _db.Orders.First(o => o.OrderId == orderId);
			order.Status = "Submitted";
			_db.SaveChanges();
		}

		public IEnumerable<OrderLine> GetLines(int orderId)
		{
			var query = from ol in _db.OrderLines
						join mi in _db.MenuItems on ol.MenuItemId equals mi.MenuItemId
						where ol.OrderId == orderId
						select new OrderLine
						{
							OrderLineId = ol.OrderLineId,
							OrderId = ol.OrderId,
							MenuItemId = ol.MenuItemId,
							MenuItemName = mi.Name,
							Quantity = ol.Quantity,
							PriceEach = ol.PriceEach
						};
			return query.ToList();
		}
	}
} 