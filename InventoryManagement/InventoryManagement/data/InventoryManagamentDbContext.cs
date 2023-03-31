using Microsoft.EntityFrameworkCore;
using InventoryManagement.models;

namespace InventoryManagement.data
{
    public class InventoryManagamentDbContext: DbContext
    {
        public DbSet<ProductItemModel> ProductItems { get; set; }
        public InventoryManagamentDbContext(DbContextOptions options): base(options) { }

    }
}
