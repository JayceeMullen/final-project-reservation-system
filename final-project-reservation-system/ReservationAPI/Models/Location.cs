﻿namespace ReservationAPI.Models
{
    public class Location
    {
        public Guid LocationID { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public TimeOnly OpenTime { get; set; }
        public TimeOnly CloseTime { get; set; }

    }
}
