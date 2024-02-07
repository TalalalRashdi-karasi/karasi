using Microsoft.AspNetCore.Mvc;
using Shubak_Website.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Newtonsoft.Json;
using Shubak_Website.Repositories;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Org.BouncyCastle.Bcpg;
using Firebase.Auth;
using IdentityServer4.Extensions;
using System.Reflection;
using Microsoft.Extensions.Logging;
using System.Globalization;





namespace Shubak_Website.Controllers
{
    public class AccountController : Controller
    {
        private readonly FirebaseAuthService _auth;
        private readonly IUsersRepository _iusersRepository;
        private readonly ILogger<AccountController> _logger;


        public AccountController( FirebaseAuthService auth, IUsersRepository iusersRepository,ILogger<AccountController> logger)

        {
           _auth = auth;
            _iusersRepository = iusersRepository;
            _logger = logger;
        }


public IActionResult Register()
{

        // Set the culture to Arabic
        CultureInfo.CurrentCulture = new CultureInfo("ar");
        CultureInfo.CurrentUICulture = new CultureInfo("ar");
   return View();
}


[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Register(UserModel userModel)
{

        if(ModelState.IsValid){

            return View();
        }
    
         try
        {

            var firebaseToken = await _auth.SignUpWithEmailAndPasswordAsync(userModel.Email, userModel.Password);

         
           var UserID =  firebaseToken;

      
           await _iusersRepository.Register(userModel,UserID);
            // Store the token or do further actions
            return RedirectToAction("Index", "Home");
        }
        catch (Exception ex)
        {
            // Handle signup failure
            ModelState.AddModelError(string.Empty, "Failed to create an account. Please try again.");
            return View();
        }
}



   [HttpGet]

   public IActionResult Login(){


      return View();
   }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(UserModel loginModel)
    {

        


        if(ModelState.IsValid){

            return View(loginModel);
        }

         try
        {



            var firebaseToken = await _auth.SignInWithEmailAndPasswordAsync(loginModel.Email, loginModel.Password);

            var UserInformation = await _iusersRepository.GetUserInformationByUID(firebaseToken);
            ViewData["UserInformation"] = UserInformation;


        var claims = new List<Claim>
        {
             new Claim(ClaimTypes.Name, UserInformation.Firstname!),
                new Claim(ClaimTypes.Email, UserInformation.Email),
            // Add more claims as needed
        };

            // Add UserType claim
        if (UserInformation.UserType == "Company")
            {
                claims.Add(new Claim("UserType", "Company"));
        }

        var claimsIdentity = new ClaimsIdentity(claims, "MyCookie");

        var authProperties = new AuthenticationProperties
        {
            // Add expiration, etc. as needed
        };

        await HttpContext.SignInAsync("CookieAuthentication",
            new ClaimsPrincipal(claimsIdentity), authProperties);



            var token = firebaseToken;
            if (token != null){
               
                HttpContext.Session.SetString("_UserToken", token);

                return RedirectToAction("Index", "Home" );
            }else{


                return View();
            }

            // Store the token or do further actions
            
        }
        catch (Exception ex)
        {

            // Handle signup failure
            ModelState.AddModelError(string.Empty, "Failed to create an account. Please try again.");
            TempData["ErrorMessage"] = "البريد الإلكتروني او الرقم السري غير صحيح ";
            return View();
        }

        
    }




   [HttpPost]
   [HttpGet]
   public IActionResult Logout()
   {

      // Sign out the user
        HttpContext.Session.Remove("_UserToken");
        
        
        // Redirect to the home page or any other desired page
        return RedirectToAction("Index", "Home");

      
   }


public IActionResult Resetpassword(){


    return View();
}

public IActionResult ResetSeccessfuly(){


    return View();
}

   public  IActionResult ResetAccountpassword( string Email){

            var resetPass =  _auth.SendPasswordResetEmailAsync(Email);
     
     return RedirectToAction("ResetSeccessfuly");
   }


    public IActionResult AccessDenied(){


         return View();
    }

}





    
}