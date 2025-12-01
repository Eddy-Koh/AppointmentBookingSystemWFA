using System;

namespace AppointmentBookingSystemWFA.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public string RequesterName { get; set; }
        public DateTime AppointmentDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string Reason { get; set; }
        public string Status { get; set; } // "Pending", "Accepted", "Rejected"
    }
}
