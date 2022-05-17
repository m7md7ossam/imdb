using IMDB.Models;
using IMDB.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IMDB.Controllers
{
    
    public class MovieController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        [Authorize(Roles = "Admin")]
        // GET: Movie
        public ActionResult Index()
        {
            return View(db.Movies.ToList());
        }

        [Authorize(Roles = "Admin")]


        [Authorize(Roles = "Admin")]
        // GET: Movie/Create
        public ActionResult Create()
        {
            //ViewBag.SequelId = new SelectList(db.Movies, "id", "Name");
            ViewBag.Genre = new SelectList(db.Genres, "Id", "GenreName");
            ViewBag.Country = new SelectList(db.Countries, "Id", "CountryName");

            ViewBag.Director = new SelectList(db.Directors, "Id", "FirstName");
            ViewBag.Actor = new MultiSelectList(db.Actors, "Id", "FirstName");
            return View();
        }

        // POST: Movie/Create
        [HttpPost]
        public ActionResult Create(FormCollection form, HttpPostedFileBase file)
        {
            
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    
                    Movie movie = new Movie
                        {
                            Name = form["Movie.Name"],
                            Rank = Convert.ToDouble(form["Movie.Rank"]),
                            Year = Convert.ToInt32(form["Movie.Year"]),
                            Description = form["Movie.Description"]
                        };
                    string ActorsString = form["Actor"];
                    int DirectorId = Convert.ToInt32(form["Director"]);
                    int GenreId = Convert.ToInt32(form["Genre"]);
                    int CountryId = Convert.ToInt32(form["Country"]);
                    string[] Actors = ActorsString.Split(',');
                    if (file != null)
                    {

                        db.Movies.Add(movie);
                        db.SaveChanges();
                        file.SaveAs(HttpContext.Server.MapPath("~/Images/Movie/") + movie.id + System.IO.Path.GetExtension(file.FileName));
                            movie.Image = "/images/Movie/" + movie.id + System.IO.Path.GetExtension(file.FileName);


                        db.Entry(movie).State = System.Data.Entity.EntityState.Modified;
                            
                            db.SaveChanges();

                            

                            MovieDirector movieDirector = new MovieDirector
                            {
                                DirectorId = DirectorId,
                                MovieId = movie.id
                            };

                            db.MoviesDirectors.Add(movieDirector);

                            
                            MovieGenre movieGenre = new MovieGenre
                            {
                                GenreId = GenreId,
                                MovieId = movie.id
                            };

                            db.MoviesGenres.Add(movieGenre);

                            
                            MovieCountry movieCountry = new MovieCountry
                            {
                                CountryId = CountryId,
                                MovieId = movie.id
                            };

                            db.MovieCountry.Add(movieCountry);

                        

                        foreach (var item in Actors)
                            {
                                db.MovieActors.Add(new MovieActor
                                {
                                    MovieId = movie.id,
                                    ActorId = Convert.ToInt32(item)
                                });
                            }
                            db.SaveChanges();   
                    }

                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }

        }
        
        [Authorize(Roles = "Admin")]
        // GET: Movie/Edit/5
        public ActionResult Edit(int id)
        {
            MovieDirector movieDirector = db.MoviesDirectors.SingleOrDefault(x=>x.MovieId == id);
            ViewBag.Director = new SelectList(db.Directors, "Id", "FirstName", movieDirector.DirectorId);

            MovieGenre movieGenre = db.MoviesGenres.SingleOrDefault(x => x.MovieId == id);
            ViewBag.Genre = new SelectList(db.Genres, "Id", "GenreName", movieGenre.GenreId);

            MovieCountry movieCountry = db.MovieCountry.SingleOrDefault(x => x.MovieId == id);
            ViewBag.Country = new SelectList(db.Countries, "Id", "CountryName", movieCountry.CountryId);


            List<MovieActor> movieActor = db.MovieActors.Where(x => x.MovieId == id).ToList();

            List<string> SelectedActors = new List<string>();
            foreach (var item in movieActor)
            {
                SelectedActors.Add(item.ActorId.ToString());
            }

            ViewBag.Actor = new MultiSelectList(db.Actors, "Id", "FirstName", SelectedActors);
            MovieViewModel movieViewModel = new MovieViewModel 
            { 
                Movie = db.Movies.SingleOrDefault(x => x.id == id)
            };
            return View(movieViewModel);
        }

        // POST: Movie/Edit/5
        [HttpPost]
        public ActionResult Edit(FormCollection form, HttpPostedFileBase file)
        {
            try
            {
                // TODO: Add update logic here

                if (ModelState.IsValid)
                {
                    Movie movie = new Movie
                    {
                        id = Convert.ToInt32(form["Movie.id"]),
                        Name = form["Movie.Name"],
                        Year = Convert.ToInt32(form["Movie.Year"]),
                        Description = form["Movie.Description"],
                        Rank = Convert.ToInt32(form["Movie.Rank"])
                    };
                    if (file != null)
                    {
                        if (System.IO.File.Exists(Server.MapPath(form["Movie.Image"])))
                        {
                            System.IO.File.Delete(Server.MapPath(form["Movie.Image"]));
                        }
                        file.SaveAs(HttpContext.Server.MapPath("~/Images/Movie/") + movie.id + System.IO.Path.GetExtension(file.FileName));
                        movie.Image = "/images/Movie/" + movie.id + System.IO.Path.GetExtension(file.FileName);
                        
                    }
                    else
                    {
                        movie.Image = form["Movie.Image"];
                    }

                    string ActorsString = form["Actor"];
                    string DirectorsString = form["Director"];
                    string GenreString = form["Genre"];
                    string CountryString = form["Country"];
                    
                        MovieDirector movieDirector= db.MoviesDirectors
                        .SingleOrDefault(x=>x.MovieId == movie.id);
                    db.MoviesDirectors.Remove(movieDirector);

                        MovieGenre movieGenre = db.MoviesGenres
                        .SingleOrDefault(x => x.MovieId == movie.id);
                        db.MoviesGenres.Remove(movieGenre);

                        MovieCountry movieCountry = db.MovieCountry
                        .SingleOrDefault(x => x.MovieId == movie.id);
                        db.MovieCountry.Remove(movieCountry);

                        List<MovieActor> movieActors = db.MovieActors
                        .Where(x => x.MovieId == movie.id).ToList();
                    
                    foreach (var item in movieActors)
                    {
                        db.MovieActors.Remove(item);
                    }
                    db.SaveChanges();

                        db.MoviesDirectors.Add(new MovieDirector
                        {
                            DirectorId = Convert.ToInt32(DirectorsString),
                            MovieId = movie.id
                        });
                    
                        db.MoviesGenres.Add(new MovieGenre
                        {
                            GenreId = Convert.ToInt32(GenreString),
                            MovieId = movie.id
                        });
                    
                        db.MovieCountry.Add(new MovieCountry
                        {
                            CountryId = Convert.ToInt32(CountryString),
                            MovieId = movie.id
                        });
                    
                        string[] Actors = ActorsString.Split(',');

                        foreach (var item in Actors)
                        {
                            db.MovieActors.Add(new MovieActor
                            {
                                ActorId = Convert.ToInt32(item),
                                MovieId = movie.id
                            });
                        }
                        db.Entry(movie).State = System.Data.Entity.EntityState.Modified;

                        db.SaveChanges();
                        return RedirectToAction("Index");

                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [Authorize(Roles = "Admin")]
        // GET: Movie/Delete/5


        // POST: Movie/Delete/5
        [HttpPost]
        public ActionResult Delete(Movie movie)
        {
            try
            {
                // TODO: Add delete logic here
                Movie MyMovie = db.Movies.SingleOrDefault(x => x.id == movie.id);

                if (System.IO.File.Exists(Server.MapPath(MyMovie.Image)))
                {
                    System.IO.File.Delete(Server.MapPath(MyMovie.Image));
                }

                db.Movies.Remove(MyMovie);
                //MovieDirector movieDirector = db.MoviesDirectors.SingleOrDefault(x=>x.MovieId == movie.id);
                //db.MoviesDirectors.Remove(movieDirector);

                //List<MovieActor> movieActors = db.MovieActors.Where(x => x.MovieId == movie.id).ToList();
                //foreach (var item in movieActors)
                //{
                //    db.MovieActors.Remove(item);
                //}
                
                db.SaveChanges();

                
                return Json(new { result = 1 });
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult MovieProfile(int? id)
        {
            Movie movie = db.Movies.SingleOrDefault(x=>x.id == id);
            List<Review> Reviews = db.Reviews.Where(x => x.MovieId == id).ToList();
            List<UserReviewViewModel> MyReviews = new List<UserReviewViewModel>();
            foreach (var item in Reviews)
            {
                MyReviews.Add(new UserReviewViewModel
                {
                    Reviews = item,
                    User = db.Users.Where(x => x.Id == item.UserId).SingleOrDefault()
                });
            }

            MovieDirector movieDirector = db.MoviesDirectors.SingleOrDefault(x=>x.MovieId == id);

            Director director = db.Directors.SingleOrDefault(x => x.Id == movieDirector.DirectorId);

            MovieGenre movieGenre = db.MoviesGenres.SingleOrDefault(x => x.MovieId == id);

            Genre genre = db.Genres.SingleOrDefault(x => x.Id == movieGenre.GenreId);

            MovieCountry movieCountry = db.MovieCountry.SingleOrDefault(x => x.MovieId == id);

            Country country = db.Countries.SingleOrDefault(x => x.Id == movieCountry.CountryId);

            List<MovieActor> movieActor = db.MovieActors.Where(x => x.MovieId == id).ToList();
            List<Actor> actors = new List<Actor>();

            foreach (var item in movieActor)
            {
                actors.Add(db.Actors.SingleOrDefault(x=>x.Id == item.ActorId));
            }

            MovieProfileViewModel movieProfileViewModel = new MovieProfileViewModel
            {
                Movie = movie,
                Reviews =MyReviews,
                movies = db.Movies.ToList(),
                Director = director,
                Actors = actors,
                Genre = genre,
                Country = country
            };
            return View(movieProfileViewModel);
        }


        public bool IsExist(int id)
        {
            string UserId = User.Identity.GetUserId();
            int MovieId = id;
            LikeDislikeMovie Test = db.LikeDislikeMovies.Where(x => x.MovieId == MovieId)
                .Where(x => x.UserId == UserId).SingleOrDefault();
            if (Test != null)
            {
                return true;
            }
            return false;
        }

        [Authorize]
        public ActionResult Like(int? id)
        {
           if(id != null)
           {
                if (IsExist((int)id))
                {
                    string UserId = User.Identity.GetUserId();
                    int MovieId = (int)id;
                    LikeDislikeMovie LikedMovie = db.LikeDislikeMovies.Where(x => x.MovieId == MovieId)
                    .Where(x => x.UserId == UserId).SingleOrDefault();
                    if (LikedMovie.Status == "Like")
                    {
                        db.LikeDislikeMovies.Remove(LikedMovie);
                    }
                    else
                    {
                        LikedMovie.Status = "Like";
                        db.Entry(LikedMovie).State = System.Data.Entity.EntityState.Modified;
                    }
                    
                }
                else
                {
                    LikeDislikeMovie LikedMovie = new LikeDislikeMovie
                    {
                        MovieId = (int)id,
                        Status = "Like",
                        UserId = User.Identity.GetUserId()
                    };
                    db.LikeDislikeMovies.Add(LikedMovie);
                }
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Index", "Home");

        }


        [Authorize]
        public ActionResult Dislike(int? id)
        {
            if (id != null)
            {
                if (IsExist((int)id))
                {
                    string UserId = User.Identity.GetUserId();
                    int MovieId = (int)id;
                    LikeDislikeMovie LikedMovie = db.LikeDislikeMovies.Where(x => x.MovieId == MovieId)
                    .Where(x => x.UserId == UserId).SingleOrDefault();
                    if (LikedMovie.Status == "Dislike")
                    {
                        db.LikeDislikeMovies.Remove(LikedMovie);
                    }
                    else
                    {
                        LikedMovie.Status = "Dislike";
                        db.Entry(LikedMovie).State = System.Data.Entity.EntityState.Modified;
                    }
                }
                else
                {
                    LikeDislikeMovie LikedMovie = new LikeDislikeMovie
                    {
                        MovieId = (int)id,
                        Status = "Dislike",
                        UserId = User.Identity.GetUserId()
                    };
                    db.LikeDislikeMovies.Add(LikedMovie);
                }
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Index", "Home");

        }

        public bool IsLiked(int id)
        {
            if (IsExist(id))
            {
                string UserId = User.Identity.GetUserId();
                int MovieId = id;
                LikeDislikeMovie Test = db.LikeDislikeMovies.Where(x => x.MovieId == MovieId)
                    .Where(x => x.UserId == UserId)
                    .Where(x=>x.Status == "Like")
                    .SingleOrDefault();
                if (Test != null)
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        [Authorize]
        public ActionResult LikedMovies()
        {
            string UserId = User.Identity.GetUserId();
            List<LikeDislikeMovie> Liked = db.LikeDislikeMovies.Where(x => x.UserId == UserId)
                    .Where(x => x.Status == "Like").ToList();

            List<Movie> LikedMovies = new List<Movie>();

            foreach (var item in Liked)
            {
                LikedMovies.Add(db.Movies.SingleOrDefault(x=>x.id == item.MovieId));
            }

            return View(LikedMovies);
        }

        [Authorize]
        public ActionResult DislikedMovies()
        {
            string UserId = User.Identity.GetUserId();
            List<LikeDislikeMovie> Liked = db.LikeDislikeMovies.Where(x => x.UserId == UserId)
                    .Where(x => x.Status == "Dislike").ToList();

            List<Movie> LikedMovies = new List<Movie>();

            foreach (var item in Liked)
            {
                LikedMovies.Add(db.Movies.SingleOrDefault(x => x.id == item.MovieId));
            }

            return View(LikedMovies);
        }
        [Authorize]
        public ActionResult AddToFavorite(int? id)
        {
            if (id != null)
            {
                string UserId = User.Identity.GetUserId();
                int MovieId = (int)id;
                db.FavoriteMovies.Add(new FavoriteMovie
                {
                    MovieId = MovieId,
                    UserId = UserId
                });
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public ActionResult DeleteFromFavorite(int? id)
        {
            if (id != null)
            {
                string UserId = User.Identity.GetUserId();
                int MovieId = (int)id;
                FavoriteMovie favoriteMovie = db.FavoriteMovies.Where(x => x.MovieId == id)
                    .Where(x => x.UserId == UserId).SingleOrDefault();
                db.FavoriteMovies.Remove(favoriteMovie);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public ActionResult FavoriteMovies()
        {
            string UserId = User.Identity.GetUserId();
            List<FavoriteMovie> favoriteMovies = db.FavoriteMovies.Where(x=>x.UserId == UserId).ToList();
            List<Movie> movies = new List<Movie>();
            foreach (var item in favoriteMovies)
            {
                movies.Add(db.Movies.SingleOrDefault(x=>x.id == item.MovieId));
            }
            return View(movies);
        }

        public bool IsInFavorite(int id)
        {
            string UserId = User.Identity.GetUserId();
            FavoriteMovie test = db.FavoriteMovies.Where(x => x.MovieId == id).Where(x => x.UserId == UserId).SingleOrDefault();
            if(test == null)
            {
                return false;
            }
            return true;
        }

        [HttpPost]
        public JsonResult IsNameExists(Movie movie)
        {

            Movie myMovie = null;
            if (movie.id != 0)
            {
                myMovie = db.Movies.Where(x => x.Name == movie.Name).Where(x => x.id != movie.id)
                .SingleOrDefault();
            }
            else
            {
                myMovie = db.Movies.SingleOrDefault(x => x.Name == movie.Name);
            }
            if (myMovie == null)
            {
                return Json(true);
            }
            return Json(false);
        }

        [HttpPost]

        public ActionResult CheckFileExtension( )
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
