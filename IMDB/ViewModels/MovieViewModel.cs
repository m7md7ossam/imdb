using IMDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMDB.ViewModels
{
    public class MovieViewModel
    {
        public Movie Movie { get; set; }

        public List<Actor> Actors { get; set; }

        public Director Director { get; set; }
        public Genre Genre { get; set; }
        public Country Country { get; set; }
    }
}