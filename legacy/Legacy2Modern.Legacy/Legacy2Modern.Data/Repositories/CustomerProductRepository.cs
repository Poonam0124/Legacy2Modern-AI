using System;
using System.Collections.Generic;
using System.Linq;

namespace Legacy2Modern.Data.Repositories
{
    public class CustomerProductRepository
    {
        public List<CustomerProduct> GetByCustomerId(int customerId)
        {
            using (var context = new Legacy2ModernDBEntities())
            {
                return context.CustomerProducts
                    .Include("Product")
                    .Where(x => x.CustomerId == customerId)
                    .OrderByDescending(x => x.CreatedDate)
                    .ToList();
            }
        }

        public CustomerProduct GetById(
            int customerProductId)
        {
            using (var context = new Legacy2ModernDBEntities())
            {
                return context.CustomerProducts
                    .FirstOrDefault(x =>
                        x.CustomerProductId ==
                        customerProductId);
            }
        }

        public void Add(CustomerProduct customerProduct)
        {
            using (var context = new Legacy2ModernDBEntities())
            {
                customerProduct.CreatedDate =
                    DateTime.Now;

                context.CustomerProducts.Add(
                    customerProduct);

                context.SaveChanges();
            }
        }

        public void Update(CustomerProduct customerProduct)
        {
            using (var context = new Legacy2ModernDBEntities())
            {
                var existing =
                    context.CustomerProducts
                        .FirstOrDefault(x =>
                            x.CustomerProductId ==
                            customerProduct.CustomerProductId);

                if (existing == null)
                {
                    return;
                }

                existing.ProductId =
                    customerProduct.ProductId;

                existing.SubscriptionNumber =
                    customerProduct.SubscriptionNumber;

                existing.StartDate =
                    customerProduct.StartDate;

                existing.EndDate =
                    customerProduct.EndDate;

                existing.Status =
                    customerProduct.Status;

                context.SaveChanges();
            }
        }
    }
}