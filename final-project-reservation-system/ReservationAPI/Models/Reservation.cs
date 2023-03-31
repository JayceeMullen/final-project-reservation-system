namespace ReservationAPI.Models;

public class Reservation
{
    public Guid ReservationId { get; set; }
    public Guid LocationTimeSlotId { get; set; }
    public Guid CustomerId { get; set; }
    public int NumberOfGuests { get; set; }
    public DateTime ReservationDate { get; set; }
}