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

namespace Shubak_Website
{
    public class IdentityServerConfiguration 
    {
        

        // public static List<TestUser> GetUsers()
        // {
        //     var john = new TestUser()
        //     {
        //         SubjectId = "1",
        //         Username = "john",
        //         Password = "1111",
        //         Claims =
        //     {
        //         new Claim(type: "name"     /*JwtClaimTypes.Name*/,    value: "John Doe"),
        //         new Claim(type: "role"     /*JwtClaimTypes.Role*/,    value: "admin"),
        //         new Claim(type: "website"  /*JwtClaimTypes.WebSite*/, value: "https://medium.com/@iamprovidence"),
        //     }
        //     };

        //     return new List<TestUser> { john };
        // }

        // public static List<IdentityResource> GetIdentityResources()
        // {
        //     return new List<IdentityResource>
        // {
        //     new IdentityResources.OpenId(), // new IdentityResource(name: "openId", userClaims new [] { "sub" })
        //     new IdentityResources.Profile(), // new IdentityResource(IdentityServerConstants.StandardScopes.Profile, new [] { "name", "email" ... } )
        // };
        // }



        // public static List<Client> GetClients()
        // {
        //     var mvc = new Client
        //     {
        //         ClientId = "mvc", // something meaningful
        //         ClientSecrets = { new Secret("secret".Sha256()) }, // from configuration

        //         // where to redirect to after login
        //         RedirectUris =
        //     {
        //         "https://localhost:5444/signin-oidc", // mvc app 1
        //         "https://localhost:7066/signin-oidc", // mvc app 2
        //     },
        //         // where to redirect to after logout
        //         PostLogoutRedirectUris =
        //     {
        //         "https://localhost:5444/signout-callback-oidc", // mvc app 1
        //         "https://localhost:7066/signout-callback-oidc", // mvc app 2
        //     },

        //         AllowedScopes =
        //     {
        //         "openid",  // OidcConstants.StandardScopes.OpenId
        //         "profile", // OidcConstants.StandardScopes.Profile
        //     },

        //         AllowedGrantTypes = new[] { "authorization_code" },
        //         // AllowedGrantTypes = new[] { GrantType.AuthorizationCode } 
        //         // AllowedGrantTypes = GrantTypes.Code,

        //     };

        //     return new List<Client> { mvc };
        // }

    }
}