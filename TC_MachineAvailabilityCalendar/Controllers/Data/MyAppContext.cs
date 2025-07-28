using Microsoft.EntityFrameworkCore;
using TC_MachineAvailabilityCalendar.Models;

namespace TC_MachineAvailabilityCalendar.Controllers.Data
{
    public class MyAppContext : DbContext
    {
        public MyAppContext(DbContextOptions<MyAppContext> options) : base(options) { }
        public DbSet<Item> Items { get; set; }
    }
}
