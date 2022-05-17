using IMDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IMDB.Controllers
{
    [Authorize(Roles = "Admin")]
    public class GenreController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Genre
        public ActionResult Index()
        {
            return View(db.Genres.ToList());
        }

        // GET: Genre/Details/5
        public ActionResult Details(int id)
        {
            return View(db.Genres.SingleOrDefault(x=>x.Id == id));
        }

        // GET: Genre/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Genre/Create
        [HttpPost]
        public ActionResult Create(Genre genre)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    db.Genres.Add(genre);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: Genre/Edit/5
        public ActionResult Edit(int id)
        {
            return View(db.Genres.SingleOrDefault(x=>x.Id == id));
        }

        // POST: Genre/Edit/5
        [HttpPost]
        public ActionResult Edit(Genre genre)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    db.Entry(genre).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: Genre/Delete/5
        

        // POST: Genre/Delete/5
        [HttpPost]
        public ActionResult Delete(Genre genre)
        {
            try
            {
                // TODO: Add delete logic here
                Genre myGenre = db.Genres.SingleOrDefault(x => x.Id == genre.Id);
                db.Genres.Remove(myGenre);
                db.SaveChanges();
                return Json(new { result = 1 });
            }
            catch
            {
                return View();
            }
        }
        [HttpPost]
        public JsonResult IsGenreNameExists(string genreName, int? id)
        {

            Genre genre= null;
            if (id != null)
            {
                genre = db.Genres.Where(x => x.GenreName == genreName).Where(x => x.Id != id)
                .SingleOrDefault();
            }
            else
            {
                genre = db.Genres.SingleOrDefault(x => x.GenreName == genreName);
            }

            if (genre == null)
            {
                return Json(true);
            }
            return Json(false);
        }
    }
}
