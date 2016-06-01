using Lelo.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Lelo.DAL
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, Role, Guid, UserLogin, UserRole, UserClaim>
    {
        public ApplicationDbContext() : base("DefaultConnection")
        {
            System.Data.Entity.Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, ConfigurationDb>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }


        public DbSet<Board> Boards { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<LeloTask> LeloTasks { get; set; }
        public DbSet<TaskList> TaskLists { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Priority> Priorities { get; set; }

        //public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    }
}