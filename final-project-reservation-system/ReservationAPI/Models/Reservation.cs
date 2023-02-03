namespace ReservationAPI.Models;

public class Reservation
{
    public Guid Id { get; set; }
    public Guid LocationId { get; set; }
    public Guid CustomerId { get; set; }
    public int NumberOfPeople { get; set; }
    public DateTime ReservationStartDateTime { get; set; }
    public DateTime ReservationEndDateTime { get; set; }
}