using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PowTrello.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace PowTrello.DAL
{
    public class DTContext : DbContext
    {
        public DTContext() : base("TrelloConnectionString")
        {
            System.Data.Entity.Database.SetInitializer(new MigrateDatabaseToLatestVersion<DTContext, ConfigurationDb>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
            modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });

        }

        public DbSet<Board> Board { get; set; }

        public System.Data.Entity.DbSet<PowTrello.Models.ApplicationUser> ApplicationUsers { get; set; }
        // public DbSet<CurrencyInformation> CurrencyInformations { get; set; }


    }
}
