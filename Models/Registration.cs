using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace WebApplication2.Models
{
    public class Registration
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Field can't be empty")]

        public string Firstname { get; set; }

        [Required(ErrorMessage = "Field can't be empty")]
        public string Lastname { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Phone Number Required!")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$",
                   ErrorMessage = "Entered phone format is not valid.")]
        public string ContactNo { get; set; }

        [Required(ErrorMessage = "Field can't be empty")]
        //[EmailAddress]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$",
                   ErrorMessage = "Entered Email format is not valid.")]
        public string EmailId { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [MembershipPassword(MinRequiredNonAlphanumericCharacters = 1, MinNonAlphanumericCharactersError = "Your password needs to contain at least one symbol (!, @, #, etc).",
        ErrorMessage = "Your password must be 6 characters long and contain at least one symbol (!, @, #, etc).")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /*[Compare("Password", ErrorMessage = "Confirm password doesn't match, Type again !")]  
         [DataType(DataType.Password)]
         public string ConfirmPassword { get; set; }*/

        public string RoleName { get; set; }
        public List<Roles> AvailableRoles { get; set; }

    }
    public class Roles
    {
        public int ID { get; set; }
        public string RoleName { get; set; }
        public bool Checked { get; set; }


    }
}