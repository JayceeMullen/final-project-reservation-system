namespace ReservationAPI.Models;

public class ReservationRequest
{
    public Guid LocationTimeSlotId { get; set; }
    public Guid CustomerId { get; set; }
    public int NumberOfGuests { get; set; }
    public DateTime ReservationDate { get; set; }
}