using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System;
using Lelo.DAL;

namespace Lelo.Models
{
   

    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser<Guid, UserLogin, UserRole, UserClaim>
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser, Guid> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public ApplicationUser()
        {
            Id = Guid.NewGuid();
        }
    }

  

    public class Role : IdentityRole<Guid, UserRole>
    {
        public Role()
        {
            Id = Guid.NewGuid();
        }
        public Role(string name) : this() { Name = name; }
    }

    public class RoleStore : RoleStore<Role, Guid, UserRole>
    {
        public RoleStore(ApplicationDbContext context)
            : base(context)
        {
        }
    }
    public class UserClaim : IdentityUserClaim<Guid> { }
    public class UserLogin : IdentityUserLogin<Guid> { }
    public class UserRole : IdentityUserRole<Guid> { }

    public class UserStore : UserStore<ApplicationUser, Role, Guid, UserLogin, UserRole, UserClaim>
    {
        public UserStore(ApplicationDbContext context) : base(context) { }
    }
}