/* import the Models so that the Contact and Category objects can be used
   import AspNetCore MVC for MVC functionalities
   import LILNQ functionalities to allow transformation/filtering of database data
*/
using Assignment1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

/// <summary>
/// Add the contact controller to the Controllers namespace so it can be used elsewhere
/// </summary>
namespace Contacts.Controllers
{
    /// <summary>
    /// Create the contact controller class which inherits the Controller attributes
    /// </summary>
    public class ContactController : Controller
    {
        private readonly ContactContext context; // create the database context for this controller essentially as a constant

        // add that context to controller in a way that is accessible to other parts of the program
        public ContactController(ContactContext ctx)
        {
            context = ctx;
        }

        /// <summary>
        /// HTTP GET route to show the contact details page for the selected contact to the user
        /// </summary>
        /// <param name="id">ID of the selected contact</param>
        /// <returns>View with the contact details, or a 404 page if that id does not exist in the database</returns>
        [HttpGet]
        public IActionResult Details(int id)
        {
            var contact = context.Contacts.Find(id);
            if (contact == null)
                return NotFound();

            ViewBag.Categories = context.Categories.OrderBy(c => c.CategoryName).ToList();
            return View(contact);
        }

        /// <summary>
        /// HTTP GET route to show the add contact page to the user
        /// </summary>
        /// <returns>View with the add contact page where the relationship categories are sorted in ascending order </returns>
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Categories = context.Categories.OrderBy(c => c.CategoryName).ToList();
            return View(new Contact());
        }

        /// <summary>
        /// HTTP POST route to send the data for the new contact to the database
        /// </summary>
        /// <param name="contact">Contact object to store the new contact information</param>
        /// <returns>
        /// Send the user back to the index page if the contact information is valid, otherwise keep the user
        /// on the page and show errors for whatever is invalid
        /// </returns>
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
                contact.CreatedDateTime = DateTime.Now.ToString("MM/dd/yyyy 'at' hh:mm tt"); // get the datetime the contact was added
                context.Contacts.Add(contact);
                context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Categories = context.Categories.OrderBy(c => c.CategoryName).ToList();
            return View(contact);
        }

        /// <summary>
        /// HTTP GET route to send the user to the edit contact page
        /// </summary>
        /// <param name="id">ID for the selected contact</param>
        /// <returns>
        /// Shows the View with the contact information to edit, otherwise show a 404 page if the ID is not in
        /// the database
        /// </returns>
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var contact = context.Contacts.Find(id);
            if (contact == null)
                return NotFound();

            ViewBag.Categories = context.Categories.OrderBy(c => c.CategoryName).ToList();
            return View(contact);
        }

        /// <summary>
        /// HTTP POST route to send the edited contact information to the database
        /// </summary>
        /// <param name="contact">Contact object of the selected contact</param>
        /// <returns>
        /// View with the contact details for the selected contact after it has been edited, otherwise keep the user on
        /// the page with errors for all the invalid information
        /// </returns>
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

        /// <summary>
        /// Get the delete contact page when the user clicks to delte a contact, passing the contact ID for lookup
        /// </summary>
        /// <param name="id">Contact ID for the contact that was passed to the controller (clicked on by user)</param>
        /// <returns>The view if a contact with that ID is found</returns>
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var contact = context.Contacts.Find(id);
            if (contact == null)
                return NotFound();

            return View(contact);
        }

        /// <summary>
        /// Delete the contact from the database based on their ID as long as it exists in the database, then save the
        /// database after this change
        /// </summary>
        /// <param name="id">Contact ID that was passed to the controller from the previous delete page</param>
        /// <returns>Takes the user back to the index page after the contact is deleted</returns>
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