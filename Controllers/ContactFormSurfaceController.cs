using AarhusWebDevCoop.ViewModels;
using System.Net.Mail;
using System.Web.Mvc;
using Umbraco.Web.Mvc;
using Umbraco.Core.Models;
using Umbraco.Core.Services;
using Umbraco.Core;


namespace AarhusWebDevCoop.Controllers
{
    public class ContactFormSurfaceController : SurfaceController
    {
        // GET: ContactFormSurface
        public ActionResult Index()
        {
            return PartialView("ContactForm", new ContactForm());
        }

        [HttpPost]
        public ActionResult HandleFormSubmit(ContactForm model)
        {
            MailMessage message = new MailMessage();
            message.To.Add("TheEmailAdressYouWantToSendTheMailTo");
            message.Subject = model.Subject;
            message.From = new MailAddress("YourSendinblueRegisteredEmail", "Aarhus Web Developer Corporation");
            message.Body = "From " + model.Name + ", " + model.Email + ": " + model.Message;
            using (SmtpClient smtp = new SmtpClient())
            {
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = false;
                smtp.EnableSsl = true;
                smtp.Host = "smtp-relay.sendinblue.com";
                smtp.Port = 587;
                smtp.Credentials = new System.Net.NetworkCredential("joshuabacuriomercado@gmail.com", "HGPDJ7FVCUwrNyZA");
                
 // send mail
                 smtp.Send(message);
            }
            return RedirectToCurrentUmbracoPage();
        }

    }
}