using IMDB.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IMDB.Startup))]
namespace IMDB
{
    
    public partial class Startup
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateDefaultRolesAndUsers();
        }
        public void CreateDefaultRolesAndUsers()
        {
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            IdentityRole AdminRole = new IdentityRole();
            if (!RoleManager.RoleExists("Admin"))
            {
                AdminRole.Name = "Admin";
                RoleManager.Create(AdminRole);
                ApplicationUser User = new ApplicationUser();
                User.UserName = "admin";
                User.Email = "admin@admin.com";
                User.ProfileImage = "A:/AD/IMDB/IMDB/images/Profile/user.png";
                User.FirstName = "none";
                User.LastName = "none";
                var Check = UserManager.Create(User, "@Dmin123!");
                if (Check.Succeeded)
                {
                    UserManager.AddToRole(User.Id, "Admin");
                }

            }

            IdentityRole UserRole = new IdentityRole();
            if (!RoleManager.RoleExists("User"))
            {
                UserRole.Name = "User";
                RoleManager.Create(UserRole);              
            }
        }
    }
}
