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

            if (!ModelState.IsValid) { return CurrentUmbracoPage(); }


            MailMessage message = new MailMessage();
            message.To.Add("joshuabacuriomercado@gmail.com");
            message.Subject = model.Subject;
            message.From = new MailAddress("joshuabacuriomercado@gmail.com", "Aarhus Web Developer Corporation");
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


            TempData["success"] = true;


            // Get the GuidUdi of the current page
            GuidUdi currentPageUdi = new GuidUdi(CurrentPage.ContentType.ItemType.ToString(), CurrentPage.Key);

            // Create the new content type
            IContent msg = Services.ContentService.CreateContent(model.Subject, currentPageUdi, "message");
            msg.SetValue("messageName", model.Name);
            msg.SetValue("email", model.Email);
            msg.SetValue("subject", model.Subject);
            msg.SetValue("messageContent", model.Message);
            msg.SetValue("umbracoNaviHide", true);

            // Save
            Services.ContentService.Save(msg);

            return RedirectToCurrentUmbracoPage();


        }




    }
}