using Shubak_Website.Controllers;
using Shubak_Website.Models;
using Shubak_Website.Context;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Globalization;

namespace Shubak_Website.Models
{
    public class UserModel
    {

   
        [EmailAddress]
         [Required(ErrorMessage = "لم تقم بإدخال البريد الإلكتروني")]
        public string Email { get; set; }

       
        [Required(ErrorMessage = "لم تقم بإدخال  الرقم السري")]
         public string Password { get; set; }


        public string UID {get; set;}

        
        [Required(ErrorMessage = "لم تقم بإدخال الاسم  ")]
        public string? Firstname { get; set; } 

        public bool? Artisticevents { get; set; } 
        public bool? Sportsevents { get; set; }
        public bool? Scientificevents { get; set; }
        public bool? Entertainmentevents { get; set; }

         [Required(ErrorMessage = "لم تقم بتحديد نوع الحساب")]
         public string? UserType { get; set; }


        [Required(ErrorMessage = "لم تقم بإدخال الجنس  ")]
         public string? Sex {get; set;}
        
        
    }
} 