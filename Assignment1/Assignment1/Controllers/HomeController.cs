using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Assignment1.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Contacts.Controllers {
    public class HomeController : Controller
    {
        private ContactContext context { get; set; }
        public HomeController(ContactContext ctx)
        {
            context = ctx;
        }
        public IActionResult Index()
        {
            var contacts = context.Contacts.ToList();
            return View(contacts);
        }
    }
}