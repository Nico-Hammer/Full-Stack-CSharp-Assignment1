using Assignment1.Models;
using Microsoft.AspNetCore.Mvc;
namespace Contacts.Controllers
{
    public class ContactController : Controller
    {
        private readonly ContactContext context;

        public ContactController(ContactContext ctx)
        {
            context = ctx;
        }

        [HttpGet]
        public IActionResult Details(Contact contact)
        {
            return View(contact);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            return View("Edit", new Contact());
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            ViewBag.Action = "Edit";
            return View("Edit", new Contact());
        }
        
        [HttpPost]
        public IActionResult Edit(Contact contact)
        {
            if (ModelState.IsValid)
            {
                if (contact.ContactID == 0)
                {
                    context.Contacts.Add(contact);
                }
                else
                {
                    context.Contacts.Update(contact);
                }
                context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Action = (contact.ContactID == 0) ? "Add" : "Edit";
                return View(contact);
            }
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            var contact = context.Contacts.Find(id);
            return View(contact);
        }
        
        public IActionResult Delete(Contact contact)
        {
            context.Contacts.Remove(contact);
            context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}