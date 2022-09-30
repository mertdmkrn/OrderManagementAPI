using OrderManagement.DataAccess;
using OrderManagement.DataAccess.Repository;
using OrderManagement.DataAccess.Repository.Abstract;
using OrderManagement.Models;
using OrderManagement.Services.Abstract;
using OrderManagement.Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Services
{
    public class OrderService : IOrderService
    {
        public IOrderRepository _orderRepository;
        public IOrderItemRepository _orderItemRepository;

        public OrderService()
        { 
            _orderRepository = new OrderRepository();
            _orderItemRepository = new OrderItemRepository();
        }

        public List<Order> GetAllOrder()
        { 
            return _orderRepository.GetAllOrder();
        }

        public Order? GetOrderById(int id)
        { 
            if(id > 0)
                return _orderRepository.GetOrderById(id);

            throw new Exception("Id must be greater than 0.");
        }

        public List<Order> GetOrderByCustomerId(int customerId)
        { 
           return _orderRepository.GetOrderByCustomerId(customerId);
        }

        public bool DeleteOrder(Order order)
        {
            return _orderRepository.DeleteOrder(order);
        }

        public bool DeleteOrders(List<Order> orderList)
        {
            return _orderRepository.DeleteOrders(orderList);
        }

        public Order SaveOrder(OrderInfo orderInfo)
        {
            Order order = new Order();
            order.CustomerId = orderInfo.Customer.Id;
            _orderRepository.SaveOrder(order);

            List<OrderItem> orderItems = new List<OrderItem>();

            foreach(var product in orderInfo.ProductList){ 
                
                OrderItem orderItem = new OrderItem();
                orderItem.OrderId = order.Id;
                orderItem.ProductId = product.ProductId;
                orderItem.Quantity = product.Quantity;

                orderItems.Add(orderItem);
            }
            
            _orderItemRepository.SaveOrderItems(orderItems);
       
            return GetOrderById(order.Id);
        }

        public Order UpdateOrder(Order order){ 
        
            return _orderRepository.UpdateOrder(order);
        }

    }
}
