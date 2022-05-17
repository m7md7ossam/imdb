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
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
       
        public ActionResult Index()
        {
            MoviesActorsDirectorsViewModel MoviesActorsDirectors = new MoviesActorsDirectorsViewModel
            {
                Movies = db.Movies.ToList(),
                Actors = db.Actors.ToList(),
                Directors = db.Directors.ToList(),
                Genres = db.Genres.ToList()
            };
            return View(MoviesActorsDirectors);
        }

        public ActionResult Search(string search)
        {
            MoviesActorsDirectorsViewModel MoviesActorsDirectors = new MoviesActorsDirectorsViewModel
            {
                Movies = db.Movies.Where(x => x.Name.Contains(search) || search == null).ToList(),
                Actors = db.Actors.Where(x => x.FirstName.Contains(search) || x.LastName.Contains(search) || search == null).ToList(),
                Directors = db.Directors.Where(x => x.FirstName.Contains(search) || x.LastName.Contains(search) || search == null).ToList()
            };
            return View(MoviesActorsDirectors);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
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



        public bool IsLiked(int id)
        {
            if (IsExist(id))
            {
                string UserId = User.Identity.GetUserId();
                int MovieId = id;
                LikeDislikeMovie Test = db.LikeDislikeMovies.Where(x => x.MovieId == MovieId)
                    .Where(x => x.UserId == UserId)
                    .Where(x => x.Status == "Like")
                    .SingleOrDefault();
                if (Test != null)
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        public bool IsInFavorite(int id)
        {
            string UserId = User.Identity.GetUserId();
            FavoriteMovie test = db.FavoriteMovies.Where(x => x.MovieId == id).Where(x => x.UserId == UserId).SingleOrDefault();
            if (test == null)
            {
                return false;
            }
            return true;
        }

        public ActionResult MovieGenre(int? id)
        {
            List<MovieGenre> movieGenre = db.MoviesGenres.Where(x => x.GenreId == id).ToList();
            List<Movie> movies = new List<Movie>();

            foreach (var item in movieGenre)
            {
                movies.Add(db.Movies.SingleOrDefault(x=>x.id == item.MovieId));
            }

            MovieGenreViewModel movieGenreViewModel = new MovieGenreViewModel 
            {
                Genre = db.Genres.SingleOrDefault(x=>x.Id == id),
                Movies = movies
            };
            return View(movieGenreViewModel);
        }

        public ActionResult MovieCountry(int? id)
        {
            List<MovieCountry> movieCountry = db.MovieCountry.Where(x => x.CountryId == id).ToList();
            List<Movie> movies = new List<Movie>();

            foreach (var item in movieCountry)
            {
                movies.Add(db.Movies.SingleOrDefault(x => x.id == item.MovieId));
            }

            MovieCountryViewModel movieCountryViewModel = new MovieCountryViewModel
            {
                Country = db.Countries.SingleOrDefault(x => x.Id == id),
                Movies = movies
            };
            return View(movieCountryViewModel);
        }

        [HttpPost]
        public ActionResult GetGenres()
        {
            List<Genre> genres = db.Genres.ToList();
            return Json(genres);
        }
        [HttpPost]
        public ActionResult Getcountries()
        {
            List<Country> countries = db.Countries.ToList();
            return Json(countries);
        }

        [HttpGet]
        public List<Movie> GetMovies(int id)
        {
            List<MovieGenre> movieGenre = db.MoviesGenres.Where(x => x.GenreId == id).ToList();
            List<Movie> movies = new List<Movie>();

            foreach (var item in movieGenre)
            {
                movies.Add(db.Movies.SingleOrDefault(x => x.id == item.MovieId));
            }
            return movies;
        }
    }
}