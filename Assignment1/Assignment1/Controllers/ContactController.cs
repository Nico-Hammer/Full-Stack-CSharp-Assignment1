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
        public IActionResult Add(Contact contact)
        {
            // Validates and formats the phone number
            if (!string.IsNullOrEmpty(contact.Phonenumber))
            {
                var digits = new string(contact.Phonenumber.Where(char.IsDigit).ToArray());
                if (digits.Length == 10)
                    contact.Phonenumber = $"{digits.Substring(0, 3)}-{digits.Substring(3, 3)}-{digits.Substring(6, 4)}";
                else
                    ModelState.AddModelError("Phonenumber", "Phone number must be 10 digits.");
            }

            if (ModelState.IsValid)
            {
                contact.CreatedDateTime = DateTime.Now.ToString("MM/dd/yyyy 'at' hh:mm tt");
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
        public IActionResult Edit(Contact contact)
        {
            // Validates and formats the phone number
            if (!string.IsNullOrEmpty(contact.Phonenumber))
            {
                var digits = new string(contact.Phonenumber.Where(char.IsDigit).ToArray());
                if (digits.Length == 10)
                    contact.Phonenumber = $"{digits.Substring(0, 3)}-{digits.Substring(3, 3)}-{digits.Substring(6, 4)}";
                else
                    ModelState.AddModelError("Phonenumber", "Phone number must be 10 digits.");
            }

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
