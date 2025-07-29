using Microsoft.EntityFrameworkCore;
using TC_MachineAvailabilityCalendar.Models;

namespace TC_MachineAvailabilityCalendar.Controllers.Data
{
    public class MyAppContext : DbContext
    {
        public MyAppContext(DbContextOptions<MyAppContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Schedule>()
                .HasOne(s => s.Item)
                .WithMany(i => i.Schedules)
                .HasForeignKey(s => s.ItemId);

        }

        public DbSet<Item> Items { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
    }
}
