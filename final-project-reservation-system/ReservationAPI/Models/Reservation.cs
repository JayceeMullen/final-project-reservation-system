namespace ReservationAPI.Models;

public class Reservation
{
    public Guid Id { get; set; }
    public Guid LocationTimeSlotId { get; set; }
    public Guid CustomerId { get; set; }
    public int NumberOfPeople { get; set; }
    
    public DateTime ReservationDate { get; set; }
}