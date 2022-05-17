using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IMDB.Models
{
    public class Movie
    {
        [Key]
        public int id { get; set; }

        [Required(ErrorMessage = "Please enter name.")]
        [RegularExpression(@"^([A-z][A-Za-z]*\s+[A-Za-z]*)|([A-z][A-Za-z]*)$", ErrorMessage = "name should not contain numbers")]
        [Remote("IsNameExists", "Movie", HttpMethod = "POST", ErrorMessage = "Movie Name already exists.", AdditionalFields = "id")]
        [MinLength(2, ErrorMessage = "Movie Name should be greater than 2")]
        [MaxLength(15, ErrorMessage = "Movie Name should be less than 15")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter Year.")]
        [Range(1800,2100, ErrorMessage = "Year should be in range 1800 to 2100")]
        public int Year { get; set; }

        [Required(ErrorMessage = "Please enter rank.")]
        [Range(0, 10, ErrorMessage = "rank should be in range 0 to 10")]
        public double Rank { get; set; }

        //public int SequelId { get; set; }

        //[ForeignKey("SequelId")]
        //public virtual Movie Sequel { get; set; }

        public string Image { get; set; }

        [Required(ErrorMessage = "Please enter description.")]
        [MinLength(15, ErrorMessage = "description length sholud be greater than 15")]
        [MaxLength(150, ErrorMessage = "description length sholud not be greater than 150")]
        public string Description { get; set; }
    }
}