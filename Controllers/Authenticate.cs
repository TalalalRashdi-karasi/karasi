using System.ComponentModel.DataAnnotations;
using IdentityServer4.Models;
using Shubak_Website.Context;
using Shubak_Website.Controllers;
using Shubak_Website.Repositories;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Shubak_Website.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using IdentityServer4.Test;
using System.Security.Claims;
using IdentityServer4;


// namespace Shubak_Website.Controllers
// {

//     private async Task Authenticate(TestUser user)
//     {
//         var identityUser = new IdentityServerUser(user.SubjectId);

//         await HttpContext.SignInAsync(identityUser);
//     }
// }