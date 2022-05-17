using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IMDB.ViewModels
{
    public class EditUserViewModel
    {

        public string Id { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        [Remote("IsUserEmailExists", "User", HttpMethod = "POST", ErrorMessage = "Email already exists.", AdditionalFields = "Id")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "UseName")]
        [Remote("IsUserNameExists", "User", HttpMethod = "POST", ErrorMessage = "UserName already exists.",AdditionalFields ="Id")]
        [MinLength(2,ErrorMessage = "UserName should be greater than 2")]
        [MaxLength(15,ErrorMessage = "UserName should be less than 15")]
        [RegularExpression(@"^(?![0-9._])(?!.*[0-9._]$)(?!.*\d_)(?!.*_\d)[a-zA-Z0-9_]+$", ErrorMessage = "UserName should not contain Special character")]
        public string UserName { get; set; }

        public string ProfileImage { get; set; }

        [Required(ErrorMessage = "Please enter first name.")]
        [RegularExpression(@"^([A-z][A-Za-z]*\s+[A-Za-z]*)|([A-z][A-Za-z]*)$", ErrorMessage = "FirstName should not contain numbers")]
        public string FirstName { get; set; }


        [Required(ErrorMessage = "Please enter last name.")]
        [RegularExpression(@"^([A-z][A-Za-z]*\s+[A-Za-z]*)|([A-z][A-Za-z]*)$", ErrorMessage = "LastName should not contain numbers")]
        public string LastName { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Remote("IsUserPhoneNumberExists", "User", HttpMethod = "POST", ErrorMessage = "PhoneNumber already exists.", AdditionalFields = "Id")]
        [RegularExpression(@"^\(?([0]{1}[1]{1})\)?(\d{9})$", ErrorMessage = "PhoneNumber should be 11 number start with 01")]
        public string PhoneNumber { get; set; }

        public bool EmailConfirmed { get; set; }

        public string PasswordHash { get; set; }

        public string SecurityStamp { get; set; }

        public bool PhoneNumberConfirmed { get; set; }

        public bool TwoFactorEnabled { get; set; }

        public DateTime? LockoutEndDateUtc { get; set; }

        public bool LockoutEnabled { get; set; }

        public int AccessFailedCount { get; set; }
    }
}