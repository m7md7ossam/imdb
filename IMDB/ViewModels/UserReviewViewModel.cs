using IMDB.Migrations;
using IMDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMDB.ViewModels
{
    public class UserReviewViewModel
    {
        public Models.Review Reviews { get; set; }

        public ApplicationUser User { get; set; }
    }
}