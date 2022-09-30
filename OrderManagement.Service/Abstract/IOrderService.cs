using OrderManagement.Models;
using OrderManagement.Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Services.Abstract
{
    public interface IOrderService
    {
		List<Order> GetAllOrder();
	
		Order? GetOrderById(int id);
	
		List<Order> GetOrderByCustomerId(int customerId);
	
		bool DeleteOrder(Order order);
	
		bool DeleteOrders(List<Order> orderList);
	
		Order? SaveOrder(OrderInfo orderIndo);
	
		Order UpdateOrder(Order order);
    }
}
