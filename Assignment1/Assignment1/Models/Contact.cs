/* import DataAnnotations to make MVC operations easier
   import MVC Validation so that the inputs from the user for the contact can be validated properly
*/
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

/// <summary>
/// Add the contact class to the Models namespace so it can be used elsewhere
/// </summary>
namespace Assignment1.Models
{
    /// <summary>
    /// Contact class that stores all the information about a given contact while also providing data validation
    /// </summary>
    public class Contact
    {
        public int ContactID { get; set; }

        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }

        private string phonenumber; // used to store the raw phone number input

        /// <summary>
        /// Get the phone number from above and provide validation for it, making sure it's the proper length and then
        /// automatically format it to the (xxx)-xxx-xxxxx format before setting the phone number variable above to the
        /// now valid value
        /// </summary>
        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Please provide a valid phone number")]
        public string Phonenumber {
            get => phonenumber;
            set {
                if (!string.IsNullOrEmpty(value)) {
                    var digits = new string(value.Where(char.IsDigit).ToArray()); // create an array of the digits in the string

                    // format the phone number when it contains 10 digits otherwise return an invalid value
                    if (digits.Length == 10)
                        phonenumber = $"({digits.Substring(0, 3)})-{digits.Substring(3, 3)}-{digits.Substring(6, 4)}";
                    else
                        phonenumber = value;
                }
                else { phonenumber = value; }
            }
        }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Please provide a valid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Category is required")]
        [Range(1, 3, ErrorMessage = "Please select a category")]
        public int? CategoryID { get; set; }
        
        // since the category name is never used for any lookup or stored anywhere, it doesn't need to be validated
        [ValidateNever]
        public Category CategoryName { get; set; }

        public string? Organization { get; set; }

        // gets the time of contact creation and formats that to a string with the provided format
        public string CreatedDateTime { get; set; } = DateTime.Now.ToString("MM/dd/yyyy 'at' h:mm tt");

        /* create the slug by taking the first and last names as lowercase strings and replacing spaces with "-"
           i.e. "John Doe" becomes "john-doe", after which it is added to the url so that current contact is displayed
           in the url
        */
        public string Slug => FirstName?.Replace(' ', '-').ToLower() + '-' + LastName?.ToLower();
    }
}