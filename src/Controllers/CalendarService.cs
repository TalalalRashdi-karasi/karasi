using Shubak_Website.Context;
using Shubak_Website;
using System;
using System.Net;
using System.Net.Mail;
using Ical.Net;
using Ical.Net.CalendarComponents;
using Ical.Net.DataTypes;
namespace Shubak_Website.Controllers;
public class CalendarService
{
    

       public void SendCalendarInvite(string toEmail, string subject, string body, DateTime eventStart, DateTime eventEnd, string location)
    {
        var calendar = new Calendar();
        var organizer = new Organizer("dr.usb@live.com");
        var organizerEvent = new CalendarEvent
        {
            Organizer = organizer,
            Start = new CalDateTime(eventStart),
            End = new CalDateTime(eventEnd),
            Location = location,
            Summary = subject,
            Description = body
        };

        calendar.Events.Add(organizerEvent);

        var icsContent = calendar.ToString();

        // Create a MailMessage
        var message = new MailMessage("dr.usb@live.com", toEmail)
        {
            Subject = subject,
            Body = body,
            IsBodyHtml = true
        };

        // Attach the calendar invite (.ics) file
        message.Attachments.Add(new System.Net.Mail.Attachment(new System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(icsContent)), "calendar_invite.ics", "text/calendar"));

        // Configure the SMTP client
        using (var client = new SmtpClient("smtp@live.com"))
        {
            client.Port = 587;
            client.Credentials = new NetworkCredential("dr.usb@live.com", "*Drusb@123*");
            client.EnableSsl = true;




            try
            {
            // Send the email
            client.Send(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                // Log or handle the exception accordingly
            }
        }
    }
    
    
}
