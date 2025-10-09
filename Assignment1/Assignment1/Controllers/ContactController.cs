using Assignment1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

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
        public IActionResult Details(int id)
        {
            var contact = context.Contacts.Find(id);
            if (contact == null)
                return NotFound();

            ViewBag.Categories = context.Categories.OrderBy(c => c.CategoryName).ToList();
            return View(contact);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Categories = context.Categories.OrderBy(c => c.CategoryName).ToList();
            return View(new Contact());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Contact contact)
        {
            if (ModelState.IsValid)
            {
                contact.CreatedDateTime = DateTime.Now.ToString("MM/dd/yyyy 'at'  hh:mm:ss");
                context.Contacts.Add(contact);
                context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Categories = context.Categories.OrderBy(c => c.CategoryName).ToList();
            return View(contact);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var contact = context.Contacts.Find(id);
            if (contact == null)
                return NotFound();

            ViewBag.Categories = context.Categories.OrderBy(c => c.CategoryName).ToList();
            return View(contact);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Contact contact)
        {
            if (ModelState.IsValid)
            {
                context.Contacts.Update(contact);
                context.SaveChanges();
                return RedirectToAction("Details", new { id = contact.ContactID });
            }

            ViewBag.Categories = context.Categories.OrderBy(c => c.CategoryName).ToList();
            return View(contact);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var contact = context.Contacts.Find(id);
            if (contact == null)
                return NotFound();

            return View(contact);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var contact = context.Contacts.Find(id);
            if (contact != null)
            {
                context.Contacts.Remove(contact);
                context.SaveChanges();
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
