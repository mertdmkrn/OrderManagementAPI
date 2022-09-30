using Microsoft.EntityFrameworkCore;
using OrderManagement.DataAccess.Repository.Abstract;
using OrderManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.DataAccess.Repository
{
    public class OrderItemRepository : IOrderItemRepository
    {
        public List<OrderItem> GetAllOrderItem()
        { 
           using(var context = new OrderManagementDbContext())
           { 
                return context.OrderItem.Include("Product").ToList();
           }
        }

        public OrderItem? GetOrderItemById(int id)
        { 
           using(var context = new OrderManagementDbContext())
           { 
                return context.OrderItem.Find(id);
           }
        }

        public List<OrderItem> GetOrderItemByOrderId(int orderId)
        { 
           using(var context = new OrderManagementDbContext())
           { 
                return context.OrderItem.Include("Product").Where(x=>x.Order.Id == orderId).ToList();
           }
        }
        
        public bool DeleteOrderItem(OrderItem orderitem)
        { 
           using(var context = new OrderManagementDbContext())
           { 
                context.OrderItem.Remove(orderitem);
                context.SaveChanges();
                return true;
           }
        }

        public bool DeleteOrderItems(List<OrderItem> orderItems)
        { 
           using(var context = new OrderManagementDbContext())
           { 
                context.OrderItem.RemoveRange(orderItems);
                context.SaveChanges();
                return true;
           }
        }
        
        public OrderItem SaveOrderItem(OrderItem orderItem)
        { 
           using(var context = new OrderManagementDbContext())
           { 
                context.OrderItem.Add(orderItem);
                context.SaveChanges();
                return orderItem;
           }
        }

        public List<OrderItem> SaveOrderItems(List<OrderItem> orderItems)
        { 
           using(var context = new OrderManagementDbContext())
           { 
                context.OrderItem.AddRange(orderItems);
                context.SaveChanges();
                return orderItems;
           }
        }

        public OrderItem UpdateOrderItem(OrderItem orderItem)
        { 
           using(var context = new OrderManagementDbContext())
           { 
                context.OrderItem.Update(orderItem);
                context.SaveChanges();
                return orderItem;
           }
        }

    }
}
