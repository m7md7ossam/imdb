using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace IMDB.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string ProfileImage { get; set; }

        [Required(ErrorMessage = "Please enter first name.")]
        [RegularExpression(@"^([A-z][A-Za-z]*\s+[A-Za-z]*)|([A-z][A-Za-z]*)$", ErrorMessage ="FirstName should not contain numbers")]
        public string FirstName { get; set; }


        [Required(ErrorMessage = "Please enter last name.")]
        [RegularExpression(@"^([A-z][A-Za-z]*\s+[A-Za-z]*)|([A-z][A-Za-z]*)$", ErrorMessage = "LastName should not contain numbers")]
        public string LastName { get; set; }

        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0]{1}[1]{1})\)?(\d{9})$", ErrorMessage = "PhoneNumber should be 11 numbers start with 01")]
        public string PhoneNumber { get; set; }

        //[Required(ErrorMessage = "Please enter your email addres.")]
        //[EmailAddress(ErrorMessage = "Invalid Email Address")]
        //public string Email { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieDirector> MoviesDirectors { get; set; }
        public DbSet<MovieGenre> MoviesGenres { get; set; }
        public DbSet<MovieActor> MovieActors { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<LikeDislikeMovie> LikeDislikeMovies{ get; set; }
        public DbSet<FavoriteActor> FavoriteActors{ get; set; }
        public DbSet<FavoriteDirector> FavoriteDirectors{ get; set; }
        public DbSet<FavoriteMovie> FavoriteMovies{ get; set; }
        public DbSet<Country> Countries{ get; set; }
        public DbSet<MovieCountry> MovieCountry { get; set; }
    }
}