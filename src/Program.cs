
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Shubak_Website;
using Shubak_Website.Context;
using Shubak_Website.Controllers;
using Shubak_Website.Models;
using Shubak_Website.Repositories;
using Recombee.ApiClient;
using Recombee.ApiClient.ApiRequests;
using Recombee.ApiClient.Bindings;
using Recombee.ApiClient.Util;
using Firebase.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(config =>
        {
            config.Cookie.Name = "MyCookie";
            config.LoginPath = "/account/login";
            config.AccessDeniedPath = "/account/AccessDenied";
            config.ExpireTimeSpan = TimeSpan.FromMinutes(15.0);
            config.SlidingExpiration = true;
        });



builder.Services.AddMvc()
    .AddSessionStateTempDataProvider()
    .AddRazorPagesOptions(options =>
        {
            options.Conventions.AuthorizeFolder("/");
            options.Conventions.AllowAnonymousToPage("/Home/Index");
        });

builder.Services.AddSession();
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<ShubakContext>();
builder.Services.AddSingleton<TicketsRepository>();
builder.Services.AddSingleton<EventsRepository>();
builder.Services.AddSingleton<FirebaseAuthService>();
builder.Services.AddSingleton<CalendarService>();
builder.Services.AddSingleton<IUsersRepository , UsersRepository>();

builder.Services.AddRazorPages()
                .AddRazorRuntimeCompilation();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession();

builder.Services.AddHealthChecks();

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.None;
    options.Secure = CookieSecurePolicy.Always;
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy =>
        policy.RequireClaim("UserType", "Company"));
});

builder.Services.AddMvc().AddRazorPagesOptions(options =>
        {
            options.Conventions.AuthorizeFolder("/");
            options.Conventions.AllowAnonymousToPage("/Home/Index");
        
        
});


var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();
// app.UseHttpsRedirection();
app.MapDefaultControllerRoute();

// Use cookie middleware
app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.None,
    Secure = CookieSecurePolicy.Always
});

// Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); // Detailed errors in development
}
else
{
    app.UseExceptionHandler("/Home/Error"); // Generic error handler in production

    // HSTS (HTTP Strict Transport Security) to enforce HTTPS
    app.UseHsts();

    // Optionally, configure a custom status code handler
    app.UseStatusCodePagesWithReExecute("/Home/StatusCode", "?code={0}");
}

// add health check
app.UseHealthChecks("/health");

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

    endpoints.MapControllerRoute(
        name: "accessDenied",
        pattern: "/account/accessdenied",
        defaults: new { controller = "Account", action = "AccessDenied" }
    );

    endpoints.MapControllerRoute(
        name: "logout",
        pattern: "/account/logout",
        defaults: new { controller = "Account", action = "Logout" }
    );
    
    endpoints.MapRazorPages();
});

// app.Use(async (context, next) =>


//     var contentSecurityPolicy = new StringBuilder();
//     var googleJs = "https://www.googletagmanager.com https://www.google-analytics.com https://www.google.com/ https://www.gstatic.com/";

//     contentSecurityPolicy.Append("default-src https: 'self' 'unsafe-inline';");
//     contentSecurityPolicy.Append($"script-src 'self' 'unsafe-inline' {googleJs};");
//     contentSecurityPolicy.Append("img-src 'self' data: https: https://www.google-analytics.com;");
//     contentSecurityPolicy.Append("style-src 'self' 'unsafe-inline' ;");
//     contentSecurityPolicy.Append("media-src 'self' https: data: ;");
//     context.Response.Headers.Add("Content-Security-Policy", contentSecurityPolicy.ToString());
//     context.Response.Headers.Add("X-Xss-Protection", "1; mode=block");
//     context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
//     context.Response.Headers.Add("Referrer-Policy", "no-referrer");
//     context.Response.Headers.Add("X-Frame-Options", "DENY");

//     context.Response.Headers.Add("Feature-Policy", "geolocation 'none';midi 'none';sync-xhr 'none';microphone 'none';camera 'none';magnetometer 'none';gyroscope 'none';fullscreen 'self';payment 'none';");

//     await next.Invoke();
// });
app.Run();
