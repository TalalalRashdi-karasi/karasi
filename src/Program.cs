
using Microsoft.AspNetCore.Authentication.Cookies;

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





var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddMvc().AddSessionStateTempDataProvider();
    builder.Services.AddSession();

builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<ShubakContext>();
 
builder.Services.AddSingleton<TicketsRepository>();
builder.Services.AddSingleton<EventsRepository>();
builder.Services.AddSingleton<FirebaseAuthService>();
builder.Services.AddSingleton<IUsersRepository , UsersRepository>();
builder.Services.AddRazorPages()
                .AddRazorRuntimeCompilation();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession();
builder.Services.AddAuthorization();
builder.Services.AddMvc().AddRazorPagesOptions(options =>
        {
            options.Conventions.AuthorizeFolder("/");
            options.Conventions.AllowAnonymousToPage("/Home/Index");
        });
builder.Services.AddAuthorization(options =>
{
    // Define your policies here
    options.AddPolicy("RequireAdmin", policy => policy.RequireClaim("userType", "true"));
});
builder.Services.Configure<CookiePolicyOptions>(options =>
        {
            options.CheckConsentNeeded = context => true;
            options.MinimumSameSitePolicy = SameSiteMode.None;
            options.Secure = CookieSecurePolicy.Always;
        });




var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    // app.UseHsts();
}

// Use cookie middleware
app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.None,
    Secure = CookieSecurePolicy.Always
});

app.UseSession();
// app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

app.MapDefaultControllerRoute();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();