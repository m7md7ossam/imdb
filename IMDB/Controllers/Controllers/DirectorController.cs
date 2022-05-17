using IMDB.Models;
using IMDB.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IMDB.Controllers
{
    
    public class DirectorController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Director
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View(db.Directors.ToList());
        }

        [Authorize(Roles = "Admin")]
        // GET: Director/Details/5
        public ActionResult Details(int id)
        {
            return View(db.Directors.SingleOrDefault(x => x.Id == id));
        }

        [Authorize(Roles = "Admin")]
        // GET: Director/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Director/Create
        [HttpPost]
        public ActionResult Create(Director director, HttpPostedFileBase file)
        {
            try
            {
                // TODO: Add insert logic here

                if (ModelState.IsValid)
                {
                    if (file != null)
                    {
                        if (System.IO.File.Exists(Server.MapPath(director.Image)))
                        {
                            System.IO.File.Delete(Server.MapPath(director.Image));
                        }
                        file.SaveAs(HttpContext.Server.MapPath("~/Images/Director/") + director.Id + System.IO.Path.GetExtension(file.FileName));
                        director.Image = "/images/Director/" + director.Id + System.IO.Path.GetExtension(file.FileName);

                        db.Directors.Add(director);
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
        // GET: Director/Edit/5
        public ActionResult Edit(int id)
        {
            return View(db.Directors.SingleOrDefault(x => x.Id == id));
        }

        // POST: Director/Edit/5
        [HttpPost]
        public ActionResult Edit(Director director, HttpPostedFileBase file)
        {
            try
            {
                // TODO: Add update logic here

                if (ModelState.IsValid)
                {
                    if (file != null)
                    {
                        if (System.IO.File.Exists(Server.MapPath(director.Image)))
                        {
                            System.IO.File.Delete(Server.MapPath(director.Image));
                        }
                        file.SaveAs(HttpContext.Server.MapPath("~/Images/Director/") + director.Id + System.IO.Path.GetExtension(file.FileName));
                        director.Image = "/images/Director/" + director.Id + System.IO.Path.GetExtension(file.FileName);
                    }
                    db.Entry(director).State = System.Data.Entity.EntityState.Modified;

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


        // POST: Director/Delete/5
        [HttpPost]
        public ActionResult Delete(Director director)
        {
            try
            {
                // TODO: Add delete logic here
                Director MyDirector = db.Directors.SingleOrDefault(x => x.Id == director.Id);
                if (System.IO.File.Exists(Server.MapPath(MyDirector.Image)))
                {
                    System.IO.File.Delete(Server.MapPath(MyDirector.Image));
                }
                db.Directors.Remove(MyDirector);
                db.SaveChanges();
                return Json(new { result = 1 });
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult DirectorProfile(int? id)
        {
            Director director= db.Directors.SingleOrDefault(x => x.Id == id);
            List<MovieDirector> movieDirector = db.MoviesDirectors.Where(x => x.DirectorId == id).ToList();
            List<Movie> movies = new List<Movie>();
            foreach (var item in movieDirector)
            {
                movies.Add(db.Movies.SingleOrDefault(x => x.id == item.MovieId));
            }

            DirectorProfileViewModel directorProfileViewModel = new DirectorProfileViewModel { 
                Director = director,
                Movies = movies
            };
            return View(directorProfileViewModel);
        }

        [HttpPost]
        public JsonResult IsFirstNameExists(string firstName, int? id)
        {

            Director director = null;
            if(id != null)
            {
                director = db.Directors.Where(x => x.FirstName == firstName).Where(x => x.Id != id)
                .SingleOrDefault();
            }
            else
            {
                director = db.Directors.SingleOrDefault(x => x.FirstName == firstName);
            }
            if (director == null)
            {
                return Json(true);
            }
            return Json(false);
        }

        [HttpPost]
        public JsonResult IsLastNameExists(string lastName, int? id)
        {

            Director director = null;
            if (id != null)
            {
                director = db.Directors.Where(x => x.LastName == lastName).Where(x => x.Id != id)
                .SingleOrDefault();
            }
            else
            {
                director = db.Directors.SingleOrDefault(x => x.LastName == lastName);
            }
                
            if (director == null)
            {
                return Json(true);
            }
            return Json(false);
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
