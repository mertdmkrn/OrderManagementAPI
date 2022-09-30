using OrderManagement.DataAccess;
using OrderManagement.DataAccess.Repository;
using OrderManagement.DataAccess.Repository.Abstract;
using OrderManagement.Models;
using OrderManagement.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Services
{
    public class CustomerService : ICustomerService
    {
        public ICustomerRepository _customerRepository;

        public CustomerService()
        { 
            _customerRepository = new CustomerRepository();
        }

        public List<Customer> GetAllCustomer()
        { 
            return _customerRepository.GetAllCustomer();
        }

        public Customer? GetCustomerById(int id)
        { 
            if(id > 0)
                return _customerRepository.GetCustomerById(id);

            throw new Exception("Id must be greater than 0.");
        }

        public bool DeleteCustomer(Customer customer)
        { 
            return _customerRepository.DeleteCustomer(customer);
        }

        public Customer SaveCustomer(Customer customer)
        { 
            return _customerRepository.SaveCustomer(customer);
        }

        public Customer UpdateCustomer(Customer customer)
        { 
            return _customerRepository.UpdateCustomer(customer);
        }

    }
}
