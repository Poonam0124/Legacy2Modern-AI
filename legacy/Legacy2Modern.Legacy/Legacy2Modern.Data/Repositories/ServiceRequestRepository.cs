using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Legacy2Modern.Data.Repositories
{
    public class ServiceRequestRepository
    {
        public List<ServiceRequest> GetAll()
        {
            using (var context = new Legacy2ModernDBEntities())
            {
                return context.ServiceRequests
                    .Include("Customer")
                    .Include("CustomerProduct")
                    .Include("Employee")
                    .Include("Employee1")
                    .OrderByDescending(x => x.CreatedDate)
                    .ToList();
            }
        }

        public ServiceRequest GetById(int serviceRequestId)
        {
            using (var context = new Legacy2ModernDBEntities())
            {
                return context.ServiceRequests
                    .Include("Customer")
                    .Include("CustomerProduct")
                    .Include("CustomerProduct.Product")
                    .Include("Employee")
                    .Include("Employee1")
                    .FirstOrDefault(x =>
                        x.ServiceRequestId == serviceRequestId);
            }
        }

        public List<ServiceRequest> Search(
            string searchText,
            string status,
            string priority)
        {
            using (var context = new Legacy2ModernDBEntities())
            {
                var query = context.ServiceRequests
                    .Include("Customer")
                    .Include("CustomerProduct")
                    .Include("Employee")
                    .Include("Employee1")
                    .AsQueryable();

                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    searchText = searchText.Trim();

                    query = query.Where(x =>
                        x.RequestNumber.Contains(searchText) ||
                        x.Subject.Contains(searchText) ||
                        x.Customer.FirstName.Contains(searchText) ||
                        x.Customer.LastName.Contains(searchText));
                }

                if (!string.IsNullOrWhiteSpace(status) &&
                    status != "All")
                {
                    query = query.Where(x =>
                        x.Status == status);
                }

                if (!string.IsNullOrWhiteSpace(priority) &&
                    priority != "All")
                {
                    query = query.Where(x =>
                        x.Priority == priority);
                }

                return query
                    .OrderByDescending(x => x.CreatedDate)
                    .ToList();
            }
        }

        public List<Customer> GetActiveCustomers()
        {
            using (var context = new Legacy2ModernDBEntities())
            {
                return context.Customers
                    .Where(x => x.IsActive)
                    .OrderBy(x => x.FirstName)
                    .ThenBy(x => x.LastName)
                    .ToList();
            }
        }

        public List<CustomerProduct> GetCustomerProducts(
            int customerId)
        {
            using (var context = new Legacy2ModernDBEntities())
            {
                return context.CustomerProducts
                    .Include("Product")
                    .Where(x =>
                        x.CustomerId == customerId &&
                        x.Status == "Active")
                    .OrderBy(x => x.Product.ProductName)
                    .ToList();
            }
        }

        public List<Employee> GetActiveEmployees()
        {
            using (var context = new Legacy2ModernDBEntities())
            {
                return context.Employees
                    .Where(x => x.IsActive)
                    .OrderBy(x => x.FirstName)
                    .ThenBy(x => x.LastName)
                    .ToList();
            }
        }

        public int Create(ServiceRequest serviceRequest)
        {
            using (var context = new Legacy2ModernDBEntities())
            {
                context.ServiceRequests.Add(serviceRequest);
                context.SaveChanges();

                return serviceRequest.ServiceRequestId;
            }
        }
    }
}