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
    public class OrderRepository : IOrderRepository
    {
        public List<Order> GetAllOrder()
        { 
           using(var context = new OrderManagementDbContext())
           { 
                return context.Orders.Include("Customer").Include("OrderItems").Include("OrderItems.Product").ToList();
           }
        }

        public Order? GetOrderById(int id)
        { 
           using(var context = new OrderManagementDbContext())
           { 
                return context.Orders.Include("Customer").Include("OrderItems").Include("OrderItems.Product").Where(x=>x.Id == id).FirstOrDefault();
           }
        }

        public List<Order> GetOrderByCustomerId(int customerId)
        { 
           using(var context = new OrderManagementDbContext())
           { 
                return context.Orders.Include("Customer").Include("OrderItems").Include("OrderItems.Product").Where(x=>x.Customer.Id == customerId).ToList();
           }
        }
        
        public bool DeleteOrder(Order order)
        { 
           using(var context = new OrderManagementDbContext())
           { 
                context.Orders.Remove(order);
                context.SaveChanges();
                return true;
           }
        }

        public bool DeleteOrders(List<Order> orderList)
        { 
           using(var context = new OrderManagementDbContext())
           { 
                context.Orders.RemoveRange(orderList);
                context.SaveChanges();
                return true;
           }
        }

        public Order SaveOrder(Order order)
        { 
           using(var context = new OrderManagementDbContext())
           { 
                context.Orders.Add(order);
                context.SaveChanges();
                return order;
           }
        }

        public Order UpdateOrder(Order order)
        { 
           using(var context = new OrderManagementDbContext())
           { 
                context.Orders.Update(order);
                context.SaveChanges();
                return order;
           }
        }

    }
}
