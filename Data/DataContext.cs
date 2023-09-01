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
        public DbSet<Roster> Roster { get; set; }
        public DbSet<Shift> Shift { get; set; }
        public DbSet<DayOff> DayOff { get; set; }
        public DbSet<LeaveDay> LeaveDay { get; set; }
        public DbSet<LeaveDayType> LeaveDayType { get; set; }
        public DbSet<OverTime> OverTime { get; set; }
        public DbSet<OverTimeType> OverTimeType { get; set; }
        public DbSet<OrgStructure> OrgStructure { get; set; }
    }
}
