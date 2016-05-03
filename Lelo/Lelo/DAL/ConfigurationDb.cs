using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace Lelo.DAL
{
    internal sealed class ConfigurationDb : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public ConfigurationDb()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;

            ContextKey = "Lelo.DAL.ApplicationDbContext";
        }

        protected override void Seed(ApplicationDbContext context)
        {
            CreateTestRecord(context);
        }

        public void CreateTestRecord(ApplicationDbContext context)
        {
            //var firstEntry = context.CurrencyInformations.ToList();
            //if (firstEntry.Count == 0)
            //{
            //    context.CurrencyInformations.AddOrUpdate(newTestEntry => newTestEntry.Id, new CurrencyInformation()
            //    {
            //        CodeOfCurrency = "YYY",
            //        CreatedAt = DateTime.Now,
            //        DateFromFile = DateTime.Now.AddDays(20),
            //        TheValueOfTheExchangeRate = 3.33M

            //    });
            //}
        }
    }
}
