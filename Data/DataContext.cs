using Microsoft.EntityFrameworkCore;
using ShopAccBE.Model;

namespace ShopAccBE.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<UserInfo> UserInfo { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<AdminInfo> AdminInfo { get; set; }
    }
}
