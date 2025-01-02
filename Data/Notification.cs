using odaurehonbe.Data;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;



public class Notification
    {
        [Key]
        public int NotificationID { get; set; }

        [Required]
        public int TicketID { get; set; }

        public Ticket Ticket { get; set; }

       
        public int? ClerkID { get; set; }

        public TicketClerk? Clerk { get; set; }

        [Required]
        public string Message { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now.ToUniversalTime();

        public bool IsHandled { get; set; } = false;
    }

