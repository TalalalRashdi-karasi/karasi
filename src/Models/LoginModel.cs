using Shubak_Website.Controllers;
using Shubak_Website.Models;
using Shubak_Website.Context;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Shubak_Website.Models
{
    public class LoginModel
    {


    public string ReturnUrl { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
      
    }
}

