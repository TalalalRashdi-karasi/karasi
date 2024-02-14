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
using Microsoft.AspNetCore.Identity;





namespace Shubak_Website.Controllers
{
    public class AccountController : Controller
    {
        private readonly FirebaseAuthService _auth;
        private readonly IUsersRepository _iusersRepository;
        private readonly ILogger<AccountController> _logger;
         //private readonly SignInManager<UserModel> _signInManager;

        public AccountController( 
            FirebaseAuthService auth, 
            IUsersRepository iusersRepository,ILogger<AccountController> logger)
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
   public IActionResult Login()
   {
        /*if (this.User.Identity.IsAuthenticated)
        {*/
            // Home Page.
            return View();
        /*}

        return RedirectToAction("Login", "Account");*/
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
            new (ClaimTypes.Name, UserInformation.Firstname!),
            new (ClaimTypes.Email, UserInformation.Email),
            new (ClaimTypes.GroupSid , UserInformation.UserType),
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
            //AllowRefresh = <bool>,
// Refreshing the authentication session should be allowed.

IssuedUtc = DateTimeOffset.UtcNow,
// The time at which the authentication ticket was issued.

ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(15),
// The time at which the authentication ticket expires. A 
// value set here overrides the ExpireTimeSpan option of 
// CookieAuthenticationOptions set with AddCookie.

IsPersistent = true,
// Whether the authentication session is persisted across 
// multiple requests. When used with cookies, controls
// whether the cookie's lifetime is absolute (matching the
// lifetime of the authentication ticket) or session-based.

//RedirectUri = <string>
// The full path or absolute URI to be used as an http 
// redirect response value.
        };

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity), 
            authProperties);



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
    public async Task<IActionResult> Logout()
    {
        // Clear the existing external cookie
        await HttpContext.SignOutAsync(
            CookieAuthenticationDefaults.AuthenticationScheme);

        return RedirectToAction("Index", "Home");
    }


//    [HttpPost]
//     [ValidateAntiForgeryToken]
//    public async  Task<IActionResult> LogoutConfirmed()
//    {

//       // Sign out the user
//     // Perform sign out
//         await _signInManager.SignOutAsync();
//         // Redirect to the home page or any other desired page
//         return RedirectToAction("Index", "Home");

      
//    }


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