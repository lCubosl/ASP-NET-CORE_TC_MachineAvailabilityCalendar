using Microsoft.EntityFrameworkCore;
using TC_MachineAvailabilityCalendar.Models;

namespace TC_MachineAvailabilityCalendar.Controllers.Data
{
    public class MyAppContext : DbContext
    {
        public MyAppContext(DbContextOptions<MyAppContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Mapping Schedule to Item
            // Schedule has one Item
            // Item has one Schedule       
            modelBuilder.Entity<Schedule>()
                .HasOne(s => s.Item)
                .WithMany(i => i.Schedules)
                .HasForeignKey(s => s.ItemId)
                .OnDelete(DeleteBehavior.Cascade);

            // create test item
            modelBuilder.Entity<Item>().HasData(
                new Item { Id=1, Name = "TestMachine", ImageUrl="004.jpg" }
                );

            // create test Schedule ONCE 
            modelBuilder.Entity<Schedule>().HasData(
                new Schedule { Id=1, ScheduleDate= new DateTime(2025, 7, 30), ItemId = 7}
                );
        }

        public DbSet<Item> Items { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
    }
}
