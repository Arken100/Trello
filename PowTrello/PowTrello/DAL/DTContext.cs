using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PowTrello.Models;

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

        }

        public DbSet<Board> Board { get; set; }
        // public DbSet<CurrencyInformation> CurrencyInformations { get; set; }

        
    }
}
