using IMDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMDB.ViewModels
{
    public class DirectorProfileViewModel
    {
        public Director Director { get; set; }

        public IEnumerable<Movie> Movies { get; set; }
    }
}