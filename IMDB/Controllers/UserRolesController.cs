using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using IMDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IMDB.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserRolesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        
        // GET: UserRoles
        public ActionResult Index()
        {
            //var UserRole = new IdentityUserRole().;
            string UserId = User.Identity.GetUserId();
            return View(db.Users.Where(x=>x.Id != UserId).ToList());
        }

        public bool IsAdmin(string id)
        {
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            ApplicationUser user = db.Users.SingleOrDefault(x=>x.Id == id);
            string userRole = UserManager.GetRoles(id).SingleOrDefault();
            if(userRole == "Admin")
            {
                return true;
            }
            return false;
        }

        public ActionResult MakeAdmin(string id)
        {
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            UserManager.RemoveFromRole(id, "User");
            UserManager.AddToRole(id, "Admin");
            return RedirectToAction("Index");
        }

        public ActionResult MakeUser(string id)
        {
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            UserManager.RemoveFromRole(id, "Admin");
            UserManager.AddToRole(id, "User");
            return RedirectToAction("Index");
        }


    }
}
