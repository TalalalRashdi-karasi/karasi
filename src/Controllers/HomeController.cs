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
using Microsoft.AspNetCore.Identity;


namespace Shubak_Website.Controllers;

public class HomeController : Controller
{

    private readonly FirebaseAuthService _auth;
     
    private readonly IUsersRepository _iusersRepository;
    private readonly EventsRepository _eventsRepository;
    private readonly TicketsRepository _TicketsRepository;

    private readonly CalendarService _CalendarService;
    private readonly ILogger<HomeController> _logger;

    

    public HomeController( ILogger<HomeController> logger, CalendarService calendarService ,IUsersRepository iusersRepository, FirebaseAuthService auth, EventsRepository eventsRepository, TicketsRepository TicketsRepository )
    {   

        
       
        _auth = auth;
        _CalendarService = calendarService;
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

         string? data = TempData["LargeNoOfSeat"] as string;
        var toUSer =  HttpContext.Session.GetString("_UserToken");
        ViewData["UserToken"]= toUSer  ?? "";

         var mainUSer =  await _iusersRepository.GetUserInformationByUID(toUSer);
        
          var firstName =  mainUSer?.Firstname  ?? null ;
          ViewData["ShowFirstName"] = firstName  ?? null ;

        var eventByID = await _eventsRepository.GetByIdAsync(id);

        
        var RemainingSeats = await _TicketsRepository.GetRemainingSeats(id);

 
         var totalSeats =    RemainingSeats.First();

  

         ViewData["RemainingSeats"] = totalSeats.RemainingSeats;


        return View(eventByID.FirstOrDefault());
    }


    [HttpPost]
    public async Task<IActionResult> SubmitForm(TicketModel ticketModel)
    {

        var EventID = ticketModel.EventId;
        
         var RemainingSeats = await _TicketsRepository.GetRemainingSeats(EventID);
         var  totalSeats = RemainingSeats.First();

         var  NoOfRemainingSeats =  totalSeats.RemainingSeats;
         var  NoOfTicket = ticketModel.TicketCount;



        var toUSer =  HttpContext.Session.GetString("_UserToken");
        ViewData["UserToken"]= toUSer  ?? "";

         var mainUSer =  await _iusersRepository.GetUserInformationByUID(toUSer);
          var firstName =  mainUSer?.Firstname  ?? "" ;
          var UMail = mainUSer?.Email ?? "";
          ViewData["ShowFirstName"] = firstName  ?? "";
        ViewData["ShowUserEmail"] = UMail ?? "";
                  
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

        var NewPrice = NoOfTicket * ticketModel.TicketPrice;

             ViewData["NewPrice"] = NewPrice;
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

    //    var DateTest = "2022-01-18 15:45:00" ; 

    //    _CalendarService.SendCalendarInvite("dr.usb3@gmail.com", "منصة كراسي", "شكراً لك", DateTime.ParseExact(DateTest ,"yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture) ,DateTime.ParseExact(DateTest ,"yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture) , "Oman");


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


    public async  Task<IActionResult>  MoreEvent( string id){


       var EventByType = await  _eventsRepository.GetEventsByEventType(id);


       
        return View(EventByType);

    }


    public async Task<IActionResult> MyTicket(){

        
                var toUSer =  HttpContext.Session.GetString("_UserToken");
        ViewData["UserToken"]= toUSer;
        if(toUSer != null){

          var mainUSer =  await _iusersRepository.GetUserInformationByUID(toUSer);
          var firstName =  mainUSer.Firstname;
          var id = mainUSer.UID;
          
          ViewData["ShowFirstName"] = firstName;

          var GetTicketByUserID = await _TicketsRepository.GetTicketByUserID(id);
          

          return View(GetTicketByUserID);
        }else{

            return View();
        }

    }
}

