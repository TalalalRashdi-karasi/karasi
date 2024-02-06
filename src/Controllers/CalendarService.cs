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



    //             // Sender's email address and password
    //     string senderEmail = "dr.usb3@gmail.com";
    //     string senderPassword = "xuyw cgay gllz xyrt";

    //     // Recipient's email address
    //     string recipientEmail = toEmail;

    //     // Create a new MailMessage
    //     MailMessage mailMessage = new MailMessage
    //     {
    //         From = new MailAddress(senderEmail),
    //         Subject = "Hello, this is a test email!",
    //         Body = "This is the content of the email."
    //     };

    //     // Add recipient's email address
    //     mailMessage.To.Add(recipientEmail);

    //     // Create a SmtpClient
    //     SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
    //     {
    //         Port = 587,
    //         Credentials = new NetworkCredential(senderEmail, senderPassword),
    //         EnableSsl = true,
    //     };

    //     try
    //     {
    //         // Send the email
    //         smtpClient.Send(mailMessage);
    //         Console.WriteLine("Email sent successfully!");
    //     }
    //     catch (Exception ex)
    //     {
    //         Console.WriteLine($"Failed to send email. Error: {ex.Message}");
    //     }
    // }

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
        var message = new MailMessage("dr.usb3@gmail.com", toEmail)
        {
            Subject = subject,
            Body = body,
            IsBodyHtml = true
        };

        // Attach the calendar invite (.ics) file
        message.Attachments.Add(new System.Net.Mail.Attachment(new System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(icsContent)), "calendar_invite.ics", "text/calendar"));

        // Configure the SMTP client
        using (var client = new SmtpClient("smtp.gmail.com"))
        {
            client.Port = 587;
            client.Credentials = new NetworkCredential("info@karasi.om", "mxpp zopz aldh jzhy");
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
