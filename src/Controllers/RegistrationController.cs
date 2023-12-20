using Microsoft.AspNetCore.Mvc;
using Shubak_Website.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;



namespace Shubak_Website.Controllers
{
    public class RegistrationController : Controller
    {
    
        private readonly ILogger<RegistrationController> _logger;

        public RegistrationController(
        
            ILogger<RegistrationController> logger)
        {
          
            _logger = logger;
        }


        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(UserModel userModel)
        {

                return View();
         
        }


    }
}