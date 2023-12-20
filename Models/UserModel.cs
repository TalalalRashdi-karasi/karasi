using System.ComponentModel.DataAnnotations;

namespace Shubak_Website.Models
{
    public class UserModel
    {

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
         public string Password { get; set; }
        public string UID {get; set;}
        public string? Firstname { get; set; } 
        public bool? Artisticevents { get; set; } 
        public bool? Sportsevents { get; set; }
        public bool? Scientificevents { get; set; }
        public bool? Entertainmentevents { get; set; }
         public bool? UserType { get; set; }
         public string? Sex {get; set;}
        
        
    }
} 