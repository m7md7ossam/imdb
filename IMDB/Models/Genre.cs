using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IMDB.Models
{
    public class Genre
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter genre name.")]
        [RegularExpression(@"^([A-z][A-Za-z]*\s+[A-Za-z]*)|([A-z][A-Za-z]*)$", ErrorMessage = "Genre name should not contain numbers")]
        [Remote("IsGenreNameExists", "Genre", HttpMethod = "POST", ErrorMessage = "GenreName name already exists.", AdditionalFields = "Id")]
        [MinLength(2, ErrorMessage = "GenreName should be greater than 2")]
        [MaxLength(15, ErrorMessage = "GenreName should be less than 15")]
        public string GenreName { get; set; }
    }
}