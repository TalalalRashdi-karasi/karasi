using System.ComponentModel.DataAnnotations;
using System.Data;  
using System.Data.SqlClient;  
using System.Configuration;  
using Dapper; 
using System.Linq;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Shubak_Website.Models
{
    public class EventFormModel
    {

        public int? Id {get; set;}
         public String? UserId {get; set;}
        public String? EventName {get; set;}
        public String? AboutEvent {get; set;}
        public int? SeatCount {get; set;}
        public String? Target {get; set;}
        public String? EventPlace {get; set;}
        public double? EventPrice {get; set;}
        public DateTime? EventDate {get; set;}
        public DateTime? EventTime {get; set;}

        public int? IsActive {get; set;}

        public  IFormFile?  EvImage {get; set;}
        
        public String? EventType {get; set;}

        public EventDto MapToDto() => new(){
            Id=Id,
            UserId = UserId,
            EventName = EventName,
            AboutEvent= AboutEvent,
            SeatCount= SeatCount,
            Target = Target,
            EventPlace = EventPlace,
            EventPrice = EventPrice,
            EventDate = EventDate,
            EventTime = EventTime,
            IsActive = IsActive,
            EvImage = EvImage?.ConvertToByteArray(),
            EventType = EventType
        };
        
    }
    public class EventDto
    {

        public int? Id {get; set;}
         public String? UserId {get; set;}
        public String? EventName {get; set;}
        public String? AboutEvent {get; set;}
        public int? SeatCount {get; set;}
        public String? Target {get; set;}
        public String? EventPlace {get; set;}
        public double? EventPrice {get; set;}
        public DateTime? EventDate {get; set;}
        public DateTime? EventTime {get; set;}

        public int? IsActive {get; set;}

        public  byte[]?  EvImage {get; set;}
        
        public String? EventType {get; set;}

        internal EventViewModel MapToViewModel() => new(){
            Id=Id,
            UserId = UserId,
            EventName = EventName,
            AboutEvent= AboutEvent,
            SeatCount= SeatCount,
            Target = Target,
            EventPlace = EventPlace,
            EventPrice = EventPrice,
            EventDate = EventDate,
            EventTime = EventTime,
            IsActive = IsActive,
            EvImage = EvImage == null ? "" : $"data:image/png;base64,{Convert.ToBase64String(EvImage)}",
            EventType = EventType
        };
    }

    public class EventViewModel
    {

        public int? Id {get; set;}
         public String? UserId {get; set;}
        public String? EventName {get; set;}
        public String? AboutEvent {get; set;}
        public int? SeatCount {get; set;}
        public String? Target {get; set;}
        public String? EventPlace {get; set;}
        public double? EventPrice {get; set;}
        public DateTime? EventDate {get; set;}
        public DateTime? EventTime {get; set;}

        public int? IsActive {get; set;}

        public  string?  EvImage {get; set;}
        
        public String? EventType {get; set;}
        
    }
}