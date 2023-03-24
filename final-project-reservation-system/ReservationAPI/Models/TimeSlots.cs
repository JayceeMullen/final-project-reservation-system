namespace ReservationAPI.Models
{
    public class TimeSlots
    {
        public Guid LocationTimeSlotID { get; set; }
        public Guid LocationID { get; set; }
        public string SlotStartTime { get; set; }
    }
}
