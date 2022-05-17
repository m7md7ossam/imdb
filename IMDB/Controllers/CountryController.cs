using IMDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IMDB.Controllers
{
    [Authorize(Roles = "Admin")]

    public class CountryController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Genre
        public ActionResult Index()
        {
            return View(db.Countries.ToList());
        }

        // GET: Genre/Details/5
        public ActionResult Details(int id)
        {
            return View(db.Countries.SingleOrDefault(x => x.Id == id));
        }

        // GET: Genre/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Genre/Create
        [HttpPost]
        public ActionResult Create(Country country)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    db.Countries.Add(country);
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
            return View(db.Countries.SingleOrDefault(x => x.Id == id));
        }

        // POST: Genre/Edit/5
        [HttpPost]
        public ActionResult Edit(Country country)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    db.Entry(country).State = System.Data.Entity.EntityState.Modified;
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
        public ActionResult Delete(Country country)
        {
            try
            {
                // TODO: Add delete logic here
                Country myCountry = db.Countries.SingleOrDefault(x => x.Id == country.Id);
                db.Countries.Remove(myCountry);
                db.SaveChanges();
                return Json(new { result = 1 });
            }
            catch
            {
                return View();
            }
        }
        [HttpPost]
        public JsonResult IsCountryNameExists(string countryName, int? id)
        {

            Country country = null;
            if (id != null)
            {
                country = db.Countries.Where(x => x.CountryName == countryName).Where(x => x.Id != id)
                .SingleOrDefault();
            }
            else
            {
                country = db.Countries.SingleOrDefault(x => x.CountryName == countryName);
            }

            if (country == null)
            {
                return Json(true);
            }
            return Json(false);
        }
    }
}

