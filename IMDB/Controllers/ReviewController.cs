using IMDB.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IMDB.Controllers
{
    public class ReviewController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize]
        [HttpPost]
        //[Route("Review/AddReview/{MovieId}")]
        public ActionResult AddReview(FormCollection form)
        {
            Review review = new Review();
            review.UserId = User.Identity.GetUserId();
            review.CommentDate = DateTime.Now;
            review.Comment = form["Comment"];
            review.MovieId = Convert.ToInt32(form["Movie.id"]);
            db.Reviews.Add(review);
            db.SaveChanges();
            //return RedirectToAction("MovieProfile", "Movie", new {id = MovieId});

            return Json(new { result = 1 });

        }
    }
}
