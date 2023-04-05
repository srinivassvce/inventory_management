using Microsoft.EntityFrameworkCore;
using UserApi.models;

namespace UserApi.data
{
    public class IdentityDbContext: DbContext
    {
        public DbSet<User> Users { get; set; }
        public IdentityDbContext(DbContextOptions options): base(options) { }

    }
}
