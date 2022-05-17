using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IMDB.Models
{
    public class Country
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter country name.")]
        [RegularExpression(@"^([A-z][A-Za-z]*\s+[A-Za-z]*)|([A-z][A-Za-z]*)$", ErrorMessage = "Country name should not contain numbers")]
        [Remote("IsCountryNameExists", "Country", HttpMethod = "POST", ErrorMessage = "Country Name name already exists.", AdditionalFields = "Id")]
        [MinLength(2, ErrorMessage = "CountryName should be greater than 2")]
        [MaxLength(15, ErrorMessage = "CountryName should be less than 15")]
        public string CountryName { get; set; }
    }
}