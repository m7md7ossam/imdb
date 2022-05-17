using Microsoft.AspNet.Identity.EntityFramework;
using IMDB.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IMDB.ViewModels;

namespace IMDB.Controllers
{   [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Role
        public ActionResult Index()
        {
            return View(db.Roles.ToList());
        }

        // GET: Role/Details/5
        public ActionResult Details(string id)
        {
            var role = db.Roles.Find(id);
            if(role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // GET: Role/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Role/Create
        [HttpPost]
        public ActionResult Create(EditRoleViewModel editRoleViewModel)
        {
            try
            {
                IdentityRole role = new IdentityRole 
                {
                    Name = editRoleViewModel.Name
                };
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    db.Roles.Add(role);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(role);
            }
            catch
            {
                return View();
            }
        }

        // GET: Role/Edit/5
        public ActionResult Edit(string id)
        {
            var role = db.Roles.Find(id);
            EditRoleViewModel editRoleViewModel = new EditRoleViewModel 
            {
                Id= role.Id,
                Name = role.Name
            };
            if(role == null)
            {
                return HttpNotFound();
            }
            return View(editRoleViewModel);
        }

        // POST: Role/Edit/5
        [HttpPost]
        public ActionResult Edit(EditRoleViewModel editRoleViewModel)
        {
            IdentityRole role = new IdentityRole 
            {
                Id = editRoleViewModel.Id,
                Name = editRoleViewModel.Name
            };
            if (ModelState.IsValid)
            {
                db.Entry(role).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(role);
        }



        // POST: Role/Delete/5
        [HttpPost]
        public ActionResult Delete(string id)
        {
            try
            {
                // TODO: Add delete logic here
                var myRole = db.Roles.Find(id);
                db.Roles.Remove(myRole);
                db.SaveChanges();
                return Json(new { result = 1 });
            }
            catch
            {
                return View();
            }
        }
        [HttpPost]
        public JsonResult IsUserRoleNameExists(string name, string id)
        {

            IdentityRole role = null;
            role = db.Roles.Where(x => x.Name == name).Where(x=>x.Id != id).SingleOrDefault();
            if (role == null)
            {
                return Json(true);
            }
            return Json(false);
        }
    }
}
