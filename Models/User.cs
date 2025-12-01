namespace AppointmentBookingSystemWFA.Models
{
    public class User
    {
        public string Username { get; set; } // Primary key
        public string MobilePhone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; } // Hashed password
        public string Role { get; set; } // "Requester" or "Approver"
        public string ApprovalStatus { get; set; } // "Pending", "Accepted", "Rejected"
    }
}