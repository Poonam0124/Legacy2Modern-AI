using System.Collections.Generic;
using System.Linq;

namespace Legacy2Modern.Data.Repositories
{
    public class CustomerRepository
    {
        public List<Customer> GetAllCustomers()
        {
            using (var context = new Legacy2ModernDBEntities())
            {
                return context.Customers
                    .OrderBy(x => x.LastName)
                    .ThenBy(x => x.FirstName)
                    .ToList();
            }
        }

        public Customer GetCustomerById(int customerId)
        {
            using (var context = new Legacy2ModernDBEntities())
            {
                return context.Customers
                    .FirstOrDefault(x => x.CustomerId == customerId);
            }
        }
        public void AddCustomer(Customer customer)
        {
            using (var context = new Legacy2ModernDBEntities())
            {
                customer.CreatedDate = System.DateTime.Now;
                customer.IsActive = true;

                context.Customers.Add(customer);
                context.SaveChanges();
            }
        }

        public void UpdateCustomer(Customer customer)
        {
            using (var context = new Legacy2ModernDBEntities())
            {
                var existingCustomer =
                    context.Customers
                        .FirstOrDefault(x =>
                            x.CustomerId == customer.CustomerId);

                if (existingCustomer == null)
                {
                    return;
                }

                existingCustomer.CustomerCode = customer.CustomerCode;
                existingCustomer.FirstName = customer.FirstName;
                existingCustomer.LastName = customer.LastName;
                existingCustomer.Email = customer.Email;
                existingCustomer.Phone = customer.Phone;
                existingCustomer.Status = customer.Status;
                existingCustomer.ModifiedDate = System.DateTime.Now;

                context.SaveChanges();
            }
        }
    }
}