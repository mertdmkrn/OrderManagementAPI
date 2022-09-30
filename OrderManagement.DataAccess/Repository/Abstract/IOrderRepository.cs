using OrderManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.DataAccess.Repository.Abstract
{
    public interface IOrderRepository
    {
		List<Order> GetAllOrder();
	
		Order? GetOrderById(int id);
	
		List<Order> GetOrderByCustomerId(int customerId);
	
		bool DeleteOrder(Order order);
	
		bool DeleteOrders(List<Order> orderList);
	
		Order SaveOrder(Order order);
	
		Order UpdateOrder(Order order);
    }
}
