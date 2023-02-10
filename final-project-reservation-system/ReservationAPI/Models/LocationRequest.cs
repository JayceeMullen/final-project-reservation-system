namespace ReservationAPI.Models
{
    public class LocationRequest
    {
        public string Name { get; set; }
        public int Capacity { get; set; }
        public string OpenTime { get; set; }
        public string CloseTime { get; set; }
    }
}
