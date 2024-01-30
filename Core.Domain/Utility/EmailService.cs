using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;

namespace Core.Domain.Utilities
{
    public class EmailService
    {
        public static string SendEmail(String Email,String Message,String Subject)
        {
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            //create the mail message 
            MailMessage mail = new MailMessage();

            //set the addresses 
            mail.From = new MailAddress("heshestore786@gmail.com"); //IMPORTANT: This must be same as your smtp authentication address.
            mail.To.Add(Email);

            //set the content 
            mail.Subject = Subject;
            mail.Body = Message;
            //send the message 
            SmtpClient smtp = new SmtpClient("smtp.gmail.com");

            //IMPORANT:  Your smtp login email MUST be same as your FROM address. 
            NetworkCredential Credentials = new NetworkCredential("heshestore786@gmail.com", "LMS");
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = Credentials;
            smtp.Port = 587;    //alternative port number is 8889
            smtp.EnableSsl = false;
            smtp.Send(mail);
            return "Send Successfully";
        }
    }
}