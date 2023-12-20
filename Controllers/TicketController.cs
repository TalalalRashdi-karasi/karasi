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
using System.Xml.Schema;


namespace Shubak_Website.Controllers;

public class TicketController : Controller
{

   
    private readonly EventsRepository _eventsRepository;
    private readonly TicketsRepository _TicketsRepository;

    private readonly ILogger<TicketsRepository> _logger;

    public TicketController(
        EventsRepository eventsRepository,
        TicketsRepository ticketsRepository,
        ILogger<TicketsRepository> logger)
    {
        _eventsRepository = eventsRepository;
        _TicketsRepository = ticketsRepository;
        _logger = logger;

    }



    public  async Task<IActionResult> Index()
    {



        return View();
    }




    public async Task<IActionResult> SetNewTicket(TicketModel ticketModel){


        var addTicket = _TicketsRepository.AddAsync(ticketModel);

        return RedirectToAction("BookingConfirmation","Home");
    }








}
