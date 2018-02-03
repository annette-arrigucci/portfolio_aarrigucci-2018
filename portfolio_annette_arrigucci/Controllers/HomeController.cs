using Microsoft.AspNet.Identity;
using portfolio_annette_arrigucci.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace portfolio_annette_arrigucci.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {

            return View();
        }

        public ActionResult Portfolio()
        {

            return View();
        }

        public ActionResult Contact()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Contact([Bind(Include = "Id,FirstName,LastName,Email,Message,Phone")] Contact contact)
        {
            contact.Created = DateTime.Now;
            var newContact = contact.FirstName + " " + contact.LastName;

            var svc = new EmailService();
            var msg = new IdentityMessage();
            msg.Subject = "Contact From Portfolio Site";
            msg.Body = "\r\n You have received a request to contact from " + newContact + " (" + contact.Email + ")" + "\r\n"
                         + "\r\n with the following message: \r\n\t"
                         + contact.Message;
            msg.Destination = /*contact.Email*/"annette.arrigucci@gmail.com";


            await svc.SendAsync(msg);

            return View(contact);
        }

        public ActionResult Exercises()
        {
            return View();
        }

        public ActionResult Resume()
        {
            return View();
        }
    }
}