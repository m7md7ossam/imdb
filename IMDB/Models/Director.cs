using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IMDB.Models
{
    public class Director
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter first name.")]
        [RegularExpression(@"^([A-z][A-Za-z]*\s+[A-Za-z]*)|([A-z][A-Za-z]*)$", ErrorMessage = "FirstName should not contain numbers")]
        [Remote("IsFirstNameExists", "Director", HttpMethod = "POST", ErrorMessage = "FirstName already exists.", AdditionalFields = "Id")]
        [MinLength(2, ErrorMessage = "FirstName should be greater than 2")]
        [MaxLength(15, ErrorMessage = "FirstName should be less than 15")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter last name.")]
        [RegularExpression(@"^([A-z][A-Za-z]*\s+[A-Za-z]*)|([A-z][A-Za-z]*)$", ErrorMessage = "LastName should not contain numbers")]
        [Remote("IsLastNameExists", "Director", HttpMethod = "POST", ErrorMessage = "LastName already exists.", AdditionalFields = "Id")]
        [MinLength(2, ErrorMessage = "LastName should be greater than 2")]
        [MaxLength(15, ErrorMessage = "LastName should be less than 15")]
        public string LastName { get; set; }

        public string Image { get; set; }
    }
}