using System.Data.SqlTypes;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shubak_Website.Models;
using Shubak_Website.Repositories;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Authorization;
using IdentityServer4.Validation;
using ZXing.QrCode;
using Shubak_Website.Context;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Webp;
using SixLabors.ImageSharp.Processing;


namespace Shubak_Website.Controllers;

public class AdminController : Controller 
{


    private readonly EventsRepository _eventsRepository;
    private readonly TicketsRepository _ticketsRepository;

    private readonly ILogger<AdminController> _logger;

    public AdminController(
       
        TicketsRepository ticketsRepository,
        EventsRepository eventsRepository,
        ILogger<AdminController> logger)
    {
       
        _eventsRepository = eventsRepository;
        _ticketsRepository = ticketsRepository;
        _logger = logger;

    }


   [Authorize(Policy = "AdminPolicy")]
    public  async Task<IActionResult> Index()
    {



        return View();
    }

    public async Task<IActionResult> GetEventById(int EvID){

        
        var EventByID = await _eventsRepository.GetByIdAsync(EvID);

        ViewData["eventID"] = EvID;

        return PartialView("Partial/_EventInformation" , EventByID);

    }

    [HttpPost]
    public async Task<IActionResult> UpdateEvents(  EventFormModel addevent){

       var newvalue = await _eventsRepository.UpdateEventAsync(addevent.MapToDto());

       return RedirectToAction("MyEventList");
    }

    public async Task<IActionResult> RememberList(int EventID){

        var UserID =  HttpContext.Session.GetString("_UserToken");

         var UserEvent = await _eventsRepository.GetAllEventByUserID(UserID);
    
        var UserTicket =  await  _ticketsRepository.GetAllTicket();


        return View(Tuple.Create(UserEvent , UserTicket));
    }

    public async Task<IActionResult> GetUserticketByEventId(int EventID){


        var UserTicket =  await  _ticketsRepository.GetUserticketByEventId(EventID);

        return View(UserTicket);
    }

    public  async Task<IActionResult> MyEventList( string UID){
        

        var UserID =  HttpContext.Session.GetString("_UserToken");
       
        var MyevetList = await _eventsRepository.GetAllEventByUserID(UserID);

        return View(MyevetList);
    }

    public async Task<IActionResult> AddEvent(){
        
            var UserToken =  HttpContext.Session.GetString("_UserToken");
             ViewData["UserTokenForAdminPage"]= UserToken;
        
        return View();
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddEvent(EventFormModel addevent)
{
    if (!ModelState.IsValid)
    {
        Response.StatusCode = (int)HttpStatusCode.BadRequest;
        return View(addevent);
    }

    try{

    // Handle image upload
    if (addevent.EvImage != null && addevent.EvImage.Length > 0)
    {
        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
        var extension = Path.GetExtension(addevent.EvImage.FileName).ToLowerInvariant();

        if (!allowedExtensions.Contains(extension))
        {
            ModelState.AddModelError("EvImage", "Invalid file type");
            return View(addevent);
        }

        var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "var/www/html/img/");

        if (!Directory.Exists(uploadPath))
            Directory.CreateDirectory(uploadPath);

        var webpFileName = GetNextFileName(uploadPath, ".webp");
        var filePath = Path.Combine(uploadPath, webpFileName);

        using (var image = await Image.LoadAsync(addevent.EvImage.OpenReadStream()))
        {
            var encoder = new WebpEncoder();
            await image.SaveAsync(filePath, encoder);
        }

        addevent.ImagePath = "/img/" + Path.GetFileName(filePath);  // Save the relative path in the model
    }

    // Add event to the database
    await _eventsRepository.AddAsync(addevent.MapToDto());

    ViewData["Done"] = "تم حفظ الفعالية";
    ModelState.Clear();
    }catch(Exception ex){

    // Log the exception (optional)
    _logger.LogError(ex, "Error occurred while saving the event.");

    // Handle the error gracefully
    ModelState.AddModelError(string.Empty, "An error occurred while processing your request. Please try again later.");

    // Return the view with the error message
    return View(addevent);

    }

    return View();
}


     private string GetNextFileName(string uploadPath, string extension)
    {
        var files = Directory.GetFiles(uploadPath, $"*{extension}")
                             .Select(f => Path.GetFileNameWithoutExtension(f))
                             .Where(f => int.TryParse(f, out _))
                             .Select(f => int.Parse(f))
                             .OrderBy(f => f)
                             .ToList();

        var nextNumber = (files.Count > 0) ? files.Last() + 1 : 1;
        return nextNumber.ToString("D3") + extension;
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteEventById ( int eventId){



        var delete = await _eventsRepository.DeleteEventById(eventId);

        return RedirectToAction("MyEventList");
    }



}
