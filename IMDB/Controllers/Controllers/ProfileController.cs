using IMDB.Models;
using IMDB.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IMDB.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Profile
        //[Authorize]
        public ActionResult Index()
        {
            var id = User.Identity.GetUserId();
            ApplicationUser user = db.Users.Find(id);

            List<FavoriteMovie> favoriteMovie = db.FavoriteMovies.Where(x => x.UserId == id).ToList();
            List<Movie> movies = new List<Movie>();
            foreach (var item in favoriteMovie)
            {
                movies.Add(db.Movies.SingleOrDefault(x=>x.id == item.MovieId));
            }

            List<FavoriteActor> favoriteActor = db.FavoriteActors.Where(x => x.UserId == id).ToList();
            List<Actor> actors = new List<Actor>();
            foreach (var item in favoriteActor)
            {
                actors.Add(db.Actors.SingleOrDefault(x => x.Id == item.ActorId));
            }

            List<FavoriteDirector> favoriteDirector = db.FavoriteDirectors.Where(x => x.UserId == id).ToList();
            List<Director> directors = new List<Director>();
            foreach (var item in favoriteDirector)
            {
                directors.Add(db.Directors.SingleOrDefault(x => x.Id == item.DirectorId));
            }


            ProfileViewModel profileViewModel = new ProfileViewModel 
            {
                User = user,
                FavoriteActors = actors,
                FavoriteDirectors = directors,
                FavoriteMovies = movies

            };

            return View(profileViewModel);
        }


        public ActionResult Edit()
        {
            var UserId = User.Identity.GetUserId();
            ApplicationUser user = db.Users.Find(UserId);
            List<FavoriteDirector> favoriteDirectors = db.FavoriteDirectors.Where(x=>x.UserId == UserId).ToList();
            List<FavoriteActor> favoriteActors = db.FavoriteActors.Where(x=>x.UserId == UserId).ToList();
            List<FavoriteMovie> favoriteMovies = db.FavoriteMovies.Where(x=>x.UserId == UserId).ToList();
            List<string> test = new List<string>();
            test.Add("2");
            test.Add("3");

            List<string> selectedDirectors = new List<string>();
            foreach (var item in favoriteDirectors)
            {
                selectedDirectors.Add(item.DirectorId.ToString());
            }
            List<string> selectedActors = new List<string>();
            foreach (var item in favoriteActors)
            {
                selectedActors.Add(item.ActorId.ToString());
            }
            List<string> selectedMovies = new List<string>();
            foreach (var item in favoriteMovies)
            {
                selectedMovies.Add(item.MovieId.ToString());
            }

            ViewBag.Director = new MultiSelectList(db.Directors, "Id", "FirstName",selectedDirectors);
            ViewBag.Actor = new MultiSelectList(db.Actors, "Id", "FirstName", selectedActors);
            ViewBag.Movie = new MultiSelectList(db.Movies, "id", "Name", selectedMovies);
            //TempData["file"] =  File.Wri ;

             return View(user);
        }

        [HttpPost]
        public ActionResult Edit(FormCollection form, HttpPostedFileBase file)
        {
            var UserId = User.Identity.GetUserId();
            ApplicationUser user = db.Users.Find(UserId);
            string img = form["ProfileImage"];
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    if (form["ProfileImage"] != "/images/Profile/user.png")
                    {
                        if (System.IO.File.Exists(Server.MapPath(form["ProfileImage"])))
                        {
                            System.IO.File.Delete(Server.MapPath(form["ProfileImage"]));
                        }
                    }
                    file.SaveAs(HttpContext.Server.MapPath("~/Images/Profile/") + user.Id + System.IO.Path.GetExtension(file.FileName));
                    user.ProfileImage = "/images/Profile/" + user.Id + System.IO.Path.GetExtension(file.FileName);
                }
                else
                {
                    user.ProfileImage = form["ProfileImage"];
                }
                    user.UserName = form["UserName"];
                    user.FirstName = form["FirstName"];
                    user.LastName = form["LastName"];
                    user.PhoneNumber = form["PhoneNumber"];

                    List<FavoriteDirector> favoriteDirectors = db.FavoriteDirectors.Where(x => x.UserId == UserId).ToList();
                    foreach (var item in favoriteDirectors)
                    {
                        db.FavoriteDirectors.Remove(item);
                    }
                    List<FavoriteActor> favoriteActors = db.FavoriteActors.Where(x => x.UserId == UserId).ToList();
                    foreach (var item in favoriteActors)
                    {
                        db.FavoriteActors.Remove(item);
                    }
                    List<FavoriteMovie> favoriteMovies = db.FavoriteMovies.Where(x => x.UserId == UserId).ToList();
                    foreach (var item in favoriteMovies)
                    {
                        db.FavoriteMovies.Remove(item);
                    }

                    db.SaveChanges();
                    string ActorsString = form["Actor"];

                    if(ActorsString != null)
                    {
                        string[] Actors = ActorsString.Split(',');

                        foreach (var item in Actors)
                        {
                            db.FavoriteActors.Add(new FavoriteActor
                            {
                                ActorId = Convert.ToInt32(item),
                                UserId = UserId
                            });
                        }
                    }
                    

                    string DirectorsString = form["Director"];

                    if(DirectorsString != null)
                    {
                        string[] Directors = DirectorsString.Split(',');

                        foreach (var item in Directors)
                        {
                            db.FavoriteDirectors.Add(new FavoriteDirector
                            {
                                DirectorId = Convert.ToInt32(item),
                                UserId = UserId
                            });
                        }
                    }
                    

                    string MovieString = form["Movie"];

                    if(MovieString != null)
                    {
                        string[] Movies = MovieString.Split(',');

                        foreach (var item in Movies)
                        {
                            db.FavoriteMovies.Add(new FavoriteMovie
                            {
                                MovieId = Convert.ToInt32(item),
                                UserId = UserId
                            });
                        }
                    }
                    


                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                

                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]

        public ActionResult CheckFileExtension()
        {
            if (Request.Files.Count > 0)
            {
                HttpFileCollectionBase files = Request.Files;
                HttpPostedFileBase file = files[0];
                var supportedTypes = new[] { ".png", ".jpg", ".jpeg" };
                var fileExt = System.IO.Path.GetExtension(file.FileName);
                if (supportedTypes.Contains(fileExt))
                {
                    return Json(new { result = 1 });
                }
            }

            return Json(new { result = 0 });
        }


    }
}
