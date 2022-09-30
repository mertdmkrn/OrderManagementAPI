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
    public class CustomerRepository : ICustomerRepository
    {
        public List<Customer> GetAllCustomer()
        { 
           using(var context = new OrderManagementDbContext())
           { 
                return context.Customer.ToList();
           }
        }

        public Customer? GetCustomerById(int id)
        { 
           using(var context = new OrderManagementDbContext())
           { 
                return context.Customer.Find(id);
           }
        }

        public bool DeleteCustomer(Customer customer)
        { 
            try{
                using(var context = new OrderManagementDbContext())
                {
                    context.Customer.Remove(customer);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return true;
        }

        public Customer SaveCustomer(Customer customer)
        { 
            try{
                using(var context = new OrderManagementDbContext())
                {
                    context.Customer.Add(customer);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return customer;
        }

        public Customer UpdateCustomer(Customer customer)
        { 
            try{
                using(var context = new OrderManagementDbContext())
                {
                    context.Customer.Update(customer);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return customer;
        }
    }
}
