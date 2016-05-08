using Lelo.DAL;
using Lelo.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lelo.Controllers
{
    public class BaseController : Controller
    {
        protected ApplicationDbContext db = new ApplicationDbContext();
        protected Guid CurrentUserId;
        protected ApplicationUser CurrentUser;

        protected UserManager<ApplicationUser, Guid> UserManager;
        protected RoleManager<Role, Guid> RoleManager;

        protected BaseController()
        {
            UserManager = new UserManager<ApplicationUser, Guid>(new UserStore<ApplicationUser, Role, Guid, UserLogin, UserRole, UserClaim>(db));
            RoleManager = new RoleManager<Role, Guid>(new RoleStore(db));
        }

        public void BoardsController()
        {
            CurrentUserId = User != null ? new Guid(User.Identity.GetUserId()) : Guid.Empty;
            CurrentUser = CurrentUserId != Guid.Empty ? db.Users.First(x => x.Id == CurrentUserId) : null;
        }

        public Guid GetCurrentUserId()
        {
            var toReturn = User != null ? new Guid(User.Identity.GetUserId() == null ? Guid.Empty.ToString() : User.Identity.GetUserId().ToString()) : Guid.Empty;
            return toReturn;
        }

        public void SetCurrentUser()
        {
            CurrentUserId = User != null ? new Guid(User.Identity.GetUserId() == null ? Guid.Empty.ToString() : User.Identity.GetUserId().ToString()) : Guid.Empty;
            CurrentUser = CurrentUserId == Guid.Empty ? null : db.Users.First(x => x.Id == CurrentUserId);
        }

    }
}
