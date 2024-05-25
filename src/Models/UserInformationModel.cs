using Shubak_Website.Controllers;
using Shubak_Website.Models;
using Shubak_Website.Context;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Globalization;
using Microsoft.AspNetCore.Identity;

namespace Shubak_Website.Models
{
    public class UserInformationModel : IdentityUser
    {

        public string UID {get; set;}
        public string Email { get; set; }
        public string? Firstname { get; set; } 
        public bool? Artisticevents { get; set; } 
        public bool? Sportsevents { get; set; }
        public bool? Scientificevents { get; set; }
        public bool? Entertainmentevents { get; set; }
         public string UserType { get; set; }
         public string? Sex {get; set;}



    public UserInformationModel( string email ,string uid)
    {
        Email = email!;
        UID = uid!;
    }
        
        
    }



    


     
} 