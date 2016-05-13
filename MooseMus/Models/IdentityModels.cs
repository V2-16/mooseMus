using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MooseMus.Models.Entities;

namespace MooseMus.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    // Interface used for unittests
    public interface IAppDataContext
    {
        IDbSet<CourseModel> course { get; set; }
        IDbSet<CourseUsersModel> courseUser { get; set; }
        IDbSet<ProjectModel> project { get; set; }
        IDbSet<ProjectPartModel> projectPart { get; set; }
        IDbSet<ResultModel> result { get; set; }
        IDbSet<UserModel> user { get; set; }
        int SaveChanges();
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser> , IAppDataContext
    {
        public IDbSet<CourseModel> course { get; set; }
        public IDbSet<ProjectModel> project { get; set; }
        public IDbSet<ProjectPartModel> projectPart { get; set; }
        public IDbSet<ResultModel> result { get; set; }
        public IDbSet<UserModel> user { get; set; }
        public IDbSet<CourseUsersModel> courseUser { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {

        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}