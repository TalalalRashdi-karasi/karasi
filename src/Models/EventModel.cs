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

         [Required(ErrorMessage = "لم تقم بإدخال اسم الفعالية ")]
        public String? EventName {get; set;}

         [Required(ErrorMessage = "لم تقم بإدخال نبذه عن الفعالية ")]
        public String? AboutEvent {get; set;}
         [Required(ErrorMessage = "لم تقم بإدخال  عدد المقاعد ")]
        public int? SeatCount {get; set;}
         [Required(ErrorMessage = "لم تقم بإدخال  الفئة المستهدفه ")]
        public String? Target {get; set;}

         [Required(ErrorMessage = "لم تقم بإدخال  مكان الفعالية ")]
        public String? EventPlace {get; set;}

         [Required(ErrorMessage = "لم تقم بإدخال تكلفة الفعالية - إذا كانت مجاناً ضع ٠  ")]
        public double? EventPrice {get; set;}

         [Required(ErrorMessage = "لم تقم بإدخال تاريخ الفعالية ")]
        public DateTime? EventDate {get; set;}

         [Required(ErrorMessage = "لم تقم بإدخال وقت الفعالية ")]
        public DateTime? EventTime {get; set;}

        public int? IsActive {get; set;}

        [Required(ErrorMessage = "لم تقم بإدخال اعلان الفعالية ")]
        public  IFormFile?  EvImage {get; set;}
        
         [Required(ErrorMessage = "لم تقم بإدخال نوع الفعالية ")]
        public String? EventType {get; set;}
        public string? ImagePath { get; set; } 

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
            // EvImage = EvImage?.ConvertToByteArray(),
           // EvImage = EvImage,
            EventType = EventType,
            ImagePath = ImagePath
            
        };
        
    }
    
    public class EventDto
    {

        public int? Id {get; set;}
         public String? UserId {get; set;}

         [Required(ErrorMessage = "لم تقم بإدخال اسم الفعالية ")]
        public String? EventName {get; set;}
         [Required(ErrorMessage = "لم تقم بإدخال نبذه عن الفعالية ")]
        public String? AboutEvent {get; set;}
          [Required(ErrorMessage = "لم تقم بإدخال  عدد المقاعد ")]
        public int? SeatCount {get; set;}
           [Required(ErrorMessage = "لم تقم بإدخال  الفئة المستهدفه ")]
        public String? Target {get; set;}
         [Required(ErrorMessage = "لم تقم بإدخال  مكان الفعالية ")]
        public String? EventPlace {get; set;}

         [Required(ErrorMessage = "لم تقم بإدخال تكلفة الفعالية - إذا كانت مجاناً ضع ٠  ")]
        public double? EventPrice {get; set;}

         [Required(ErrorMessage = "لم تقم بإدخال تاريخ الفعالية ")]
        public DateTime? EventDate {get; set;}

         [Required(ErrorMessage = "لم تقم بإدخال وقت الفعالية ")]
        public DateTime? EventTime {get; set;}
        public int? IsActive {get; set;}

         [Required(ErrorMessage = "لم تقم بإدخال اعلان الفعالية ")]
        // public  byte[]?  EvImage {get; set;}
        public  IFormFile?  EvImage {get; set;}

         [Required(ErrorMessage = "لم تقم بإدخال نوع الفعالية ")]
        public String? EventType {get; set;}
        public string? ImagePath { get; set; } 

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
            // EvImage = EvImage == null ? "" : $"data:image/png;base64,{Convert.ToBase64String(EvImage)}",
           // EvImage = EvImage,
            EventType = EventType,
            ImagePath = ImagePath
        };
    }

    public class EventViewModel
    {

        public int? Id {get; set;}
         public String? UserId {get; set;}

        [Required(ErrorMessage = "لم تقم بإدخال اسم الفعالية ")]

        public String? EventName {get; set;}

         [Required(ErrorMessage = "لم تقم بإدخال  وصف الفعالية ")]
        public String? AboutEvent {get; set;}

         [Required(ErrorMessage = "لم تقم بإدخال  عدد المقاعد ")]
        public int? SeatCount {get; set;}

          [Required(ErrorMessage = "لم تقم بإدخال  الفئة المستهدفه ")]
        public String? Target {get; set;}

         [Required(ErrorMessage = "لم تقم بإدخال  مكان الفعالية ")]
        public String? EventPlace {get; set;}

          [Required(ErrorMessage = "لم تقم بإدخال تكلفة الفعالية - إذا كانت مجاناً ضع ٠  ")]
        public double? EventPrice {get; set;}

         [Required(ErrorMessage = "لم تقم بإدخال تاريخ الفعالية ")]
        public DateTime? EventDate {get; set;}

          [Required(ErrorMessage = "لم تقم بإدخال وقت الفعالية ")]
        public DateTime? EventTime {get; set;}
        public int? IsActive {get; set;}

            [Required(ErrorMessage = "لم تقم بإدخال اعلان الفعالية ")]
        public IFormFile?  EvImage {get; set;}

         [Required(ErrorMessage = "لم تقم بإدخال نوع الفعالية ")]
        public String? EventType {get; set;}

        public string? ImagePath { get; set; } 
        
    }
}