using IMDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMDB.ViewModels
{
    public class ActorProfileViewModel
    {
        public Actor Actor { get; set; }

        public IEnumerable<Movie> Movies { get; set; }
    }
}