using OrderManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.DataAccess.Repository.Abstract
{
    public interface ICustomerRepository
    {
        List<Customer> GetAllCustomer();

        Customer? GetCustomerById(int id);

        bool DeleteCustomer(Customer customer);

        Customer SaveCustomer(Customer customer);

        Customer UpdateCustomer(Customer customer);
    }
}
