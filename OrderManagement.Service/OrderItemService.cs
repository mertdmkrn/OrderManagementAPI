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
    public class OrderItemService : IOrderItemService
    {
        public IOrderItemRepository _orderItemRepository;

        public OrderItemService()
        { 
            _orderItemRepository = new OrderItemRepository();
        }

        public OrderItem? GetOrderItemById(int id)
        { 
            if(id > 0)
                return _orderItemRepository.GetOrderItemById(id);

            throw new Exception("Id must be greater than 0.");
        }

        public List<OrderItem> GetOrderItemByOrderId(int orderId)
        { 
           return _orderItemRepository.GetOrderItemByOrderId(orderId);
        }

        public bool DeleteOrderItem(OrderItem orderItem)
        {
            return _orderItemRepository.DeleteOrderItem(orderItem);
        }

        public bool DeleteOrderItems(List<OrderItem> orderItems)
        {
            return _orderItemRepository.DeleteOrderItems(orderItems);
        }

        public List<OrderItem> SaveOrderItems(int orderId, List<ProductInfo> productInfos)
        {
            List<OrderItem> orderItems = new List<OrderItem>();

            foreach(var product in productInfos){ 
                
                OrderItem orderItem = new OrderItem();
                orderItem.OrderId = orderId;
                orderItem.ProductId = product.ProductId;
                orderItem.Quantity = product.Quantity;

                orderItems.Add(orderItem);
            }

            _orderItemRepository.SaveOrderItems(orderItems);

            return GetOrderItemByOrderId(orderId);
        }

        public OrderItem UpdateOrderItem(OrderItem orderItem)
        {
            return _orderItemRepository.UpdateOrderItem(orderItem);
        }
    }
}
