using System.Collections.Generic;
using System.Linq;

namespace Legacy2Modern.Data.Repositories
{
    public class ProductRepository
    {
        public List<Product> GetActiveProducts()
        {
            using (var context = new Legacy2ModernDBEntities())
            {
                return context.Products
                    .Where(x => x.IsActive)
                    .OrderBy(x => x.ProductName)
                    .ToList();
            }
        }

        public Product GetById(int productId)
        {
            using (var context = new Legacy2ModernDBEntities())
            {
                return context.Products
                    .FirstOrDefault(x =>
                        x.ProductId == productId);
            }
        }
    }
}