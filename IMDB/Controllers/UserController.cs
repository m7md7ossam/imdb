using IMDB.Models;
using IMDB.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IMDB.Controllers
{
    
    public class UserController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: User
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            string userId = User.Identity.GetUserId();
            return View(db.Users.Where(x=>x.Id != userId).ToList());
        }

        [Authorize(Roles = "Admin")]
        // GET: User/Details/5
        public ActionResult Details(string id)
        {
            return View(db.Users.Find(id));
        }


        // GET: User/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(string id)
        {
            ApplicationUser user = db.Users.Find(id);
            EditUserViewModel editUserViewModel = new EditUserViewModel 
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                ProfileImage = user.ProfileImage,
                UserName = user.UserName,
                AccessFailedCount = user.AccessFailedCount,
                EmailConfirmed = user.EmailConfirmed,
                LockoutEnabled = user.LockoutEnabled,
                LockoutEndDateUtc = user.LockoutEndDateUtc,
                PasswordHash = user.PasswordHash,
                PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                SecurityStamp = user.SecurityStamp,
                TwoFactorEnabled = user.TwoFactorEnabled
            };
            return View(editUserViewModel);
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(EditUserViewModel userViewModel, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser
                {
                    Id = userViewModel.Id,
                    Email = userViewModel.Email,
                    FirstName = userViewModel.FirstName,
                    LastName = userViewModel.LastName,
                    PhoneNumber = userViewModel.PhoneNumber,
                    //ProfileImage = userViewModel.ProfileImage,
                    UserName = userViewModel.UserName,
                    AccessFailedCount = userViewModel.AccessFailedCount,
                    EmailConfirmed = userViewModel.EmailConfirmed,
                    LockoutEnabled = userViewModel.LockoutEnabled,
                    LockoutEndDateUtc = userViewModel.LockoutEndDateUtc,
                    PasswordHash = userViewModel.PasswordHash,
                    PhoneNumberConfirmed = userViewModel.PhoneNumberConfirmed,
                    SecurityStamp = userViewModel.SecurityStamp,
                    TwoFactorEnabled = userViewModel.TwoFactorEnabled
                };
                if (file != null)
                {
                    if (userViewModel.ProfileImage != "/images/Profile/user.png")
                    {
                        if (System.IO.File.Exists(Server.MapPath(userViewModel.ProfileImage)))
                        {
                            System.IO.File.Delete(Server.MapPath(userViewModel.ProfileImage));
                        }
                    }
                    file.SaveAs(HttpContext.Server.MapPath("~/Images/Profile/") + user.Id + System.IO.Path.GetExtension(file.FileName));
                    user.ProfileImage = "/images/Profile/" + user.Id + System.IO.Path.GetExtension(file.FileName);
                }
                else
                {
                    user.ProfileImage = userViewModel.ProfileImage;
                }
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }


        [Authorize(Roles = "Admin")]
        // POST: User/Delete/5
        [HttpPost]
        public ActionResult Delete(string id)
        {
            var User = db.Users.Find(id);
            if (User.ProfileImage != "/images/Profile/user.png") 
            {
                if (System.IO.File.Exists(Server.MapPath(User.ProfileImage)))
                {
                    System.IO.File.Delete(Server.MapPath(User.ProfileImage));
                }
            }

            db.Users.Remove(User);
            db.SaveChanges();
            return Json(new { result = 1 });

        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public JsonResult IsUserNameExists(string UserName, string id)
        {
            
            ApplicationUser user = null;
            user = db.Users.Where(x => x.UserName == UserName).Where(x=>x.Id != id).SingleOrDefault();
            if(user == null)
            {
                return Json(true);
            }
            return Json(false);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public JsonResult IsUserEmailExists(string Email, string id)
        {
            
            ApplicationUser user = null;
            user = db.Users.Where(x => x.Email == Email).Where(x=>x.Id != id).SingleOrDefault();
            if(user == null)
            {
                return Json(true);
            }
            return Json(false);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public JsonResult IsUserPhoneNumberExists(string PhoneNumber, string id)
        {
            
            ApplicationUser user = null;
            if ((db.Users.SingleOrDefault(x=>x.Id == id)).PhoneNumber != null)
            {
                user = db.Users.Where(x => x.PhoneNumber == PhoneNumber).Where(x => x.Id != id).SingleOrDefault();
            }
            else
            {
                user = db.Users.SingleOrDefault(x => x.PhoneNumber == PhoneNumber);
            }
            if (user == null)
            {
                return Json(true);
            }
            return Json(false);
        }
        [Authorize(Roles = "Admin")]
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

        [HttpPost]
        [Authorize]
        public string GetProfileImage(string id)
        {
            ApplicationUser user = db.Users.SingleOrDefault(x=>x.Id == id);
            return user.ProfileImage;
        }


    }
}
