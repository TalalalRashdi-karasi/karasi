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


    [Authorize(Policy = "Company")]
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
    public async Task<IActionResult> UpdateEvents([FromForm] EventFormModel addevent){

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
    public async Task<IActionResult> AddNewEvent([FromForm] EventFormModel addevent)
    {


        if (!ModelState.IsValid)
        {

            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json("Model is not valid");

        }
        else
        {
            await _eventsRepository.AddAsync(addevent.MapToDto());

            ModelState.Clear();
        }

        return RedirectToAction("Index");
    }



    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteEventById ( int eventId){



        var delete = await _eventsRepository.DeleteEventById(eventId);

        return RedirectToAction("MyEventList");
    }
}
