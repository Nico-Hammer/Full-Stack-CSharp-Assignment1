using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
namespace Assignment1.Models
{
    public class Contact
    {
        
        public int ContactID { get; set; }
        
        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }
        
        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }
        
        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage="Please provide a valid phone number")]
        public int? Phonenumber { get; set; }
        
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Please provide a valid email address")]
        public string? Email { get; set; }
        
        [Required(ErrorMessage = "Category is required")]
        public string Category { get; set; }
        
        public string Organization { get; set; }

        public string Slug => FirstName?.Replace(' ', '-').ToLower() + '-' + LastName?.ToLower();
    }
}