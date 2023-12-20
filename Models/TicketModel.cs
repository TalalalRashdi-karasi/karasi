using System.ComponentModel.DataAnnotations;

namespace Shubak_Website.Models
{
    public class TicketModel
    {
        public int? TicketId {get; set;}
       
        public string? UserId { get; set; }

        public int? TicketCount { get; set; }
        public int? EventId { get; set; }

        public double? TicketPrice { get; set; } 
        public string? EventName { get; set; } 

        public DateTime? EventDate { get; set; }
          public string? EventType { get; set; }
         public DateTime? EventTime { get; set; }

         public string? EventPlace {get; set;}
         public string? FirstName {get; set;}

         public string? Email {get; set;}

         public int? RemainingSeats {get; set;}

         public int? TotalSeats {get; set;}
        
    }
}