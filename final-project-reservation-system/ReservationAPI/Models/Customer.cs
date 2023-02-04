namespace ReservationAPI.Models
{
    public class Customer
    {
        public Guid CustomerID { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

    }
}
