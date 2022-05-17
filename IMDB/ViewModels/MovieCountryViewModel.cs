using IMDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMDB.ViewModels
{
    public class MovieCountryViewModel
    {
        public Country Country { get; set; }

        public IEnumerable<Movie> Movies { get; set; }
    }
}