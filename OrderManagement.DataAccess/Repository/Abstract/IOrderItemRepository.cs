using OrderManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.DataAccess.Repository.Abstract
{
    public interface IOrderItemRepository
    {
		List<OrderItem> GetAllOrderItem();
	
		OrderItem? GetOrderItemById(int id);
	
		List<OrderItem> GetOrderItemByOrderId(int orderId);
	
		bool DeleteOrderItem(OrderItem orderitem);
	
		bool DeleteOrderItems(List<OrderItem> orderItems);
	
		OrderItem SaveOrderItem(OrderItem orderItem);
	
		List<OrderItem> SaveOrderItems(List<OrderItem> orderItems);
	
		OrderItem UpdateOrderItem(OrderItem orderItem);
    }
}
