using IMDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMDB.ViewModels
{
    public class MovieGenreViewModel
    {
        public Genre Genre { get; set; }

        public IEnumerable<Movie> Movies { get; set; }
    }
}