using System.Collections.Generic;
using Legacy2Modern.Data;
using Legacy2Modern.Data.Repositories;

namespace Legacy2Modern.Business.Services
{
    public class CustomerService
    {
        private readonly CustomerRepository _customerRepository;

        public CustomerService()
        {
            _customerRepository = new CustomerRepository();
        }

        public List<Customer> GetAllCustomers()
        {
            return _customerRepository.GetAllCustomers();
        }

        public Customer GetCustomerById(int customerId)
        {
            return _customerRepository.GetCustomerById(customerId);
        }
        public void AddCustomer(Customer customer)
        {
            _customerRepository.AddCustomer(customer);
        }

        public void UpdateCustomer(Customer customer)
        {
            _customerRepository.UpdateCustomer(customer);
        }
    }
}