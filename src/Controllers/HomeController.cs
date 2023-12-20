using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shubak_Website.Context;
using Shubak_Website.Controllers;
using Shubak_Website.Models;
using Shubak_Website.Repositories;
using Firebase.Auth;
using System;
using IdentityServer4.Extensions;
using Microsoft.Data.SqlClient;
using System.Xml.Schema;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;


namespace Shubak_Website.Controllers;

public class HomeController : Controller
{

     private readonly FirebaseAuthService _auth;
     
     private readonly IUsersRepository _iusersRepository;
    private readonly EventsRepository _eventsRepository;
    private readonly TicketsRepository _TicketsRepository;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger,IUsersRepository iusersRepository, FirebaseAuthService auth, EventsRepository eventsRepository, TicketsRepository TicketsRepository )
    {   
        _auth = auth;
        _iusersRepository = iusersRepository;
        _TicketsRepository = TicketsRepository;
        _eventsRepository = eventsRepository;
        _logger = logger;
    }

    
    [HttpGet]
    public async Task<IActionResult> Index()
    {

        var toUSer =  HttpContext.Session.GetString("_UserToken");
        ViewData["UserToken"]= toUSer;
        if(toUSer != null){

          var mainUSer =  await _iusersRepository.GetUserInformationByUID(toUSer);
          var firstName =  mainUSer.Firstname;
          var id = mainUSer.UID;
          ViewData["ShowFirstName"] = firstName;
          
        }
           


        var Allevents = await _eventsRepository.GetAllEventsAsync();
        // var userCardTicket = await _TicketsRepository.GetByIdAsync(id);
        var result = Allevents
        .OrderByDescending(c => c.EventTime)
        .AsEnumerable();

    


        // var result2 = userCardTicket
        // .OrderByDescending(c => c.EventDate)
        // .AsEnumerable();

        // ViewData["userCardTicket"] = result2;

        return View(result);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [Route("/{controller}/{action}/{id}")]
    [HttpGet]
    public async Task<IActionResult> EventDetails(int id)
    {
        var RemainingSeats = await _TicketsRepository.GetRemainingSeats(id);
         var totalSeats =    RemainingSeats.First();
         ViewData["RemainingSeats"] = totalSeats.RemainingSeats;

         string? data = TempData["LargeNoOfSeat"] as string;
        var toUSer =  HttpContext.Session.GetString("_UserToken");
        ViewData["UserToken"]= toUSer  ?? "";

         var mainUSer =  await _iusersRepository.GetUserInformationByUID(toUSer);
         
          var firstName =  mainUSer?.Firstname  ?? null ;
          ViewData["ShowFirstName"] = firstName  ?? null ;

        var eventByID = await _eventsRepository.GetByIdAsync(id);
        return View(eventByID.FirstOrDefault());
    }


    [HttpPost]
    public async Task<IActionResult> SubmitForm(TicketModel ticketModel)
    {

        var EventID = ticketModel.EventId;
        
         var RemainingSeats = await _TicketsRepository.GetRemainingSeats(EventID);
         var  totalSeats =    RemainingSeats.First();

         var  NoOfRemainingSeats =  totalSeats.RemainingSeats;
         var  NoOfTicket = ticketModel.TicketCount;


        var toUSer =  HttpContext.Session.GetString("_UserToken");
        ViewData["UserToken"]= toUSer  ?? "";

         var mainUSer =  await _iusersRepository.GetUserInformationByUID(toUSer);
          var firstName =  mainUSer?.Firstname  ?? "" ;
          ViewData["ShowFirstName"] = firstName  ?? "";

                  
        if(  NoOfTicket >  NoOfRemainingSeats ){

            
             TempData["LargeNoOfSeat"] = "عدد  التذاكر المطلوبة اكثر من التبقية";

            return RedirectToAction("EventDetails", new { id = EventID }  );
         }

        // var ticket = await _TicketsRepository.AddAsync(ticketModel);
        return RedirectToAction("TiketDetails", ticketModel);
    }




    [HttpGet]
    [HttpPost]
    public async  Task<IActionResult>  TiketDetails(TicketModel ticketModel)
    {
        var EventID = ticketModel.EventId;
        
         var RemainingSeats = await _TicketsRepository.GetRemainingSeats(EventID);
         var  totalSeats =    RemainingSeats.First();

         var  NoOfRemainingSeats =  totalSeats.RemainingSeats;
         var  NoOfTicket = ticketModel.TicketCount;



         var toUSer =  HttpContext.Session.GetString("_UserToken");
        ViewData["UserToken"]= toUSer ?? "";
        if(toUSer != null){

          var mainUSer =  await _iusersRepository.GetUserInformationByUID(toUSer);
          var Email = mainUSer.Email;
          ViewData["UserEmil"] = Email;
          var firstName =  mainUSer.Firstname  ?? "" ;
          var id = mainUSer.UID;
          ViewData["ShowFirstName"] =  firstName ?? "" ;
          
        }


        return View(ticketModel);
    }


        public async Task<IActionResult> BookingConfirmation(){



        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
