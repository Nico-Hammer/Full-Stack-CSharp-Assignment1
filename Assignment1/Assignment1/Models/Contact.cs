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
        public string Phonenumber { get; set; }
        
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Please provide a valid email address")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Category is required")]
        [Range(1, 3, ErrorMessage = "Please select a category")]
        public int? CategoryID { get; set; }
        
        public string? Organization { get; set; }

        public string Slug => FirstName?.Replace(' ', '-').ToLower() + '-' + LastName?.ToLower();
    }
}