using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer
{
    public class ContactInformation
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [RegularExpression(@"^[A-Z]+[a-zA-Z]*$", ErrorMessage = "Enter Valid First Name")]
        public string FirstName { get; set; }
        [Required]
        [RegularExpression(@"^[A-Z]+[a-zA-Z]*$", ErrorMessage = "Enter Valid Last Name")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Contact Number")]
        [RegularExpression(@"^((\+)?(\d{2}[-]))?(\d{10}){1}?$", ErrorMessage = "Enter Valid Contact number.")]
        public string ContactNumber { get; set; }

        public string Status { get; set; }
    }
}
