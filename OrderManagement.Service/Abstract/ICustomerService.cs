using OrderManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Services.Abstract
{
    public interface ICustomerService
    {
        List<Customer> GetAllCustomer();

        Customer? GetCustomerById(int id);

        bool DeleteCustomer(Customer customer);

        Customer SaveCustomer(Customer customer);

        Customer UpdateCustomer(Customer customer);
    }
}
