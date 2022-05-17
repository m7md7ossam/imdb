using IMDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMDB.ViewModels
{
    public class ProfileViewModel
    {
        public ApplicationUser User { get; set; }

        public IEnumerable<Movie> FavoriteMovies { get; set; }

        public IEnumerable<Actor> FavoriteActors { get; set; }

        public IEnumerable<Director> FavoriteDirectors { get; set; }

    }
}