using System;
using System.ComponentModel.DataAnnotations;
namespace LoginReg.Models
{
    public class User
    {
        [Key]
        public int id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required(ErrorMessage="hey dingus")]
        public string LastName  { get; set; }

        [Required(ErrorMessage="hey dingus")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string confirm_password { get; set; }
        
    }
}