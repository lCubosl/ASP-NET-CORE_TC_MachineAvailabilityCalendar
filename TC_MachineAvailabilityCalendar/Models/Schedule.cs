namespace TC_MachineAvailabilityCalendar.Models
{
    public class Schedule
    {
        public int Id { get; set; }
        public DateTime ScheduleDate { get; set; }
        
        // Foreign key
        public int ItemId { get; set; }
        public Item Item { get; set; }
    }
}
