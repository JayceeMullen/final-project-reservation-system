namespace ReservationAPI.Models;

public class ReservationRequest
{
    public Guid LocationId { get; set; }
    public Guid CustomerId { get; set; }
    public int NumberOfPeople { get; set; }
    
    public DateTime ReservationDate { get; set; }
}