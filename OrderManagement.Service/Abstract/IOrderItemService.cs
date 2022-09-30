using OrderManagement.Models;
using OrderManagement.Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Services.Abstract
{
    public interface IOrderItemService
    {
		OrderItem? GetOrderItemById(int id);
	
		List<OrderItem> GetOrderItemByOrderId(int orderId);
	
		bool DeleteOrderItem(OrderItem orderitem);
	
		bool DeleteOrderItems(List<OrderItem> orderItems);
	
		List<OrderItem> SaveOrderItems(int id, List<ProductInfo> productInfos);
	
		OrderItem UpdateOrderItem(OrderItem orderItem);
    }
}
