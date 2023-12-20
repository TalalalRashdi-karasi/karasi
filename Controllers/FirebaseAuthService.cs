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




namespace Shubak_Website.Controllers
{
    public class FirebaseAuthService : Controller
    {

        FirebaseAuthProvider _auth;

        public FirebaseAuthService(){
        _auth = new FirebaseAuthProvider(
        new FirebaseConfig("AIzaSyBMbGxoifkFiQ8QCvibE5Otw25ddu3lsB4"));  

        }


    public async Task<string> SignInWithEmailAndPasswordAsync(string email, string password)
    {
        var result = await _auth.SignInWithEmailAndPasswordAsync(email,password);



        return result.User.LocalId;
    }

        public async Task<string> SignUpWithEmailAndPasswordAsync(string email, string password)
    {
        var result = await _auth.CreateUserWithEmailAndPasswordAsync(email, password);
        return result.User.LocalId;
    }

    public async Task LogoutAsync() {

        
        
    }



    }
}