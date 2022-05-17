using IMDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMDB.ViewModels
{
    public class MoviesActorsDirectorsViewModel
    {
        public IEnumerable<Movie> Movies { get; set; }
        public IEnumerable<Actor> Actors{ get; set; }
        public IEnumerable<Director> Directors{ get; set; }

        public IEnumerable<Genre> Genres { get; set; }
    }
}