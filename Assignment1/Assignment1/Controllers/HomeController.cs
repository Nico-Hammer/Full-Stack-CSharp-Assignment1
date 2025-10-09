/* import models
   import ASP.NET MVC functionality
   import EFCore database functionality
   import LINQ functionality to query the database
*/
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Assignment1.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Create the Controllers namespace so that the controllers can be used elsewhere
/// </summary>
namespace Contacts.Controllers {
    
    /// <summary>
    /// Create a controller class that inherits the MVC Controller attributes
    /// </summary>
    public class HomeController : Controller
    {
        private ContactContext context { get; set; } // create the database context for the Contact object
        
        /// <summary>
        /// Pass that database context to the home controller, setting the local context to what was passed
        /// </summary>
        /// <param name="ctx">Database context to work with the Contact objects</param>
        public HomeController(ContactContext ctx)
        {
            context = ctx;
        }
        
        /// <summary>
        /// Route for the index page that creates a list of contacts including the Category object, ordered by last name
        /// </summary>
        /// <returns>
        /// The list of Contact objects that are then passed to the View for rendering
        /// </returns>
        public IActionResult Index()
        {
            var contacts = context.Contacts.Include(c => c.CategoryName)
                .OrderBy(c => c.LastName)
                .ToList();
            return View(contacts);
        }
    }
}