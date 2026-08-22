using System.Collections.Generic;
using Legacy2Modern.Data;
using Legacy2Modern.Data.Repositories;

namespace Legacy2Modern.Business.Services
{
    public class CustomerService
    {
        private readonly CustomerRepository _customerRepository;
        private readonly ProductRepository _productRepository;

        private readonly CustomerContactRepository _customerContactRepository;
        private readonly CustomerProductRepository _customerProductRepository;

        public CustomerService()
        {
            _customerRepository =
                new CustomerRepository();

            _customerContactRepository =
                new CustomerContactRepository();

            _customerProductRepository =
                new CustomerProductRepository();

            _productRepository =
                new ProductRepository();
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

        public List<CustomerContact> GetCustomerContacts(
    int customerId)
        {
            return _customerContactRepository
                .GetByCustomerId(customerId);
        }

        public CustomerContact GetCustomerContactById(
            int customerContactId)
        {
            return _customerContactRepository
                .GetById(customerContactId);
        }

        public void AddCustomerContact(
            CustomerContact contact)
        {
            _customerContactRepository.Add(contact);
        }

        public void UpdateCustomerContact(
            CustomerContact contact)
        {
            _customerContactRepository.Update(contact);
        }
        public List<CustomerProduct> GetCustomerProducts(
    int customerId)
        {
            return _customerProductRepository
                .GetByCustomerId(customerId);
        }

        public CustomerProduct GetCustomerProductById(
            int customerProductId)
        {
            return _customerProductRepository
                .GetById(customerProductId);
        }

        public void AddCustomerProduct(
            CustomerProduct customerProduct)
        {
            _customerProductRepository
                .Add(customerProduct);
        }

        public void UpdateCustomerProduct(
            CustomerProduct customerProduct)
        {
            _customerProductRepository
                .Update(customerProduct);
        }

        public List<Product> GetActiveProducts()
        {
            return _productRepository
                .GetActiveProducts();
        }
    }
}