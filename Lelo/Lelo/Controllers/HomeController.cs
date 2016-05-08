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
    public class HomeController : BaseController
    {

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Init()
        {



            //List<Role> Roles = new List<Role>
            //{
            //    new Role{
            //    Name = "Admin"
            //    },
            //    new Role
            //    {
            //        Name = "LeloUser"
            //    }
            //};

            //Roles.ForEach(delegate (Role role)
            //    {
            //        db.Roles.Add(role);
            //    });

            ApplicationDbContext dbc = new ApplicationDbContext();
            string[] roles = { "Admin", "LeloUser" };

            foreach (string role in roles)
            {
                ApplicationUser userToCreate = new ApplicationUser();
               // var UserManager = new UserManager<ApplicationUser, Guid>(new UserStore(db));

                var UserManager = new UserManager<ApplicationUser, Guid>(new UserStore<ApplicationUser, Role, Guid, UserLogin, UserRole, UserClaim>(db));
                var RoleManager = new RoleManager<Role, Guid>(new RoleStore(db));

                userToCreate.UserName = userToCreate.Email = role.ToLower() + "@example.com";

                var adminresult = UserManager.Create(userToCreate, "123456");

                if (!RoleManager.RoleExists(role))
                {
                    var roleresult = RoleManager.Create<Role, Guid>(new Role(role));
                }

                if (adminresult.Succeeded)
                {
                    var result = UserManager.AddToRole(userToCreate.Id, role);
                }
            }



            return Content("222");
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }



    }
}