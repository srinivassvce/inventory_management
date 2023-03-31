using Microsoft.EntityFrameworkCore;
using ProductRegistration.models;

namespace ProductsRegistration.data
{
    public class ProductsDbContext: DbContext
    {
        public DbSet<ProductModel> Products { get; set; }
        public ProductsDbContext(DbContextOptions options): base(options) { }

    }
}
