using IMDB.Models;
using IMDB.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IMDB.Controllers
{
    
    public class ActorController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Actor
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View(db.Actors.ToList());
        }

        [Authorize(Roles = "Admin")]
        // GET: Actor/Details/5
        public ActionResult Details(int id)
        {
            return View(db.Actors.SingleOrDefault(x => x.Id == id));
        }

        [Authorize(Roles = "Admin")]
        // GET: Actor/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Actor/Create
        [HttpPost]
        public ActionResult Create(Actor actor, HttpPostedFileBase file)
        {
            try
            {
                // TODO: Add insert logic here

                if (ModelState.IsValid)
                {
                    if (file != null)
                    {
                        db.Actors.Add(actor);
                        db.SaveChanges();
                        file.SaveAs(HttpContext.Server.MapPath("~/Images/Actor/") + actor.Id + System.IO.Path.GetExtension(file.FileName));
                        actor.Image = "/images/Actor/" + actor.Id + System.IO.Path.GetExtension(file.FileName);

                        db.Entry(actor).State = System.Data.Entity.EntityState.Modified;
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
        // GET: Actor/Edit/5
        public ActionResult Edit(int id)
        {
            return View(db.Actors.SingleOrDefault(x => x.Id == id));
        }

        // POST: Actor/Edit/5
        [HttpPost]
        public ActionResult Edit(Actor actor, HttpPostedFileBase file)
        {
            try
            {
                // TODO: Add update logic here

                if (ModelState.IsValid)
                {
                    if (file != null)
                    {
                        if (System.IO.File.Exists(Server.MapPath(actor.Image)))
                        {
                            System.IO.File.Delete(Server.MapPath(actor.Image));
                        }
                        file.SaveAs(HttpContext.Server.MapPath("~/Images/Actor/") + actor.Id + System.IO.Path.GetExtension(file.FileName));
                        actor.Image = "/images/Actor/" + actor.Id + System.IO.Path.GetExtension(file.FileName);
                    }
                    db.Entry(actor).State = System.Data.Entity.EntityState.Modified;

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
        // GET: Actor/Delete/5
        

        // POST: Actor/Delete/5
        [HttpPost]
        public ActionResult Delete(Actor actor)
        {
            try
            {
                // TODO: Add delete logic here

                Actor MyActor = db.Actors.SingleOrDefault(x => x.Id == actor.Id);
                if (System.IO.File.Exists(Server.MapPath(MyActor.Image)))
                {
                    System.IO.File.Delete(Server.MapPath(MyActor.Image));
                }
                db.Actors.Remove(MyActor);
                db.SaveChanges();
                return Json(new { result = 1 });
            }
            catch
            {
                return View();
            }
        }


        [HttpGet]
        public ActionResult ActorProfile(int? id)
        {
            Actor actor = db.Actors.SingleOrDefault(x => x.Id == id);
            List<MovieActor> movieActors = db.MovieActors.Where(x => x.ActorId == id).ToList();
            List<Movie> movies = new List<Movie>();

            foreach (var item in movieActors)
            {
                movies.Add(db.Movies.SingleOrDefault(x=>x.id == item.MovieId));
            }

            ActorProfileViewModel actorProfileViewModel = new ActorProfileViewModel { 
                Actor = actor,
                Movies = movies
            };

            return View(actorProfileViewModel);
        }
        [HttpPost]
        public JsonResult IsFirstNameExists(string firstName, int? id)
        {

            Actor actor = null;
            if (id != null)
            {
                actor = db.Actors.Where(x => x.FirstName == firstName).Where(x => x.Id != id)
                .SingleOrDefault();
            }
            else
            {
                actor = db.Actors.SingleOrDefault(x => x.FirstName == firstName);
            }
            if (actor == null)
            {
                return Json(true);
            }
            return Json(false);
        }

        [HttpPost]
        public JsonResult IsLastNameExists(string lastName, int? id)
        {

            Actor actor = null;
            if (id != null)
            {
                actor = db.Actors.Where(x => x.LastName == lastName).Where(x => x.Id != id)
                .SingleOrDefault();
            }
            else
            {
                actor = db.Actors.SingleOrDefault(x => x.LastName == lastName);
            }

            if (actor == null)
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
