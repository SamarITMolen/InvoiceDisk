using InvoiceDisk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace InvoiceDisk.Controllers
{
    public class EmailController : Controller
    {
        // GET: Email
        public ActionResult Index()
        {
            return View();
        }

        public static bool email(string p)
        {
            bool emailstatus = false;

            try
            {

                string  bodyhtml =@"<div class='container'>
                        <div>
                       <h3>Dear Mr.samar gul,</h3>
                       <p>
                           Here you receive our offer 2 as discussed.
                           We would like to hear if you agree with this.

                           You will find the offer as an attachment to this email.
                       </p>
                       <p>
                           With kind regards,
                       </p>
                       <p>
                           samar gul
                           It Molen
                       </p>
                       <p>
                           345465
                           fuygk @yahoo.com
                       </p>
                   </div>
               </div>";


                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                mail.From = new MailAddress("samarbudhni@gmail.com");
                mail.To.Add("sabar2143@gmail.com");
                mail.Subject = "It Molen Offer Letter";//emailModel.Subject;
                mail.Body = bodyhtml;
                mail.IsBodyHtml = true;
                //making attachment
                System.Net.Mail.Attachment attachment;
                attachment = new System.Net.Mail.Attachment(p);
                mail.Attachments.Add(attachment);
                //Gmail Port
                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("samarbudhni@gmail.com", "samar1234567");
                //You can specifiy below line of code either in web.config file or as below.
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(mail);
                emailstatus = true;
            }
            catch (Exception)
            {

                emailstatus = false;

                throw;
            }

            return emailstatus;
        }

    }
}