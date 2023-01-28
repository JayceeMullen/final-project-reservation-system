namespace ReservationAPI.Models;

public class ReservationRequest
{
    public Guid LocationId { get; set; }
    public Guid CustomerId { get; set; }
    public int NumberOfPeople { get; set; }
    public DateTime ReservationStartDateTime { get; set; }
    public DateTime ReservationEndDateTime { get; set; }
}